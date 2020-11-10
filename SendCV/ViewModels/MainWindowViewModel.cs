using Microsoft.EntityFrameworkCore.Internal;
using SendCV.Command;
using SendCV.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Unity;

namespace SendCV.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IUnityContainer _container;

        public MainWindowViewModel(IUnityContainer container)
        {
            _container = container;
            var x = new AddCompany();
            UpdateViewCommand = new UpdateViewCommand(this);
        }
        public ICommand UpdateViewCommand { get; set; }

       
    
        List<BaseViewModel> _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get 
            {
                if (_selectedViewModel == null)
                {
                    _selectedViewModel = new List<BaseViewModel>();
                    _selectedViewModel.Add(new AddCompanyViewModel());
                }
                
                var x = _selectedViewModel.FirstOrDefault();
                return x; 
            }
            set
            {
                if (!_selectedViewModel.Any(y => y.GetType() == typeof(AddCompanyViewModel)))
                {
                    _selectedViewModel.Add(value);
                }
                else if (!_selectedViewModel.Any(y => y.GetType() == typeof(TableViewModel)))
                {
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
    }
}
