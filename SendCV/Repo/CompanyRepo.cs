using SendCV.Context;
using SendCV.Interface;
using SendCV.Models;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Unity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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


        public async Task<CompanyCredentials> GetCompanyByLastDate(string name)
        {
            
            var x = await _context.CompanyCredentials.Where(c => c.Name == name).OrderByDescending(y => y.DateEmailSend).FirstOrDefaultAsync();
            return x;
        }

        public int GetCompnayCount(string name)
        {
            var countCompany = _context.CompanyCredentials.Where(c => c.Name == name).Count();
            return countCompany;
        }
        public async Task SaveCompany(CompanyCredentials company)
        {
            await _context.CompanyCredentials.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        IList<CompanyCredentials> ICompanyRepo.GetCompanies()
        {
            return _context.CompanyCredentials.Include(a => a.CompanyAddress).ToList();

        }
    }
}
