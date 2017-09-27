using IngeniuxApiDemos.Target.IngeniuxCms.Model.Attributes;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Validation;
using IngeniuxApiDemos.Common.Extensions;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Model
{
    public abstract class BaseIngeniuxModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        [Ignore]
        public bool HasParentId => !ParentId.IsNullOrWhiteSpace();
        public string Moniker { get; set; }

        [Ignore]
        public ComponentType ComponentType { get; set; } = ComponentType.Create;

        [Ignore]
        public abstract string SchemaRootName { get; }
        [Ignore]
        public abstract string SchemaFriendlyName { get; }
        
        public ValidationResults Validate()
        {
            return BaseObjectValidator.Validate(this);
        }
    }
}
