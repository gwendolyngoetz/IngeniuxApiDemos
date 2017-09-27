using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using IngeniuxApiDemos.Common.Extensions;
using IngeniuxApiDemos.Source.Slickplan.Model;
using DataImportException = IngeniuxApiDemos.Common.Exceptions.DataImportException;

namespace IngeniuxApiDemos.Source.Slickplan.XmlDeserialization
{
    public interface ISlickplanXmlDeserializer
    {
        SlickplanItem Deserialize(string xml);
    }

    public class SlickplanXmlDeserializer : ISlickplanXmlDeserializer
    {
        private readonly IDictionary<string, string> _pagesTypes;

        public SlickplanXmlDeserializer(IEnumerable<PageType> pagesTypes)
        {
            _pagesTypes = pagesTypes.ToDictionary(x => x.Id, x => x.Name);
        }

        public SlickplanItem Deserialize(string xml)
        {
            var xdoc = XDocument.Parse(xml);
            var sitemapEl = xdoc.Element("sitemap");

            if (sitemapEl == null)
            {
                throw new DataImportException("Could not find sitemap element in xml.");
            }

            var sectionElements = sitemapEl.Elements("section");

            if (sectionElements == null)
            {
                throw new DataImportException("Could not find section element in xml.");
            }

            var sections = GetSections(sectionElements);
            var elements = SyncSectionElements(sections);
            var rawCell = elements.First(x => x.Level == 0);
            var rootCell = new SlickplanItem(rawCell, null);
            PopulateCells(elements.Where(x => x.Level != 0).ToList(), rootCell);

            return rootCell;
        }

        private List<SlickplanSection> GetSections(IEnumerable<XElement> sectionElements)
        {
            var sections = new List<SlickplanSection>();

            foreach (var sectionElement in sectionElements)
            {
                var cellsElement = sectionElement.Element("cells");

                if (cellsElement == null)
                {
                    throw new DataImportException("Could not find cells element in xml.");
                }

                sections.Add(new SlickplanSection
                {
                    SectionId = sectionElement.Attribute("id")?.Value,
                    Elements = cellsElement.Elements().Select(DeserializeElement).ToList()
                });
            }
            return sections;
        }

        private SlickplanElement DeserializeElement(XElement element)
        {
            var archetype = element.Element("archetype")?.Value;
            string pageType = "";

            if (!archetype.IsNullOrWhiteSpace())
            {
                pageType = _pagesTypes[archetype];
            }

            return new SlickplanElement
            {
                Id = element.Attribute("id")?.Value,
                Level = element.Element("level")?.Value.ToInt() ?? 0,
                Text = element.Element("text")?.Value,
                Order = element.Element("order")?.Value.ToInt() ?? 0,
                ParentId = element.Element("parent")?.Value,
                PageType = pageType,
                SectionId = element.Element("section")?.Value
            };
        }

        private List<SlickplanElement> SyncSectionElements(List<SlickplanSection> sections)
        {
            var results = new List<SlickplanElement>();

            var rootSection = sections.SingleOrDefault(x => x.SectionId == "svgmainsection");

            if (rootSection == null)
            {
                throw new DataImportException("Could not find section element id 'svgmainsection'");
            }

            results.AddRange(rootSection.Elements);

            foreach (var rootSectionElement in rootSection.Elements)
            {
                if (rootSectionElement.SectionId.IsNullOrWhiteSpace())
                {
                    continue;
                }

                var section = sections.FirstOrDefault(x => x.SectionId == rootSectionElement.SectionId);

                if (section == null)
                {
                    throw new DataImportException($"Could not find section element id '{rootSectionElement.SectionId}'");
                }

                var childElements = section.Elements.Where(x => x.Level != 0).ToList();

                childElements.Where(x => x.Level == 1).Each(x => x.ParentId = rootSectionElement.Id);
                childElements.Each(x =>
                {
                    x.Level += rootSectionElement.Level;
                    x.IsChildSection = true;
                });

                results.AddRange(childElements);
                
            }

            return results;
        }

        private void PopulateCells(List<SlickplanElement> allRawCells, SlickplanItem parentSlickplanCell)
        {
            var isRoot = parentSlickplanCell.Level == 0;
            var parentCellId = parentSlickplanCell.Id;
            var rawCells = allRawCells.Where(x => x.ParentId == parentCellId || (isRoot && x.Level == 1)).Where(x => x.PageType != "Do Not Import").ToList();
            var childCells = rawCells.Where(x => !x.IsChildSection).Select(rawCell => new SlickplanItem(rawCell, parentSlickplanCell)).ToList();
            var contentCells = rawCells.Where(x => x.IsChildSection).Select(rawCell => new SlickplanItem(rawCell, parentSlickplanCell)).ToList();

            parentSlickplanCell.Children.AddRange(childCells);
            parentSlickplanCell.Contents.AddRange(contentCells);

            childCells.Each(cell => PopulateCells(allRawCells, cell));
            contentCells.Each(cell => PopulateCells(allRawCells, cell));
        }
    }
}