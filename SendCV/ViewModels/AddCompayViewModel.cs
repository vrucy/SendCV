using SendCV.Command;
using SendCV.Context;
using SendCV.Enums;
using SendCV.Interface;
using SendCV.Models;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using static SendCV.Services.FileReader;

namespace SendCV.ViewModels
{
    public class AddCompayViewModel : BaseViewModel
    {
        private IEmailService _emailService;
        private IUnityContainer _container;
        public AddCompayViewModel(IEmailService emailService)
        {
            _companies = new ObservableCollection<CompanyCredentials>();
            company = new CompanyCredentials();
            _emailService = emailService;
        }
        
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
        

        public void AddCompany(object x)
        {
            //company.Selected = true;
            _companies.Add(company);
            company = new CompanyCredentials();
            OnPropertyChanged("Companies");
        }
        public void DeleteCompany(object x)
        {
            var companyToRemove = Companies.Where(c => c.Selected).ToList();
            companyToRemove.ForEach(item => Companies.Remove(item));
            OnPropertyChanged("Companies");
        }
        private void SendMail(object x)
        {
            var companyToSend = Companies.Where(c => c.Selected).ToList();
            companyToSend.ForEach(c => _emailService.SendEmail(c.Email,true,c.Name));

            OnPropertyChanged("Companies");
        }
        public string CompanyName
        {
            get { return company.Name; }
            set { company.Name = value; OnPropertyChanged("CompanyName"); }
        }

        public string CompanyEmail
        {
            get { return company.Email; }
            set { company.Email = value; OnPropertyChanged("CompanyEmail"); }
        }
        public string CompanyAddress
        {
            get { return company.Address; }
            set { company.Address = value; OnPropertyChanged("CompanyAddress"); }
        }
        public string CompanyCountry
        {
            get { return company.Country; }
            set { company.Country = value; OnPropertyChanged("CompanyCountry"); }
        }
        public string CompanyNameHR
        {
            get { return company.NameHR; }
            set { company.NameHR = value; OnPropertyChanged("CompanyNameHR"); }
        }
        private string _selectedMyEnumType;
        public string SelectedMyEnumType
        {
            get { return _selectedMyEnumType; }
            set
            {
                _selectedMyEnumType = value;
                OnPropertyChanged("SelectedMyEnumType");
            }
        }

        public IEnumerable<TypeEmail> MyEnumTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(TypeEmail)).Cast<TypeEmail>();
            }
        }
        CompanyCredentials company;
        ObservableCollection<CompanyCredentials> _companies;

        public ObservableCollection<CompanyCredentials> Companies
        {
            get { return _companies; }
            set
            {
                _companies = value;

                OnPropertyChanged("Companies");
            }
        }
        //TODO: not good navigation must fix
        public void NavigateBack(object x)
        {
            var mainWindow = _container.Resolve<MainWindow>();
            //var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
