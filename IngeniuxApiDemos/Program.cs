using System;
using IngeniuxApiDemos.Common;
using IngeniuxApiDemos.Common.Configuration;
using IngeniuxApiDemos.Common.Exceptions;
using IngeniuxApiDemos.Processors;
using NLog;

namespace IngeniuxApiDemos
{
    public class Program
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            if (_logger == null)
            {
                throw new DataImportException("Could not start logging subsystem. Check NLog.config file settings.");
            }
            try
            {
                var config = GetConfig(args);
               
                ProcessorRunner.Run(config);
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex);
            }
        }

        private static IConfiguration GetConfig(string[] args)
        {
            var argumentParser = new ArgumentParser(args);
            var configFilePath = argumentParser.GetArgumentValue("configFilePath");
            
            _logger.Info($"Config settings: configFilePath={configFilePath}");

            return Configuration.Create(configFilePath);
        }
    }
}