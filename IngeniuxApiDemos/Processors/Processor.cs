using System;
using IngeniuxApiDemos.Common;
using IngeniuxApiDemos.Common.Configuration;
using IngeniuxApiDemos.Common.Exceptions;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Validation;
using NLog;

namespace IngeniuxApiDemos.Processors
{
    public interface IProcessor
    {
        void Run(IConfiguration config);
    }

    public abstract class Processor : IProcessor
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public void Run(IConfiguration config)
        {
            try
            {
                _logger.Info("Start - Processing");
                RunInternal(config);
            }
            catch (ValidationException ex)
            {
                _logger.Fatal(ex, ex.ValidationResults.ToString());
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);
            }

            _logger.Info("End   - Processing");
        }

        protected abstract void RunInternal(IConfiguration config);
    }
}
