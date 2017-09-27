using System.Collections.Generic;

namespace IngeniuxApiDemos.Source.Slickplan.Model
{
    public class SlickplanItem
    {
        public string Id { get; }
        public int Level { get; }
        public string Text { get; set; }
        public int Order { get; }
        public SlickplanItem ParentSlickplan { get; }
        public List<SlickplanItem> Children { get; } = new List<SlickplanItem>();
        public List<SlickplanItem> Contents { get;} = new List<SlickplanItem>();
        public string PageType { get; set; }

        public SlickplanItem(SlickplanElement slickplanElement, SlickplanItem parentSlickplanItem)
        {
            Id = slickplanElement.Id;
            Level = slickplanElement.Level;
            Text = slickplanElement.Text;
            Order = slickplanElement.Order;
            PageType = slickplanElement.PageType;
            ParentSlickplan = parentSlickplanItem;
        }
    }
}
