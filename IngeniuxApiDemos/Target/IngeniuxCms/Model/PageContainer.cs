using System.Collections.Generic;
using Ingeniux.CMS;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Model
{
    public class PageContainer
    {
        public string Name { get; set; }
        public IPage Page { get; set; }
        public List<PageContainer> Children { get; set; } = new List<PageContainer>();
        public List<PageContainer> Pages { get; set; } = new List<PageContainer>();

        public override string ToString()
        {
            return $"{{ Id: '{Page.Id}', Name: '{Name}', PageName: '{Page.Name}' }}";
        }
    }
}