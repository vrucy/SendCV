using SendCV.Interface;
using SendCV.Services;
using Unity;

namespace SendCV.Unity
{
    internal class Bootstrapper
    {
        public static void Initialize(IUnityContainer container)
        {
            container.RegisterType<IEmailService,EmailService>();
        }
    }
}
