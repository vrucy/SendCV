using SendCV.Context;
using SendCV.Interface;
using SendCV.Models;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace SendCV.Repo
{
    public class CompanyRepo: ICompanyRepo
    {
        private readonly SendCVContext _context;
        private IUnityContainer _container;
        public CompanyRepo(IUnityContainer unity)
        {
            _container = unity;
            _context = _container.Resolve<SendCVContext>();
        }

        public void SaveCompanies(IEnumerable<CompanyCredentials> companies)
        {
            try
            {
                companies.ForEach(c => c.DateEmailSend = DateTime.Now);
                _context.CompanyCredentials.AddRange(companies);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
