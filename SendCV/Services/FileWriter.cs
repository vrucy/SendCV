using Syncfusion.DocIO.DLS;
using System;
using System.Configuration;
using Syncfusion.Pdf;
using Syncfusion.DocToPDFConverter;
using System.IO;
using Syncfusion.DocIO;
using System.Collections.Generic;
using System.IO.Compression;

namespace SendCV.Services
{
    public class FileWriter
    {
        private string rootWritePath = ConfigurationManager.AppSettings["rootWritePath"];
        private string pathCompany { get; set; }
        public FileWriter()
        {

        }
        public void WriteDocuments(string companyName)
        {
            pathCompany = String.Format("{0}/{1}", rootWritePath, companyName);
            CreateCompanyFolder(pathCompany);
            CopyFile(pathCompany);
            WriteTextEmail(companyName, "", pathCompany);
            WriteCoverLetter(companyName, pathCompany);
            ZipFiles(pathCompany);
        }
        private void WriteCoverLetter(string companyName, string path)
        {
            WordDocument docToRead = new WordDocument(String.Format("{0}/CoverLetterVladimirVrucinic.docx", rootWritePath));

            docToRead.Replace("{company}", companyName,true,false);
            DocToPDFConverter converter = new DocToPDFConverter();
            PdfDocument pdfDocument = converter.ConvertToPDF(docToRead);
            pdfDocument.Save(String.Format("{0}/CoverLetterVladimirVrucinic.pdf", path));
            
            pdfDocument.Close(true);
            docToRead.Close();

        }
        private void WriteTextEmail(string companyName, string hrManager, string path)
        {
            WordDocument docToRead = new WordDocument(String.Format("{0}/EmailToSend.docx", rootWritePath));
            if (String.IsNullOrEmpty(hrManager))
            {
                docToRead.Replace("{hrManager}", String.Empty, true, false);
            }
            docToRead.Replace("{company}", companyName, true, false);
            docToRead.Save(String.Format("{0}/EmailToSend.txt", path), FormatType.Txt);
        }
        private void ZipFiles( string path)
        {
            string[] filePaths = Directory.GetFiles(path, "*.pdf", SearchOption.TopDirectoryOnly);

            var zip = ZipFile.Open(String.Format("{0}/VladimirVrucinicDoc.zip",path), ZipArchiveMode.Create);
            foreach (var file in filePaths)
            {
                
                zip.CreateEntryFromFile(file, Path.GetFileName(file));
            }
            zip.Dispose();
        }
        private void CopyFile(string path)
        {
            var sourceFileCV = String.Format("{0}/VladimirVrucinicCV.pdf",rootWritePath);
            var sourceFileRecommendation = String.Format("{0}/VladimirVrucinicRecommendation.pdf", rootWritePath);
            System.IO.File.Copy(sourceFileCV, String.Format("{0}/VladimirVrucinicCV.pdf", path));
            System.IO.File.Copy(sourceFileRecommendation, String.Format("{0}/VladimirVrucinicRecommendation.pdf", path));
        }
        private void CreateCompanyFolder(string path)
        {
            var companyName = path.Substring(path.LastIndexOf('/') + 1);
            string[] myDirs = Directory.GetDirectories(rootWritePath, $"{companyName}*", SearchOption.TopDirectoryOnly);
            if (System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(String.Format("{0}{1}",path,myDirs.Length));
                pathCompany = String.Format("{0}{1}", path, myDirs.Length);
            }
            else
            {
                System.IO.Directory.CreateDirectory(path);
            }

        }
    }
}
