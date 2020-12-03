using SendCV.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SendCV.Command
{
    public class UpdateViewCommand : ICommand
    {
        private MainWindowViewModel viewModel;

        public UpdateViewCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "AddCompany")
            {
                viewModel.SelectedViewModel = new AddCompanyViewModel();
            }
            else if (parameter.ToString() == "Table")
            {
                viewModel.SelectedViewModel = new TableViewModel();
            }
            else if (parameter.ToString() == "EmailCredentials")
            {
                viewModel.SelectedViewModel = new EmailCredentialsViewModel();
            }
        }
    }
}
