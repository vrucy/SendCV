using Serilog;
using System;
using System.IO;
using System.Reflection;
using Unity;

namespace SendCV.Unity
{
    public static class UnityConfig
    {
        public static IUnityContainer Container => _container.Value;


        private static readonly Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(CreateContainer);

        private static IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            InitLogger(container);
            Bootstrapper.Initialize(container);
            //ConfigureSerilogLogger(container);
        }
        private static void InitLogger(IUnityContainer container)
        {
            var newPath = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "Prod_log.txt");
            ILogger log = new LoggerConfiguration()
                    .WriteTo.File(newPath, rollingInterval: RollingInterval.Year)
                    .CreateLogger();

            Log.Logger = log;
        }
    }
}
