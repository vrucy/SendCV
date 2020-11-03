using SendCV.Models;

namespace SendCV.Interface
{
    public interface IEmailService
    {
        void SendEmail(CompanyCredentials company, bool isSendAtt);
    }
}
