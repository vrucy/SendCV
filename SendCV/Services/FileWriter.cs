using Syncfusion.DocIO.DLS;
using System;
using System.Configuration;
using Syncfusion.Pdf;
using Syncfusion.DocToPDFConverter;
using System.IO;
using Syncfusion.DocIO;
using System.Collections.Generic;
using System.IO.Compression;
using SendCV.Models;
using Syncfusion.Windows.Shared;
using SendCV.Extensions;

namespace SendCV.Services
{
    public class FileWriter
    {
        private string rootWritePath = ConfigurationManager.AppSettings["rootWritePath"];
        private string pathCompany { get; set; }
        public FileWriter()
        {

        }
        public void WriteDocuments(CompanyCredentials company, bool isSendAtt)
        {
            pathCompany = String.Format("{0}/{1}", rootWritePath, company.Name);
            CreateCompanyFolder(pathCompany);
            WriteTextEmail(company, pathCompany, isSendAtt);

            WriteCoverLetter(company, pathCompany);
            CopyFile(pathCompany);
            ZipFiles(pathCompany);

        }
        private void WriteCoverLetter(CompanyCredentials company, string path)
        {
            WordDocument docToRead = new WordDocument(String.Format("{0}/CoverLetterVladimirVrucinic.docx", rootWritePath));

            docToRead.Replace("{company}", company.Name, true, false);
            docToRead.Replace("{date}", DateTime.Now.ToString("MMMM dd, yyyy"), true, false);
            docToRead.HrManager(company.NameHR);

            docToRead.Replace("{city}", company.CompanyAddress.City, true, false);
            docToRead.Replace("{address}", company.CompanyAddress.Address, true, false);

            DocToPDFConverter converter = new DocToPDFConverter();
            PdfDocument pdfDocument = converter.ConvertToPDF(docToRead);
            pdfDocument.Save(String.Format("{0}/CoverLetterVladimirVrucinic.pdf", path));

            pdfDocument.Close(true);
            docToRead.Close();

        }
        private void WriteTextEmail(CompanyCredentials company, string path, bool isSendAtt)
        {
            var typeEmail = isSendAtt ? "EmailToSend.docx" : "EmailToSendWithoutAtt.docx";
            WordDocument docToRead = new WordDocument(String.Format("{0}/{1}", rootWritePath, typeEmail));
            //TODO: uraditi za sve jednu metodu koja radi ovo ispod za writeCoverLetter 
            docToRead.HrManager(company.NameHR);

            docToRead.Replace("{company}", company.Name, true, false);
            docToRead.Replace("{country}", company.CompanyAddress.Country, true, false);
            docToRead.Save(String.Format("{0}/EmailToSend.txt", path), FormatType.Txt);
        }
        private void ZipFiles(string path)
        {
            string[] filePaths = Directory.GetFiles(path, "*.pdf", SearchOption.TopDirectoryOnly);

            var zip = ZipFile.Open(String.Format("{0}/VladimirVrucinicDoc.zip", path), ZipArchiveMode.Create);
            foreach (var file in filePaths)
            {
                zip.CreateEntryFromFile(file, Path.GetFileName(file));
            }
            zip.Dispose();
        }
        private void CopyFile(string path)
        {
            var sourceFileCV = String.Format("{0}/VladimirVrucinicCV.pdf", rootWritePath);
            var sourceFileDiplom = String.Format("{0}/VladimirVrucinicDiplom.pdf", rootWritePath);
            var sourceFileRecommendation = String.Format("{0}/VladimirVrucinicRecommendation.pdf", rootWritePath);
            System.IO.File.Copy(sourceFileCV, String.Format("{0}/VladimirVrucinicCV.pdf", path));
            System.IO.File.Copy(sourceFileDiplom, String.Format("{0}/VladimirVrucinicDiplom.pdf", path));
            System.IO.File.Copy(sourceFileRecommendation, String.Format("{0}/VladimirVrucinicRecommendation.pdf", path));
        }
        private void CreateCompanyFolder(string path)
        {
            var companyName = path.Substring(path.LastIndexOf('/') + 1);
            string[] myDirs = Directory.GetDirectories(rootWritePath, $"{companyName}*", SearchOption.TopDirectoryOnly);
            if (System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(String.Format("{0}{1}", path, myDirs.Length));
                pathCompany = String.Format("{0}{1}", path, myDirs.Length);
            }
            else
            {
                System.IO.Directory.CreateDirectory(path);
            }

        }
    }
}
