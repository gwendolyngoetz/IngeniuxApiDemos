
namespace IngeniuxApiDemos.Source.Slickplan.Model
{
    public class SlickplanElement
    {
        public string Id { get; set; }
        public int Level { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public string ParentId { get; set; }
        public string SectionId { get; set; }
        public string PageType { get; set; }
        public bool IsChildSection { get; set; }
    }
}