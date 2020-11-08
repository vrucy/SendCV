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
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzQ0MjkwQDMxMzgyZTMzMmUzMG1WZUJEckZHWXYvN1ZEZDdyNDFHRVRibVBhM0N2ODlKSUlnU0JJc0tza2s9");
        }
        protected override void OnStartup(StartupEventArgs e)
        {
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
