using SendCV.Interface;
using SendCV.Models;
using Serilog;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using Unity;

namespace SendCV.Services
{
    public class EmailService : IEmailService
    {
        private string rootPath = ConfigurationManager.AppSettings["rootWritePath"];
        private string userName = ConfigurationManager.AppSettings["UserNameEmail"];

        private string pass = CryptoHelper.Crypto.DecryptStringAES(ConfigurationManager.AppSettings["PassEmail"]);
        private IUnityContainer _container;
        private FileReader _fileReader;
        //private readonly ILogger _logger;
        public EmailService(IUnityContainer container, FileReader fileReader/*,ILogger logger*/)
        {
            _container = container;
            _fileReader = fileReader;
            //_logger = logger;
        }

        

        public async Task SendEmail(CompanyCredentials company,bool isAtt)
        {
            try
            {
                using (SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com"))
                {
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(userName, pass);
                    SmtpServer.EnableSsl = true;

                    await SmtpServer.SendMailAsync(CreateMail(company, isAtt));
                }
            }
            catch (System.Exception e)
            {
                //_logger.Error("Exeption message: " + e.Message);
                //_logger.Error("Inner exeption message: " + e.InnerException.Message);
                //TODO: mess error comp name
                throw;
            }
        }

        private MailMessage CreateMail (CompanyCredentials company, bool isAtt)
        {
            var companyPath = String.Format("{0}/{1}", rootPath, company.Name);

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(userName);
            mail.To.Add(company.Email);
            mail.Subject = SubjectEmail(company.SubjectEmail);
            mail.Body = _fileReader.GetEmailText(companyPath);

            var zipPath = String.Format("{0}/VladimirVrucinicDoc.zip", companyPath);
            mail.Attachments.Add(new Attachment(zipPath));
            return mail;
        }
        private string SubjectEmail(string subject)
        {
            if (String.IsNullOrEmpty(subject))
            {
                return "Vladimir Vrucinic - Software Developer job";
            }
            else
            {
                return subject;
            }
        }
    }
}
