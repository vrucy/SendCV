using SendCV.Services;
using SendCV.Unity;
using System.Windows;
using Unity;

namespace SendCV
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IUnityContainer _container;
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzQ0MjkwQDMxMzgyZTMzMmUzMG1WZUJEckZHWXYvN1ZEZDdyNDFHRVRibVBhM0N2ODlKSUlnU0JJc0tza2s9");
            var emailService = UnityConfig.Container.Resolve<EmailService>();
            //string text = File.ReadAllText("D://proba.txt");
            //text = text.Replace("test", "novo");
            //File.WriteAllText("D://test.txt", text);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //UnityConfig.Container.Resolve<AppConfig>().InitConfig();
            StartMainWindow();
            base.OnStartup(e);
        }
        private void StartMainWindow()
        {
            var mainWindow = UnityConfig.Container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
