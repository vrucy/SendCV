using SendCV.Models;
using System.Threading.Tasks;

namespace SendCV.Interface
{
    public interface IEmailService
    {
        Task SendEmail(CompanyCredentials company, bool isSendAtt);
    }
}
