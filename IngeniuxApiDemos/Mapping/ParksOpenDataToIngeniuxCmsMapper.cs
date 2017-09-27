using System.Collections.Generic;
using System.Linq;
using IngeniuxApiDemos.Common.Configuration;
using IngeniuxApiDemos.Source.ParksOpenData.Model;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Components;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Pages;

namespace IngeniuxApiDemos.Mapping
{
    public class ParksOpenDataToIngeniuxCmsMapper : Mapper<IEnumerable<Park>, IEnumerable<Page>>
    {
        public ParksOpenDataToIngeniuxCmsMapper(IConfiguration config) : base(config) { }


        protected override IEnumerable<Page> ExecuteInternal(IEnumerable<Park> payload)
        {
            var parkUrls = string.Join("", payload.OrderBy(park => park.CommonName).Select(park => $"<li><a href='{park.Website.Url}'>{park.CommonName}</a></li>"));

            return new List<Page>
            {
                new DemoPage
                {
                    Moniker = "Parks",
                    Heading = "Parks",
                    ParentId = _config.Target.Ingeniux.RootPageFolderId,
                    DemoCopyComponent = new DemoCopyComponent
                    {
                        Moniker = "Parks_Copy",
                        Copy = $"<ul>{parkUrls}</ul>"
                    }
                }
            };
        }
    }
}