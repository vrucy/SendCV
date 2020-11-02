using SendCV.Interface;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using Unity;

namespace SendCV.Services
{
    public class EmailService: IEmailService
    {
        private string rootPath = ConfigurationManager.AppSettings["rootWritePath"];
        private const string userName = "vrucy1991@gmail.com";
        //TODO: encript
        private const string pass = "Lion wir1";
        private IUnityContainer _container;

        public EmailService(IUnityContainer container)
        {
            _container = container;
        }

        public void SendEmail(string emailToSend, bool isSendAtt, string companyName )
        {
            var companyPath = String.Format("{0}/{1}", rootPath, companyName);
            var zipPath = String.Format("{0}/VladimirVrucinicDoc.zip", companyPath);
            var fileReader = _container.Resolve<FileReader>();
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(userName);
                mail.To.Add(emailToSend);

                mail.Subject = "Job";
                mail.Body = fileReader.GetEmailText(companyPath);
                if (isSendAtt)
                {
                    mail.Attachments.Add(new Attachment(zipPath));
                }
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(userName, pass);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (System.Exception e)
            {

                throw;
            }
        }
    }
}
