using System.Diagnostics;
using IngeniuxApiDemos.Common.Configuration;
using NLog;

namespace IngeniuxApiDemos.Source
{
    public abstract class SourceEndpoint<T>
    {
        protected readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        protected readonly IConfiguration _config;

        protected SourceEndpoint(IConfiguration config)
        {
            _config = config;
        }

        public T Execute()
        {
            Stopwatch stopwatch = null;
            var fullName = GetType().FullName;

            try
            {
                _logger.Info($"Start - {fullName}");
                stopwatch = Stopwatch.StartNew();
                return ExecuteInternal();
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

        protected abstract T ExecuteInternal();
    }
}