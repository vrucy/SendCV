using SendCV.Models;
using System.Threading.Tasks;

namespace SendCV.Interface
{
    public interface ICompanyRepo
    {
        Task SaveCompany(CompanyCredentials company);
        Task<CompanyCredentials> GetCompanyByLastDate(string name);
        int GetCompnayCount(string name);

    }
}
