using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Attributes;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Components;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Model.Pages
{
    // Class generated on 2017-09-09 11:11:30
    // AllowedComponents current as of generation date
    public partial class DemoPage : Page
    {
        public override string SchemaRootName => "DemoPage";
        public override string SchemaFriendlyName => "DemoPage";


        [AllowedComponents(
            "DemoTagComponent"
        )]
        public List<Component> DemoTagComponents { get; set; } = new List<Component>();

        [Required]
        public string Heading { get; set; }

        public string Text { get; set; }

        public DemoCopyComponent DemoCopyComponent { get; set; }
    }
}
