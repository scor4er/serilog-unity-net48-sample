using Microsoft.Extensions.Logging;

namespace LoggingSample.Messaging
{
    public class SimpleMessageService : ISimpleMessageService
    {
        private readonly ILogger _logger;

        public SimpleMessageService(ILogger logger)
        {
            _logger = logger;
        }

        public void Message()
        {
            _logger.LogInformation("Just a message");
        }
    }
}