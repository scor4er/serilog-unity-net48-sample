using Microsoft.Extensions.Logging;

namespace LoggingSample.Blah
{
    public class BlahService : IBlahService
    {
        private readonly ILogger _logger;

        public BlahService(ILogger logger)
        {
            _logger = logger;
        }

        public void Blah()
        {
            _logger.LogInformation("Blah called");
        }
    }
}