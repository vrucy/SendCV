using Microsoft.EntityFrameworkCore.Internal;
using SendCV.Command;
using SendCV.Interface;
using SendCV.Models;
using SendCV.Services;
using SendCV.Views;
using Syncfusion.Windows.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Input;
using Unity;
using Unity.Injection;

namespace SendCV.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ICommand _NavigateToAddCompany;
        private ICommand _NavigateToTable;
        private IUnityContainer _container;

        public MainWindowViewModel(IUnityContainer container)
        {
            _container = container;
            var x = new AddCompany();
            UpdateViewCommand = new UpdateViewCommand(this);
        }
        public ICommand UpdateViewCommand { get; set; }

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
            //addCompany.Show();
        }
        //mozda napraviti listu i cuvati?
        //private LinkedList<BaseViewModel> _selectedViewModel ;
        List<BaseViewModel> _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get 
            {
                if (_selectedViewModel == null)
                {
                    _selectedViewModel = new List<BaseViewModel>();
                    _selectedViewModel.Add(new AddCompanyViewModel());
                    //_selectedViewModel.AddFirst(new AddCompanyViewModel());
                }
                
                var x = _selectedViewModel.FirstOrDefault();
                return x; 
            }
            set
            {
                var y = value;
                //da li posoji model
                var x = _selectedViewModel.Any(y=>y.GetType() == typeof(AddCompanyViewModel));
                if (!x)
                {
                    //_selectedViewModel.AddFirst(value);
                    _selectedViewModel.Add(value);
                }
                else if (!_selectedViewModel.Any(y => y.GetType() == typeof(TableViewModel)))
                {
                    //_selectedViewModel.AddFirst(value);
                    _selectedViewModel.Add(value);
                    var itemIndex = _selectedViewModel.FindIndex(r => r.GetType() == value.GetType());
                    var item = _selectedViewModel[itemIndex];
                    _selectedViewModel[itemIndex] = _selectedViewModel[0];
                    _selectedViewModel[0] = item;
                }
                else
                {
                    var itemIndex = _selectedViewModel.FindIndex(r=>r.GetType() == value.GetType());
                    var item = _selectedViewModel[itemIndex];
                    _selectedViewModel[itemIndex] = _selectedViewModel[0];
                    _selectedViewModel[0] = item;
                }

                OnPropertyChanged(nameof(SelectedViewModel));
            }
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
    }
}
