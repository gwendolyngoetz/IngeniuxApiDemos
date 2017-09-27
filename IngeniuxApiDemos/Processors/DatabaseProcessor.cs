using IngeniuxApiDemos.Common.Configuration;
using IngeniuxApiDemos.Mapping;
using IngeniuxApiDemos.Source.Database;
using IngeniuxApiDemos.Target.IngeniuxCms;

namespace IngeniuxApiDemos.Processors
{
    public class DatabaseProcessor : Processor
    {
        protected override void RunInternal(IConfiguration config)
        {
            var sourceEndpoint = new DatabaseSourceEndpoint(config);
            var sourceResult = sourceEndpoint.Execute();

            var mapper = new DatabaseToIngeniuxCmsMapper(config);
            var mapperResult = mapper.Execute(sourceResult);

            var targetEndpoint = new IngeniuxTargetEndpoint(config);
            targetEndpoint.Execute(mapperResult);
        }
    }
}