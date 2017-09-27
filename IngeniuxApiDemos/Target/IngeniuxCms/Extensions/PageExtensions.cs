using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ingeniux.CMS;
using IngeniuxApiDemos.Target.IngeniuxCms.Model;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Attributes;
using IngeniuxApiDemos.Common.Extensions;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Extensions
{
    internal static class PageExtensions
    {
        internal static void SetElementValue(this IPage page, string key, string value)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            var element = page.Element(key);

            if (element == null)
            {
                return;
            }

            element.Value = value;
        }

        internal static void MapProperties(this IPage page, BaseIngeniuxModel source)
        {
            var properties = source.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);

            foreach (var property in properties)
            {
                var value = property.GetValue(source, null);

                if (value == null || value.ToString().IsNullOrWhiteSpace())
                {
                    continue;
                }

                if (property.GetCustomAttributes(typeof(IgnoreAttribute)).Any())
                {
                    continue;
                }

                if (property.PropertyType.IsSubclassOf(typeof(BaseIngeniuxModel)) || property.PropertyType == typeof(BaseIngeniuxModel))
                {
                    var obj = (BaseIngeniuxModel)value;

                    if (!obj.Id.IsNullOrWhiteSpace())
                    {
                        page.SetElementValue(property.Name, obj.Id);
                    }
                }
                else
                {
                    page.SetElementValue(property.Name, value.ToString());
                }
            }
        }

        internal static bool IsList(this IPage parentComponent, string listElementName)
        {
            var listElement = parentComponent.Element(listElementName) as ListElement;
            return listElement != null;
        }

        internal static bool IsComponentElement(this IPage parentComponent, string listElementName)
        {
            var element = parentComponent.Element(listElementName) as ComponentElement;
            return element != null;
        }
        
        internal static bool AddToList(this IPage parentComponent, string listElementName, IPage childComponent)
        {
            var listElement = parentComponent.Element(listElementName) as ListElement;

            if (listElement == null)
            {
                return false;
            }

            // Remove the default empty element.
            if (listElement._Elements.Count == 1 && listElement._Elements[0].GetType() == typeof(ComponentElement) && listElement._Elements[0].Value.IsNullOrWhiteSpace())
            {
                listElement.ClearAllListItems();
            }

            listElement.Expanded = true;
            listElement.AddListItem(new ComponentElement(listElement.ChildElementName)
            {
                Value = childComponent.Id,
                Expanded = true
            });
            
            return true;
        }
    }
}
