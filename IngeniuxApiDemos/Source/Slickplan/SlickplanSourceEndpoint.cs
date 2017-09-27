using System.Collections.Generic;
using System.Diagnostics;
using IngeniuxApiDemos.Common.Configuration;
using IngeniuxApiDemos.Source.Slickplan.Model;
using IngeniuxApiDemos.Source.Slickplan.XmlDeserialization;
using Newtonsoft.Json;
using NLog;

namespace IngeniuxApiDemos.Source.Slickplan
{
    public class SlickplanSourceEndpoint : SourceEndpoint<IEnumerable<SlickplanItem>>
    {
        public SlickplanSourceEndpoint(IConfiguration config) : base(config) { }
        
        protected override IEnumerable<SlickplanItem> ExecuteInternal()
        {
            var pageTypeDocument = System.IO.File.ReadAllText(_config.Source.Slickplan.PageTypeSchemaFileName);
            var pagesTypes = JsonConvert.DeserializeObject<IEnumerable<PageType>>(pageTypeDocument);

            var xmlDataSourceDocument = System.IO.File.ReadAllText(_config.Source.Slickplan.ImportFileName);
            var slickplanXmlDeserializer = new SlickplanXmlDeserializer(pagesTypes);

            var rootCell = slickplanXmlDeserializer.Deserialize(xmlDataSourceDocument);

            return new List<SlickplanItem>
            {
                rootCell
            };
        }
    }
}
