using System;
using LoggingSample.Blah;
using LoggingSample.Messaging;
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
            Container.RegisterType<ISimpleMessageService, SimpleMessageService>();
            RegisterLogging();

            var blahService = Container.Resolve<IBlahService>();
            blahService.Blah();

            var simpleMessageService = Container.Resolve<ISimpleMessageService>();
            simpleMessageService.Message();

            Console.ReadKey();
        }

        private static void RegisterLogging()
        {
            var loggerFactory = new LoggerFactory();
            var loggerConfig = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(Matching.FromSource("LoggingSample.Blah"))
                    .WriteTo.Console(
                        outputTemplate:
                        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{SourceContext}] {Message}{NewLine}{Exception}"))
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(Matching.FromSource("LoggingSample.Messaging"))
                    .WriteTo.Console(outputTemplate: "{Message}"))
                .CreateLogger();

            loggerFactory.AddSerilog(loggerConfig);

            Container.AddExtension(new LoggingExtension(loggerFactory));
        }
    }
}