using SendCV.Command;
using SendCV.Context;
using SendCV.Interface;
using SendCV.Models;
using Syncfusion.Data.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using Unity;

namespace SendCV.ViewModels
{
    public class AddCompayViewModel :BaseViewModel
    {
        ObservableCollection<CompanyCredentials> _companies;
        ObservableCollection<CompanyCredentials>_selectedCompany ;
        private IEmailService _emailService;
        private IUnityContainer _container;
        CompanyCredentials company;
        public AddCompayViewModel(IEmailService emailService)
        {
            _companies = new ObservableCollection<CompanyCredentials>();
            company = new CompanyCredentials();
            _selectedCompany = new ObservableCollection<CompanyCredentials>();
            _emailService = emailService;
        }
        public ObservableCollection<CompanyCredentials> Companies
        {
            get { return _companies; }
            set { _companies = value; OnPropertyChanged("Companies"); }
        }
        public ObservableCollection<CompanyCredentials> SelectedCompanies
        {
            get { return _selectedCompany; }
            set { _selectedCompany = value; OnPropertyChanged("SelectedCompanies"); }
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
            _companies.Add(company);
            OnPropertyChanged("Companies");
        }
        public void DeleteCompany(object x)
        {
            var selectedCompany = SelectedCompanies;
            var companyToRemove = Companies.Where(c => c.Selected);
            companyToRemove.ForEach(item => Companies.Remove(item));
            OnPropertyChanged("Companies");
        }
        private ObservableCollection<object> _selectedItems = new ObservableCollection<object>();
        public ObservableCollection<object> SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;
                OnPropertyChanged("SelectedItems");
            }
        }

        public string CompanyName
        {
            get{ return company.Name; }
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
        //TODO: not good navigation must fix
        public void NavigateBack(object x)
        {
            var mainWindow = _container.Resolve<MainWindow>();
            //var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
