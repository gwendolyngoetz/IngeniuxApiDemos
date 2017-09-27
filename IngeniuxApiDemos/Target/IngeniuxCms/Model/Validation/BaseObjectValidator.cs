using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Attributes;
using IngeniuxApiDemos.Common.Extensions;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Model.Validation
{
    internal static class BaseObjectValidator
    {
        internal static ValidationResults Validate(BaseIngeniuxModel baseIngeniuxModel)
        {
            var result = new ValidationResults();

            if (baseIngeniuxModel.ComponentType != ComponentType.Create)
            {
                return result;
            }

            ValidateRequiredFields(baseIngeniuxModel, result);

            return result;
        }

        private static void ValidateRequiredFields(BaseIngeniuxModel baseIngeniuxModel, ValidationResults result)
        {
            var baseObjectType = baseIngeniuxModel.GetType();
            var properties = baseObjectType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).ToList();

            foreach (var property in properties)
            {
                var value = property.GetValue(baseIngeniuxModel, null);

                if (property.GetCustomAttributes(typeof(IgnoreAttribute)).Any())
                {
                    continue;
                }

                var hasRequiredAttribute = property.GetCustomAttributes(typeof(RequiredAttribute)).Any();

                if (hasRequiredAttribute && property.PropertyType.IsAssignableFrom(typeof(string)) && value != null && !value.ToString().IsNullOrWhiteSpace())
                {
                    result.Add(new ValidationResult(property.Name, value, true, baseObjectType.Name));
                }
                else if (hasRequiredAttribute && (property.PropertyType.IsSubclassOf(typeof(BaseIngeniuxModel)) || property.PropertyType == typeof(BaseIngeniuxModel)) && value != null)
                {
                    var obj = (BaseIngeniuxModel) value;
                    result.Add(obj.Validate());
                }
                else if (property.PropertyType.IsGenericType && (property.PropertyType.GetGenericTypeDefinition() == typeof(List<>)))
                {
                    var modelValueList = (value as IEnumerable);

                    // Value is not enumerable, move on
                    if (modelValueList == null)
                    {
                        continue;
                    }

                    foreach (var item in modelValueList)
                    {
                        if (!item.GetType().IsSubclassOf(typeof(BaseIngeniuxModel)))
                        {
                            continue;
                        }

                        var obj = (BaseIngeniuxModel)item;
                        ValidateAllowableComponents(property, baseIngeniuxModel, result);
                        result.Add(obj.Validate());
                    }
                }
                else if (hasRequiredAttribute)
                {
                    result.Add(new ValidationResult(property.Name, value, false, baseObjectType.Name));
                }
            }
        }

        private static void ValidateAllowableComponents(PropertyInfo property, BaseIngeniuxModel baseIngeniuxModel, ValidationResults result)
        {
            var baseObjectType = baseIngeniuxModel.GetType();

            var value = property.GetValue(baseIngeniuxModel, null);
            var attribute = property.GetCustomAttribute(typeof(AllowedComponentsAttribute)) as AllowedComponentsAttribute;

            if (attribute == null)
            {
                return;
            }

            if (!property.PropertyType.IsGenericType || (property.PropertyType.GetGenericTypeDefinition() != typeof(List<>)))
            {
                return;
            }

            var modelValueList = (value as IEnumerable);

            // Value is not enumerable, move on
            if (modelValueList == null)
            {
                return;
            }

            foreach (var item in modelValueList)
            {
                if (!item.GetType().IsSubclassOf(typeof(BaseIngeniuxModel)))
                {
                    break;
                }

                var baseObj = (BaseIngeniuxModel) item;

                var hasMatch = attribute.Values.Any(x => x == baseObj.SchemaRootName);
                result.Add(new ValidationResult(property.Name, baseObj.SchemaRootName, hasMatch, $"{baseObjectType.Name}.{property.Name} does not allow {baseObj.SchemaRootName}"));
            }
        }
    }
}
