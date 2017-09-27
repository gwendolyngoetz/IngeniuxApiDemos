using System.Collections.Generic;

namespace IngeniuxApiDemos.Source.Slickplan.Model
{
    internal class SlickplanSection
    {
        public string SectionId { get; set; }
        public List<SlickplanElement> Elements { get; set; } = new List<SlickplanElement>();
    }
}