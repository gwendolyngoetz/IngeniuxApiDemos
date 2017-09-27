using System;
using System.Collections.Generic;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Model.Attributes
{
    public class AllowedComponentsAttribute : Attribute
    {
        public IEnumerable<string> Values { get; }

        public AllowedComponentsAttribute(params string[] values)
        {
            Values = values;
        }
    }
}