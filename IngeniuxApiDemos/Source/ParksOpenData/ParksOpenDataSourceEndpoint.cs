using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using IngeniuxApiDemos.Common.Configuration;
using IngeniuxApiDemos.Source.ParksOpenData.Model;
using Newtonsoft.Json;

namespace IngeniuxApiDemos.Source.ParksOpenData
{
    public class ParksOpenDataSourceEndpoint : SourceEndpoint<IEnumerable<Park>>
    {
        public ParksOpenDataSourceEndpoint(IConfiguration config) : base(config) { }

        protected override IEnumerable<Park> ExecuteInternal()
        {
            var endpoint = _config.Source.ParksOpenData.Endpoint;
            var webClient = new System.Net.WebClient();
            var raw = webClient.DownloadData(endpoint);
            var json = System.Text.Encoding.UTF8.GetString(raw);

            var parks = JsonConvert.DeserializeObject<List<Park>>(json);
            return parks;
        }
    }
}
