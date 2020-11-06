using Microsoft.EntityFrameworkCore.Diagnostics;
using SendCV.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SendCV.Interface
{
    public interface ICompanyRepo
    {
        Task SaveCompanies(IEnumerable<CompanyCredentials> companies);
        Task SaveCompany(CompanyCredentials company);
        Task<CompanyCredentials> GetCompanyByLastDate(string name);
        int GetCompnayCount(string name);

    }
}
