using System;
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
            Bootstrapper.Initialize(container);
            //ConfigureSerilogLogger(container);
        }
    }
}
