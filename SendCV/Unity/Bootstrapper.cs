using SendCV.Context;
using SendCV.Interface;
using SendCV.Repo;
using SendCV.Services;
using Unity;

namespace SendCV.Unity
{
    internal class Bootstrapper
    {
        public static void Initialize(IUnityContainer container)
        {
            container.RegisterType<IEmailService,EmailService>();
            container.RegisterType<ICompanyRepo, CompanyRepo>();
            container.RegisterType<SendCVContext>();
        }
    }
}
