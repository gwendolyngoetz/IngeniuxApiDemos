using System.Diagnostics;
using System.Text.RegularExpressions;
using IngeniuxApiDemos.Common.Configuration;
using NLog;

namespace IngeniuxApiDemos.Mapping
{
    public abstract class Mapper<TSource, TTarget>
    {
        protected readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        protected readonly IConfiguration _config;

        protected Mapper(IConfiguration config)
        {
            _config = config;
        }

        public TTarget Execute(TSource sourcePayload)
        {
            Stopwatch stopwatch = null;
            var fullName = GetType().FullName;

            try
            {
                _logger.Info($"Start - {fullName}");
                stopwatch = Stopwatch.StartNew();
                return ExecuteInternal(sourcePayload);
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

        protected abstract TTarget ExecuteInternal(TSource sourcePayload);

        protected string CleanMonikerText(string name)
        {
            return Regex.Replace(name, "[^a-zA-Z0-9_ ]", string.Empty);
        }
    }
}