using System;
using Blah;
using Messaging;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Filters;
using Unity;
using Unity.Microsoft.Logging;

namespace LoggingSample
{
    internal class Program
    {
        private static readonly IUnityContainer Container = new UnityContainer();

        private static void Main(string[] args)
        {
            Container.RegisterType<IBlahService, BlahService>();
            Container.RegisterType<ISimpleMessageLogService, SimpleMessageLogService>();
            RegisterLogging();

            var blahService = Container.Resolve<IBlahService>();
            blahService.Blah();

            var simpleMessageService = Container.Resolve<ISimpleMessageLogService>();
            simpleMessageService.Message();

            Console.ReadKey();
        }

        private static void RegisterLogging()
        {
            var loggerFactory = new LoggerFactory();
            var loggerConfig = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(Matching.FromSource("Blah"))
                    .WriteTo.Console(
                        outputTemplate:
                        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{SourceContext}] {Message}{NewLine}{Exception}"))
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(Matching.FromSource("Messaging"))
                    .WriteTo.Console(outputTemplate: "{Message}"))
                .CreateLogger();

            loggerFactory.AddSerilog(loggerConfig);

            Container.AddExtension(new LoggingExtension(loggerFactory));
        }
    }
}

namespace Blah
{
    public interface IBlahService
    {
        void Blah();
    }

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

namespace Messaging
{
    public interface ISimpleMessageLogService
    {
        void Message();
    }

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