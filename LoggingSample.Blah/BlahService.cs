using Microsoft.Extensions.Logging;

namespace LoggingSample.Blah
{
    public class BlahService : IBlahService
    {
        private readonly ILogger<BlahService> _logger;

        public BlahService(ILogger<BlahService> logger)
        {
            _logger = logger;
        }

        public void Blah()
        {
            _logger.LogInformation("Blah called");
        }
    }
}