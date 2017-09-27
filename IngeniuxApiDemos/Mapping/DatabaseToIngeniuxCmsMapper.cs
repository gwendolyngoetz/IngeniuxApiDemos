using System.Collections.Generic;
using System.Linq;
using IngeniuxApiDemos.Common.Configuration;
using IngeniuxApiDemos.Source.Database.Model;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Components;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Pages;

namespace IngeniuxApiDemos.Mapping
{
    public class DatabaseToIngeniuxCmsMapper : Mapper<IEnumerable<Park>, IEnumerable<Page>>
    {
        public DatabaseToIngeniuxCmsMapper(IConfiguration config) : base(config) { }

        protected override IEnumerable<Page> ExecuteInternal(IEnumerable<Park> payload)
        {
            return payload.Take(5).Select(BuildPage);
        }

        private Page BuildPage(Park biography)
        {
            return new DemoPage
            {
                Moniker = CleanMonikerText(biography.Name),
                Heading = biography.Name,
                ParentId = _config.Target.Ingeniux.RootPageFolderId,
                DemoCopyComponent = new DemoCopyComponent
                {
                    Moniker = CleanMonikerText(biography.Name) + "_Copy",
                    Copy = biography.Description
                }
            };
        }
    }
}