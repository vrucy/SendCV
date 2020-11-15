using Microsoft.EntityFrameworkCore.Internal;
using SendCV.Command;
using SendCV.Interface;
using SendCV.Models;
using SendCV.Repo;
using SendCV.Services;
using Syncfusion.Data.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Unity;

namespace SendCV.ViewModels
{
    public class TableViewModel: BaseViewModel
    {
        ObservableCollection<CompanyCredentials> _companies;
        private readonly IEmailService _emailService;
        private readonly FileWriter _fileWriter;

        private IUnityContainer _container;
        private readonly ICompanyRepo _companyRepo;

        public TableViewModel()
        {
            _container = new UnityContainer();
            _companyRepo = _container.Resolve<CompanyRepo>();
            _fileWriter = _container.Resolve<FileWriter>();
            _emailService = _container.Resolve<EmailService>();

            _companies = new ObservableCollection<CompanyCredentials>(_companyRepo.GetCompanies());
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
        private async void SendMail(object x)
        {
            var companyToSend = Companies.Where(c => c.Selected).ToList();

            foreach (var item in companyToSend)
            {
                var sendAtt = item.SelectedTypeEmail.Equals("OnlyEmail") ? false : true;
                _fileWriter.WriteDocuments(item, sendAtt);
                await _emailService.SendEmail(item, sendAtt);
                await _companyRepo.SaveCompany(item);
                Companies.Remove(item);
            }
            OnPropertyChanged("Companies");

        }
    }
}
