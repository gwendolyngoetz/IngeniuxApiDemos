using IngeniuxApiDemos.Processors;
using Newtonsoft.Json;

namespace IngeniuxApiDemos.Common.Configuration
{
    public interface IConfiguration
    {
        ProcessorType ProcessorType { get; set; }
        TargetConfig Target { get; }
        SourceConfig Source { get; }
    }

    public class Configuration : IConfiguration
    {
        public ProcessorType ProcessorType { get; set; }
        public TargetConfig Target { get; set; } = new TargetConfig();
        public SourceConfig Source { get; set; } = new SourceConfig();

        public static Configuration Create(string configFilePath)
        {
            var configurationText = System.IO.File.ReadAllText(configFilePath);
            return JsonConvert.DeserializeObject<Configuration>(configurationText);
        }
    }

    public class TargetConfig
    {
        public IngeniuxConnection Ingeniux { get; set; } = new IngeniuxConnection();
    }

    public class SourceConfig
    {
        public SlickplanConnection Slickplan { get; set; } = new SlickplanConnection();
        public ParksOpenDataConnection ParksOpenData { get; set; } = new ParksOpenDataConnection();
        public DatabaseConnection Database { get; set; } = new DatabaseConnection();
    }

    public class IngeniuxConnection
    {
        public string ContentStoreUri { get; set; }
        public string ContentStoreXmlPath { get; set; }
        public string UserId { get; set; }
        public string RootPageFolderId { get; set; }
    }

    public class SlickplanConnection
    {
        public string ImportFileName { get; set; }
        public string PageTypeSchemaFileName { get; set; }
    }

    public class ParksOpenDataConnection
    {
        public string Endpoint { get; set; }
    }

    public class DatabaseConnection
    {
        public string ConnectionString { get; set; }
    }
}
