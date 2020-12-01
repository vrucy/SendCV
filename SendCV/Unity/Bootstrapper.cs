using SendCV.Context;
using SendCV.Interface;
using SendCV.Repo;
using SendCV.Services;
using Serilog;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace SendCV.Unity
{
    internal class Bootstrapper
    {
        public static void Initialize(IUnityContainer container)
        {
           
            container.RegisterType<IEmailService,EmailService>();
            container.RegisterType<ICompanyRepo, CompanyRepo>();
            container.RegisterType<IFileReader, FileReader>();
            container.RegisterType<SendCVContext>();
            container.RegisterType<FileWriter>();
            container.RegisterType<ILogger>(new ContainerControlledLifetimeManager(), new InjectionFactory((ctr, type, name) =>
            {
                return Log.Logger;
            }));
        }
    }
}
