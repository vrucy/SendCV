using SendCV.Command;
using SendCV.Interface;
using SendCV.Models;
using SendCV.Services;
using SendCV.Views;
using System.Collections.ObjectModel;
using System.Printing;
using System.Windows;
using System.Windows.Input;
using Unity;

namespace SendCV.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ICommand _NavigateToAddCompany;
        private ICommand _NavigateToTable;
        private IUnityContainer _container;
        private IEmailService _emailService;

        public MainWindowViewModel(IUnityContainer container, IEmailService emailService)
        {
            tabcollection = new ObservableCollection<TabModel>();
            _container = container;
            _emailService = emailService;
            Collection();
        }

        public ICommand NavigateToAddCompanyCommand
        {
            get
            {
                if (_NavigateToAddCompany == null)
                {
                    _NavigateToAddCompany = new RelayCommand(NavigateToAddCompany);
                }
                return _NavigateToAddCompany;
            }
        }
        public void NavigateToAddCompany(object x)
        {
            var addCompany = _container.Resolve<AddCompany>();
            addCompany.Show();
        }

        public ICommand NavigateToTableCommand
        {
            get
            {
                if (_NavigateToTable == null)
                {
                    _NavigateToTable = new RelayCommand(NavigateToTable);
                }
                return _NavigateToTable;
            }
        }
        public void NavigateToTable(object x)
        {
            var y = _container.Resolve<FileWriter>();
            //_emailService.SendEmail("test",false,"Test");
            //y.WriteDocuments("Test");
            //_navigationService.NavigateToTableOrders();
        }
        public ICommand _SaveCommand { get; private set; }
        private ObservableCollection<TabModel> _tabcollection;
        public ObservableCollection<TabModel> tabcollection
        {
            get
            {
                return _tabcollection;
            }
            set
            {
                _tabcollection = value;
            }
        }


        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new RelayCommand(Save/*, x => CanSubmit()*/);
                }
                return _SaveCommand;
            }
        }
        private void Save(object x)
        {

        }
        private void Collection()
        {
            TabModel model = new TabModel()
            {
                HeaderName = "Add new company"
            };
            TabModel model1 = new TabModel()
            {
                HeaderName = "View table"
            };

            tabcollection.Add(model);
            tabcollection.Add(model1);
        }
    }
}
