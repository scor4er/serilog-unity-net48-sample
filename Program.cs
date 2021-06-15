using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace ConsoleApp21
{
    class Program
    {text-logging-unity-full-framewo
        private static readonly IUnityContainer Container = new UnityContainer();

        static void Main(string[] args)
        {
            Container.RegisterType<IBlahService, BlahService>();



            // instantiate and configure logging. Using serilog here, to log to console and a text-file.
            var loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
            var loggerConfig = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{SourceContext}] {Message}{NewLine}{Exception}")
                    .CreateLogger();
            loggerFactory.AddSerilog(loggerConfig);




            Container.RegisterFactory<Microsoft.ExtILogger>(factory =>
            {
                ILogger log = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{SourceContext}] {Message}{NewLine}{Exception}")
                    .CreateLogger();
                return log;
            }, new ContainerControlledLifetimeManager());

            var blahService = Container.Resolve<IBlahService>();
            blahService.BlahMethod();


            Console.ReadKey();
        }
    }

    public class BlahService : IBlahService
    {
        private readonly ILogger _logger;

        public BlahService(ILogger logger)
        {
            _logger = logger.ForContext<BlahService>();
        }

        public void BlahMethod()
        {
            _logger.Information("Blah method");
        }
    }

    public interface IBlahService
    {
        void BlahMethod();
    }
}
