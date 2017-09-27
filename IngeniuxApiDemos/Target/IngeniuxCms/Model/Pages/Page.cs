using System.Collections.Generic;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Attributes;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Model.Pages
{
    public abstract class Page : BaseIngeniuxModel
    {
        [Ignore]
        public List<Page> Pages { get; set; } = new List<Page>();
    }
}