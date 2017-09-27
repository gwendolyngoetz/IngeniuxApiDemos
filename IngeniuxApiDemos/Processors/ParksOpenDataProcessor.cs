using IngeniuxApiDemos.Common.Configuration;
using IngeniuxApiDemos.Mapping;
using IngeniuxApiDemos.Source.ParksOpenData;
using IngeniuxApiDemos.Target.IngeniuxCms;

namespace IngeniuxApiDemos.Processors
{
    public class ParksOpenDataProcessor : Processor
    {
        protected override void RunInternal(IConfiguration config)
        {
            var sourceEndpoint = new ParksOpenDataSourceEndpoint(config);
            var sourceResult = sourceEndpoint.Execute();

            var mapper = new ParksOpenDataToIngeniuxCmsMapper(config);
            var mapperResult = mapper.Execute(sourceResult);

            var targetEndpoint = new IngeniuxTargetEndpoint(config);
            targetEndpoint.Execute(mapperResult);
        }
    }
}