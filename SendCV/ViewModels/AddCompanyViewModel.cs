using SendCV.Command;
using SendCV.Context;
using SendCV.Enums;
using SendCV.Interface;
using SendCV.Models;
using SendCV.Repo;
using SendCV.Services;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace SendCV.ViewModels
{
    public class AddCompanyViewModel : BaseViewModel, IDataErrorInfo
    {
        private readonly IEmailService _emailService;
        private IUnityContainer _container;
        private readonly ICompanyRepo _companyRepo;
        private readonly FileWriter _fileWriter;
        ObservableCollection<CompanyCredentials> _companies;
        CompanyCredentials company;

        public AddCompanyViewModel()
        {
            _companies = new ObservableCollection<CompanyCredentials>();
            company = new CompanyCredentials();
            company.CompanyAddress = new CompanyAddress();
            _container = new UnityContainer();
            _fileWriter = _container.Resolve<FileWriter>();

            _emailService = _container.Resolve<EmailService>();
            _companyRepo = _container.Resolve<CompanyRepo>();
        }

        #region Command
        private ICommand _NavigateBack;
        private ICommand _AddCompany;
        private ICommand _DeleteCompany;
        public ICommand DeleteCompanyCommand
        {
            get
            {
                if (_DeleteCompany == null)
                {
                    _DeleteCompany = new RelayCommand(DeleteCompany);
                }
                return _DeleteCompany;
            }
        }
        private ICommand _SendMail;
        public ICommand SendMailCommand
        {
            get
            {
                if (_SendMail == null)
                {
                    _SendMail = new RelayCommand(SendMail);
                }
                return _SendMail;
            }
        }

        public ICommand NavigateBackCommand
        {
            get
            {
                if (_NavigateBack == null)
                {
                    _NavigateBack = new RelayCommand(NavigateBack);
                }
                return _NavigateBack;
            }
        }
        public ICommand AddCompanyCommand
        {
            get
            {
                if (_AddCompany == null)
                {
                    _AddCompany = new RelayCommand(AddCompany);
                }
                return _AddCompany;
            }
        }
        public void NavigateBack(object x)
        {
            var mainWindow = _container.Resolve<MainWindow>();
            //var mainWindow = new MainWindow();
            mainWindow.Show();
        }
        public void AddCompany(object x)
        {
            company.SelectedTypeEmail = SelectedTypeEmail;
            _companies.Add(company);
            company = new CompanyCredentials();
            company.CompanyAddress = new CompanyAddress();
            SelectedTypeEmail = null;
            OnPropertyChanged("SelectedTypeEmail");
            OnPropertyChanged("CompanyName");
            OnPropertyChanged("CompanyCity");
            OnPropertyChanged("CompanyEmail");
            OnPropertyChanged("CompanyAddress");
            OnPropertyChanged("CompanyCountry");
            OnPropertyChanged("CompanyNameHR");
            OnPropertyChanged("Companies");
        }
        public void DeleteCompany(object x)
        {
            var companyToRemove = Companies.Where(c => c.Selected).ToList();
            companyToRemove.ForEach(item => Companies.Remove(item));
            OnPropertyChanged("Companies");
        }

        private async void SendMail(object x)
        {
            var companyToSend = Companies.Where(c => c.Selected).ToList();

            foreach (var item in companyToSend)
            {
                var sendAtt = item.SelectedTypeEmail.Equals("OnlyEmail") ? false : true;
                _fileWriter.WriteDocuments(item, sendAtt);
                await _emailService.SendEmail(item, sendAtt);
                Companies.Remove(item);
                await _companyRepo.SaveCompany(item);
                //await Task.Run(async () =>
                //{
                //});
            }
            OnPropertyChanged("Companies");

        }
        #endregion


        #region Prop
        public string CompanyName
        {
            get
            {
                return company.Name;
            }
            set
            {
                company.Name = value;
                OnPropertyChanged("CompanyName");
            }
        }

        public string CompanyEmail
        {
            get { return company.Email; }
            set { company.Email = value; OnPropertyChanged("CompanyEmail"); }
        }
        public string CompanyAddress
        {
            get { return company.CompanyAddress.Address; }
            set { company.CompanyAddress.Address = value; OnPropertyChanged("CompanyAddress"); }
        }
        public string CompanyCountry
        {
            get { return company.CompanyAddress.Country; }
            set { company.CompanyAddress.Country = value; OnPropertyChanged("CompanyCountry"); }
        }
        public string CompanyNameHR
        {
            get { return company.NameHR; }
            set { company.NameHR = value; OnPropertyChanged("CompanyNameHR"); }
        }
        private string _selectedMyEnumType;
        public string SelectedTypeEmail
        {
            get { return _selectedMyEnumType; }
            set
            {
                _selectedMyEnumType = value;
                OnPropertyChanged("SelectedTypeEmail");
            }
        }

        public string CompanyCity
        {
            get { return company.CompanyAddress.City; }
            set { company.CompanyAddress.City = value; OnPropertyChanged("CompanyCity"); }
        }

        private string _error;
        public string Error
        {
            get => _error;

            set
            {
                if (_error != value)
                {

                    _error = value;
                    OnPropertyChanged("Error");
                }
            }
        }

        public IEnumerable<TypeEmail> TypesForSendEmail
        {
            get
            {
                return Enum.GetValues(typeof(TypeEmail)).Cast<TypeEmail>();
            }
        }

        public ObservableCollection<CompanyCredentials> Companies
        {
            get { return _companies; }
            set
            {
                _companies = value;

                OnPropertyChanged("Companies");
            }
        }
        #endregion

        #region DataError
        Dictionary<string, string> dicError = new Dictionary<string, string>();
        Dictionary<string, bool> dicErrorSend = new Dictionary<string, bool>();

        public string this[string columnName]
        {
            get
            {
                if (!String.IsNullOrEmpty(OnValidate(columnName)) && !dicError.ContainsKey(columnName))
                {
                    dicError.Add(columnName, OnValidate(columnName));
                }
                else if (String.IsNullOrEmpty(OnValidate(columnName)))
                {
                    dicError.Remove(columnName);
                }


                return OnValidate(columnName);
            }
        }
        private string ReplaceError(string key, string error)
        {
            if (dicError.ContainsKey(key))
            {
                dicError[key] = error;
            }
            return error;
        }
        private string OnValidate(string columnName)
        {
            string result = string.Empty;
            var duplicateCompany = _companyRepo.GetCompanyByLastDate(company.Name);

            if (columnName == "CompanyName")
            {
                if (string.IsNullOrEmpty(CompanyName))
                {
                    result = "Name is mandatory";
                }
                if (duplicateCompany != null)
                {
                    result = ReplaceError("CompanyName", $"You send company last email on: {duplicateCompany.DateEmailSend}");
                }

            }

            if (columnName == "CompanyEmail")
            {
                if (string.IsNullOrEmpty(CompanyEmail))
                {
                    result = "Email Required";
                }
                else if (!Regex.IsMatch(CompanyEmail, @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{1,4}"))
                {
                    result = "Invalid Email ID";
                }
            }
            if (columnName == "SelectedTypeEmail")
            {
                if (string.IsNullOrEmpty(SelectedTypeEmail))
                {
                    result = "Combobox is required";
                }
            }

            if (dicError.Count == 0 || (dicError.Count == 1 && IsExistNameInDb()))
            {
                Error = null;
            }
            else
            {
                Error = "Error";
            }

            return result;
        }
        private bool IsExistNameInDb()
        {
            string error = "";
            if (dicError.ContainsKey("CompanyName"))
            {
                dicError.TryGetValue("CompanyName", out error);
                if (error.StartsWith("You send company last email on"))
                {
                    return true;
                }

            }
            return false;
        }
        #endregion
    }
}
