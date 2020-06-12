using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutofacSerilogIntegration;
using Serilog;
using Serilog.Events;

namespace CementControl
{
    public class App
    {



        public static IContainer ConfigureDependencyInjection()
        {
            var builder = new ContainerBuilder();

            // Register individual components
            builder.RegisterType<SerialConnection>().As<ISerialConnection>();
            builder.RegisterLogger(autowireProperties: true);
            //builder.RegisterType<TaskController>();
            //builder.Register(c => new LogManager(DateTime.Now)).As<ILogger>();

            // Scan an assembly for components
            //builder.RegisterAssemblyTypes(myAssembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();

            return builder.Build();
        }



        public static void ConfigureLogging()
        {
            var logLevel = LogEventLevel.Debug;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(logLevel)
                .Enrich.FromLogContext()
                .ReadFrom.AppSettings()
                .CreateLogger();
        }

    }
}
