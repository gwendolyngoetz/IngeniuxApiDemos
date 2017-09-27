using IngeniuxApiDemos.Common.Configuration;
using IngeniuxApiDemos.Mapping;
using IngeniuxApiDemos.Source.Slickplan;
using IngeniuxApiDemos.Target.IngeniuxCms;

namespace IngeniuxApiDemos.Processors
{
    public class SlickplanProcessor : Processor
    {
        protected override void RunInternal(IConfiguration config)
        {
            var sourceEndpoint = new SlickplanSourceEndpoint(config);
            var sourceResult = sourceEndpoint.Execute();

            var mapper = new SlickplanToIngeniuxCmsMapper(config);
            var mapperResult = mapper.Execute(sourceResult);

            var targetEndpoint = new IngeniuxTargetEndpoint(config);
            targetEndpoint.Execute(mapperResult);
        }
    }
}