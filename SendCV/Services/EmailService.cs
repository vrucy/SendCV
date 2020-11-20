using SendCV.Interface;
using SendCV.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Unity;

namespace SendCV.Services
{
    public class EmailService : IEmailService
    {
        private string rootPath = ConfigurationManager.AppSettings["rootWritePath"];
        private const string userName = "vladimir.vrucinic@gmail.com";
        //TODO: encript
        private const string pass = "lionwir11";
        private IUnityContainer _container;

        public EmailService(IUnityContainer container)
        {
            _container = container;
        }

        public async Task SendEmail(CompanyCredentials company, bool isSendAtt)
        {
            var companyPath = String.Format("{0}/{1}", rootPath, company.Name);
            var fileReader = _container.Resolve<FileReader>();
            
            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(userName);
                mail.To.Add(company.Email);

                mail.Subject = "Vladimir Vrucinic - Software Developer job";
                mail.Body = fileReader.GetEmailText(companyPath);

                var zipPath = String.Format("{0}/VladimirVrucinicDoc.zip", companyPath);
                 mail.Attachments.Add(new Attachment(zipPath));
                using (SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com"))
                {
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(userName, pass);
                    SmtpServer.EnableSsl = true;

                    await SmtpServer.SendMailAsync(mail);
                }
            }
            catch (System.Exception e)
            {

                throw;
            }
        }
    }
}
