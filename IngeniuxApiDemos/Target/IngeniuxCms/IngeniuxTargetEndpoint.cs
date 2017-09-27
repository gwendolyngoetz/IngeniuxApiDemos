using System.Collections.Generic;
using System.Diagnostics;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Pages;
using IngeniuxApiDemos.Target.IngeniuxCms.Repository;
using NLog;
using IConfiguration = IngeniuxApiDemos.Common.Configuration.IConfiguration;

namespace IngeniuxApiDemos.Target.IngeniuxCms
{
    public class IngeniuxTargetEndpoint
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly PageGenerator _pageGenerator;

        public IngeniuxTargetEndpoint(IConfiguration config)
        {
            var contentStore = new ContentStoreRepository(config);
            _pageGenerator = new PageGenerator(contentStore, config);
        }

        public void Execute(IEnumerable<Page> pages)
        {
            Stopwatch stopwatch = null;
            var fullName = GetType().FullName;

            try
            {
                _logger.Info($"Start - {fullName}");
                stopwatch = Stopwatch.StartNew();
                GeneratePages(pages);
            }
            catch
            {
                _logger.Error($"Error - {fullName}");
                throw;
            }
            finally
            {
                stopwatch?.Stop();
                _logger.Info($"End   - {fullName} (Elapsed {stopwatch?.ElapsedMilliseconds}ms)");
            }
        }

        private void GeneratePages(IEnumerable<Page> pages)
        {
            _pageGenerator.Execute(pages);
        }
    }
}