using System;
using IngeniuxApiDemos.Common.Configuration;

namespace IngeniuxApiDemos.Processors
{
    public class ProcessorRunner
    {
        public static void Run(IConfiguration config)
        {
            IProcessor processor = null;

            switch (config.ProcessorType)
            {
                case ProcessorType.Slickplan:
                    processor = new SlickplanProcessor();
                    break;

                case ProcessorType.ParksOpenData:
                    processor = new ParksOpenDataProcessor();
                    break;

                case ProcessorType.Database:
                    processor = new DatabaseProcessor();
                    break;

                default:
                    throw new NotSupportedException();

            }

            processor.Run(config);
        }
    }
}