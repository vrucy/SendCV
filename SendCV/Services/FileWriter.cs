using Syncfusion.DocIO.DLS;
using System;
using System.Configuration;
using Syncfusion.Pdf;
using Syncfusion.DocToPDFConverter;
using System.IO;
using Syncfusion.DocIO;
using System.IO.Compression;
using SendCV.Models;
using SendCV.Extensions;
using System.Windows;
using System.Threading.Tasks;
using Serilog;
using Unity;

namespace SendCV.Services
{
    public class FileWriter
    {
        private string rootWritePath = ConfigurationManager.AppSettings["rootWritePath"];
        private string pathCompany { get; set; }
        private IUnityContainer _container;

        public FileWriter()
        {
            _container = new UnityContainer();
        }
        public void WriteDocuments(CompanyCredentials company, bool isSendAtt)
        {
            pathCompany = String.Format("{0}/{1}", rootWritePath, company.Name);
            CreateCompanyFolder(pathCompany);
            try
            {
                WriteTextEmail(company, pathCompany, isSendAtt);
                WriteCoverLetter(company, pathCompany);
                CopyFile(pathCompany);
                ZipFiles(pathCompany);
            }
            catch (IOException e)
            {
                System.Windows.MessageBox.Show("File is open, please close!","Confiramtion",MessageBoxButton.OK,MessageBoxImage.Warning);
                WriteDocuments(company, isSendAtt);
            }
        }
        private void WriteCoverLetter(CompanyCredentials company, string path)
        {
            WordDocument docToRead = new WordDocument(String.Format("{0}/CoverLetterVladimirVrucinic.docx", rootWritePath));

            docToRead.ReplaceDataInDocument("{company}", company.Name);
            docToRead.Replace("{date}", DateTime.Now.ToString("MMMM dd, yyyy"), true, false);
            docToRead.ReplaceHrData(company.NameHR);

            docToRead.ReplaceDataInDocument("{city}", company.CompanyAddress.City);
            docToRead.ReplaceAddress(company.CompanyAddress.Address);

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
            docToRead.ReplaceHrDataInEmail(company.NameHR);

            docToRead.ReplaceDataInDocument("{company}", company.Name);
            docToRead.ReplaceDataInDocument("{country}", company.CompanyAddress.Country);
            docToRead.Save(String.Format("{0}/EmailToSend.txt", path), FormatType.Txt);
            docToRead.Close();
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
        public async Task CopyFileAsync(string sourceFile, string destinationFile)
        {
            var fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;
            var bufferSize = 4096;

            using (var sourceStream =
                  new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, fileOptions))

            using (var destinationStream =
                  new FileStream(destinationFile, FileMode.CreateNew, FileAccess.Write, FileShare.None, bufferSize, fileOptions))

                await sourceStream.CopyToAsync(destinationStream, bufferSize);
        }
        private void CreateCompanyFolder(string path)
        {
            var companyName = path.Substring(path.LastIndexOf('/') + 1);
            string[] myDirs = Directory.GetDirectories(rootWritePath, $"{companyName}*", SearchOption.TopDirectoryOnly);
            if (System.IO.Directory.Exists(path))
            {
                DeleteCompanyFolder(path);
                System.IO.Directory.CreateDirectory(path);
            }
            else
            {
                System.IO.Directory.CreateDirectory(path);
            }

        }
        public async void DeleteCompanyFolder(string companyName)
        {
            System.IO.Directory.Delete(companyName, true);
        }
    }
}
