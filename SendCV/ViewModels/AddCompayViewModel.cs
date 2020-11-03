using SendCV.Command;
using SendCV.Context;
using SendCV.Interface;
using SendCV.Models;
using Syncfusion.Data.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Input;
using Unity;
using static SendCV.Services.FileReader;

namespace SendCV.ViewModels
{
    public class AddCompayViewModel : BaseViewModel
    {
        ObservableCollection<CompanyCredentials> _companies;
        private IEmailService _emailService;
        private IUnityContainer _container;
        CompanyCredentials company;
        public AddCompayViewModel(IEmailService emailService)
        {
            _companies = new ObservableCollection<CompanyCredentials>();
            company = new CompanyCredentials();
            _emailService = emailService;
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
        private CompanyCredentials _company;

        public CompanyCredentials Company
        {
            get { return _company; }
            set { _company = value; }
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
        
        //TODO: not good navigation must fix
        public void NavigateBack(object x)
        {
            var mainWindow = _container.Resolve<MainWindow>();
            //var mainWindow = new MainWindow();
            mainWindow.Show();
        }
        
    }
    public class NotifyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        private void Handle(object sender, PropertyChangedEventArgs args)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, null));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (object t in e.NewItems)
                {
                    ((T)t).PropertyChanged += Handle;
                }
            }
            if (e.OldItems != null)
            {
                foreach (object t in e.OldItems)
                {
                    ((T)t).PropertyChanged -= Handle;
                }
            }
            base.OnCollectionChanged(e);
        }
    }
}
