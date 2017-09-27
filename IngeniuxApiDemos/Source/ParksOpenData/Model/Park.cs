using Newtonsoft.Json;

namespace IngeniuxApiDemos.Source.ParksOpenData.Model
{
    public class Park
    {
        [JsonProperty("common_name")]
        public string CommonName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("website")]
        public WebSite Website { get; set; } = new WebSite();

        public string Detail { get; set; }
    }

    public class WebSite
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
