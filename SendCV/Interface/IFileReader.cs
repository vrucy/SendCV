namespace SendCV.Interface
{
    public interface IFileReader
    {
        string GetEmailText(string companyPath);
        string GetEmailSubject();
        string CompanyPath(string companyName);
    }
}
