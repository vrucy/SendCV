using SendCV.Context;
using SendCV.Interface;
using SendCV.Models;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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

        public CompanyCredentials GetCompanyByLastDate(string name)
        {
            
            var x = _context.CompanyCredentials.Where(c => c.Name == name).OrderByDescending(y => y.DateEmailSend).FirstOrDefault();
            return x;
        }

        public int GetCompnayCount(string name)
        {
            var countCompany = _context.CompanyCredentials.Where(c => c.Name == name).Count();
            return countCompany;
        }

        public async void SaveCompanies(IEnumerable<CompanyCredentials> companies)
        {
            try
            {
                companies.ForEach(c => c.DateEmailSend = DateTime.Now);
                _context.CompanyCredentials.AddRange(companies);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
