using SendCV.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SendCV.Interface
{
    public interface ICompanyRepo
    {
        Task SaveCompany(CompanyCredentials company);
        CompanyCredentials GetCompanyByLastDate(string name);
        IList<CompanyCredentials> GetCompanies();
        int GetCompnayCount(string name);

    }
}
