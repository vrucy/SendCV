using SendCV.Command;
using SendCV.Interface;
using SendCV.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows.Input;
using Unity;

namespace SendCV.ViewModels
{
    public class EmailCredentialsViewModel: BaseViewModel
    {
        private IUnityContainer _container;
        private ICommand _SetCredentials;

        public EmailCredentialsViewModel()
        {
            _container = new UnityContainer();
        }
        private string userEmail;

        public string UserEmail
        {
            get 
            {
                
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["UserNameEmail"]) && userEmail == null)
                {
                    userEmail = ConfigurationManager.AppSettings["UserNameEmail"];
                }
                    return userEmail; 
                
                
            }
            set 
            {
                userEmail = value;
                OnPropertyChanged("UserEmail");
            }
        }
        private string passEmail;

        public string PassEmail
        {
            get { return passEmail; }
            set
            {
                passEmail = value;
                OnPropertyChanged("PassEmail");
            }
        }
        public ICommand SetCredentialsCommand
        {
            get
            {
                if (_SetCredentials == null)
                {
                    _SetCredentials = new RelayCommand(SetCredentials);
                }
                return _SetCredentials;
            }
        }
        public void SetCredentials(object x)
        {
            SetCredentialInConfigFile(UserEmail, CryptoHelper.Crypto.EncryptStringAES(PassEmail));
            UserEmail = null;
            PassEmail = null;
            OnPropertyChanged("UserEmail");
            OnPropertyChanged("PassEmail");
        }
        private void SetCredentialInConfigFile(string userName, string encrytPass)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["UserNameEmail"].Value = userName;
            config.AppSettings.Settings["PassEmail"].Value = encrytPass;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
