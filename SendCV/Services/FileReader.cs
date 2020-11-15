using SendCV.Interface;
using System;
using System.Configuration;
using System.IO;

namespace SendCV.Services
{
    public class FileReader: IFileReader
    {
        public string Destination()
        {
            var path = ConfigurationManager.AppSettings["path"];
            return path;
        }

        public string GetEmailText(string companyPath)
        {
            var filePath = String.Format("{0}/EmailToSend.txt", companyPath);
            
            string text = File.ReadAllText(filePath);
            return text;
        }

        public string GetEmailSubject()
        {
            return ConfigurationManager.AppSettings["EmailSubject"]; 
        }
        public string CompanyPath(string companyName)
        {
            return "";
        }
        public string PathLetter()
        {
            throw new System.NotImplementedException();
        }
    }
}
