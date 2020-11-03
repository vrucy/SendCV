using SendCV.Context;
using SendCV.ViewModels;
using Syncfusion.UI.Xaml.Grid.Cells;
using System.Collections.ObjectModel;
using System.Windows;

namespace SendCV.Views
{
    /// <summary>
    /// Interaction logic for AddCompany.xaml
    /// </summary>
    public partial class AddCompany : Window
    {
        public AddCompany(AddCompayViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
       
    }
}
