using System.Collections.Generic;
using System.Linq;
using IngeniuxApiDemos.Common.Configuration;
using IngeniuxApiDemos.Source.Slickplan.Model;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Pages;

namespace IngeniuxApiDemos.Mapping
{
    public class SlickplanToIngeniuxCmsMapper : Mapper<IEnumerable<SlickplanItem>, IEnumerable<Page>>
    {
        public SlickplanToIngeniuxCmsMapper(IConfiguration config) : base(config) { }

        protected override IEnumerable<Page> ExecuteInternal(IEnumerable<SlickplanItem> payload)
        {
            return payload.Select(BuildPage);
        }

        private Page BuildPage(SlickplanItem slickplanItem)
        {
            var page = Create(slickplanItem);

            foreach (var child in slickplanItem.Children)
            {
                page.Pages.Add(BuildPage(child));
            }

            return page;
        }

        private Page Create(SlickplanItem slickplanItem)
        {
            return new DemoPage
            {
                Moniker = CleanMonikerText(slickplanItem.Text),
                Heading = slickplanItem.Text,
                ParentId = _config.Target.Ingeniux.RootPageFolderId,
                Text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Posuere sollicitudin aliquam ultrices sagittis orci a scelerisque purus. Enim tortor at auctor urna nunc id cursus. Facilisi nullam vehicula ipsum a arcu. Elementum eu facilisis sed odio morbi quis commodo. Ultrices neque ornare aenean euismod. Interdum consectetur libero id faucibus nisl tincidunt eget nullam non. Odio morbi quis commodo odio.",
            };
        }
    }
}
