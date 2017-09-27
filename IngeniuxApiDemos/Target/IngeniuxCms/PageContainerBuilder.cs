using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ingeniux.CMS;
using IngeniuxApiDemos.Target.IngeniuxCms.Extensions;
using IngeniuxApiDemos.Target.IngeniuxCms.Model;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Attributes;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Validation;
using IngeniuxApiDemos.Target.IngeniuxCms.Repository;
using NLog;
using IngeniuxApiDemos.Common.Exceptions;
using IngeniuxApiDemos.Common.Extensions;
using DataImportException = IngeniuxApiDemos.Common.Exceptions.DataImportException;

namespace IngeniuxApiDemos.Target.IngeniuxCms
{
    internal class PageContainerBuilder
    {
        private readonly IIngeniuxRepository _repository;
        private readonly ILogger _logger;

        internal PageContainerBuilder(IIngeniuxRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;

        }

        internal PageContainerBuilder(IIngeniuxRepository repository) : this(repository, LogManager.GetCurrentClassLogger()) { }

        public PageContainer BuildPageTreeFromModel(string rootPageId, BaseIngeniuxModel source)
        {
            var parentPageContainer = new PageContainer
            {
                Name = string.Empty,
                Page = CreatePage(rootPageId, source)
            };

            foreach (var property in source.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public))
            {
                var value = property.GetValue(source, null);

                if (ShouldStopProcessing(value, property, source))
                {
                    continue;
                }

                if (value.GetType().IsSubclassOf(typeof(BaseIngeniuxModel)))
                {
                    var childPageContainer = BuildPageTreeFromModel(parentPageContainer.Page.Id, (BaseIngeniuxModel)value);
                    childPageContainer.Name = property.Name;
                    parentPageContainer.Children.Add(childPageContainer);
                }
                else
                {
                    // Recurse list objects
                    foreach (var item in (IEnumerable) value)
                    {
                        var baseObject = item as BaseIngeniuxModel;

                        // Not a part of the object model, move on
                        if (baseObject == null)
                        {
                            continue;
                        }

                        var childPageContainer = BuildPageTreeFromModel(parentPageContainer.Page.Id, baseObject);
                        childPageContainer.Name = property.Name;
                        parentPageContainer.Children.Add(childPageContainer);
                    }
                }
            }

            return parentPageContainer;
        }

        private IPage CreatePage(string rootContainerId, BaseIngeniuxModel source)
        {
            if (source.ComponentType != ComponentType.Create)
            {
                return new NullPage(source.Id);
            }

            try
            {
                source.Validate().ThrowIfErrors();
            }
            catch (ValidationException ex)
            {
                throw new DataImportException($"Page is not valid: {source.Moniker}", ex);
            }

            var schema = _repository.GetSchemaByRootName(source.SchemaRootName);

            rootContainerId = source.HasParentId ? source.ParentId : rootContainerId;

            var page = _repository.CreatePage(schema, source.Moniker, rootContainerId);
            page = _repository.GetPageById(page.Id);
            page.MapProperties(source);

            _repository.Save(page);
            _logger.Debug($"Created: {page.Id} ({page.Name})");

            return page;
        }

        private bool ShouldStopProcessing(object value, PropertyInfo property, BaseIngeniuxModel source)
        {
            // Has IgnoreAttribute, move on
            if (property.GetCustomAttributes(typeof(IgnoreAttribute)).Any())
            {
                return true;
            }

            // No value, move on
            if (value == null || value.ToString().IsNullOrWhiteSpace())
            {
                return true;
            }

            // Linking pages not creating new, move on
            if (source.ComponentType != ComponentType.Create)
            {
                return true;
            }

            var isModelType = property.PropertyType.IsSubclassOf(typeof(BaseIngeniuxModel));

            if (isModelType)
            {
                return false;
            }


            var isNotGenericList = !(!property.PropertyType.IsGenericType || property.PropertyType.GetGenericTypeDefinition() != typeof(List<>));

            // Not a generic list, move on
            if (isNotGenericList)
            {
                return true;
            }
        
            var list = value as IEnumerable;

            // Value is not enumerable, move on
            if (list == null)
            {
                return true;
            }



            return false;
        }
    }
}