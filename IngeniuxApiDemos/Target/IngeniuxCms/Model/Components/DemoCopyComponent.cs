using System.ComponentModel.DataAnnotations;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Model.Components
{
    // Class generated on 2017-09-09 11:11:30
    // AllowedComponents current as of generation date
    public partial class DemoCopyComponent : Component
    {
        public override string SchemaRootName => "DemoCopyComponent";
        public override string SchemaFriendlyName => "DemoCopyComponent";


        [Required]
        public string Copy { get; set; }
    }
}
