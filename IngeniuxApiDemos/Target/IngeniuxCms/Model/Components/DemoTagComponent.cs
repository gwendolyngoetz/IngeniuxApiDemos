using System.ComponentModel.DataAnnotations;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Model.Components
{
    // Class generated on 2017-09-09 11:11:30
    // AllowedComponents current as of generation date
    public partial class DemoTagComponent : Component
    {
        public override string SchemaRootName => "DemoTagComponent";
        public override string SchemaFriendlyName => "DemoTagComponent";


        [Required]
        public string Name { get; set; }
    }
}
