using Messaging;
using Microsoft.Extensions.Logging;

namespace LoggingSample.Messaging
{
    public class SimpleMessageLogService : ISimpleMessageLogService
    {
        private readonly ILogger<SimpleMessageLogService> _logger;

        public SimpleMessageLogService(ILogger<SimpleMessageLogService> logger)
        {
            _logger = logger;
        }

        public void Message()
        {
            _logger.LogInformation("Just a message");
        }
    }
}