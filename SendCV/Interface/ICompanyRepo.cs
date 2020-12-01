using SendCV.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SendCV.Interface
{
    public interface ICompanyRepo
    {
        Task SaveCompany(CompanyCredentials company);
        IList<CompanyCredentials> GetCompaniesByLastDate(string name);
        IList<CompanyCredentials> GetCompanies();
        int GetCompnayCount(string name);
        Task UpdateSendingDate(CompanyCredentials company);

    }
}
