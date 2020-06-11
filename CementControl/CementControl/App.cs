using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Serilog;
using Serilog.Events;

namespace CementControl
{
    public class App
    {
        private static IContainer Container { get; set; }

        public static void ConfigureApp()
        {
            ConfigureDependencyInjection();
            ConfigureLogging();
        }


        public static void ConfigureDependencyInjection()
        {
            var builder = new ContainerBuilder();

            // Register individual components
            builder.RegisterType<SerialDevice>().As<ISerialDevice>();
            //builder.RegisterType<TaskController>();
            //builder.Register(c => new LogManager(DateTime.Now)).As<ILogger>();

            // Scan an assembly for components
            //builder.RegisterAssemblyTypes(myAssembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();

            Container = builder.Build();
        }



        private static void ConfigureLogging()
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
