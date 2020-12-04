using SendCV.Interface;
using System;
using System.Configuration;
using System.IO;

namespace SendCV.Services
{
    public class FileReader: IFileReader
    {
        public string RootPath()
        {
            var path = ConfigurationManager.AppSettings["rootWritePath"];
            return path;
        }

        public string GetEmailText(string companyPath)
        {
            var filePath = String.Format("{0}/EmailToSend.txt", companyPath);
            
            return File.ReadAllText(filePath);
        }
        
        public string GetEmailSubject()
        {
            return ConfigurationManager.AppSettings["EmailSubject"]; 
        }   
    }
}
