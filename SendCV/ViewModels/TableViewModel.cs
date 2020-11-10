using Microsoft.EntityFrameworkCore.Internal;
using SendCV.Interface;
using SendCV.Models;
using SendCV.Repo;
using Syncfusion.Data.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity;

namespace SendCV.ViewModels
{
    public class TableViewModel: BaseViewModel
    {
        ObservableCollection<CompanyCredentials> _companies;
        private IUnityContainer _container;
        private readonly ICompanyRepo _companyRepo;

        public TableViewModel()
        {
            _container = new UnityContainer();
            _companyRepo = _container.Resolve<CompanyRepo>();
            _companies = new ObservableCollection<CompanyCredentials>(_companyRepo.GetCompanies());
        }
        public ObservableCollection<CompanyCredentials> Companies
        {
            get { return _companies; }
            set
            {
                _companies = value;
            }
        }
    }
}
