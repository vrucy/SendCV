//using Microsoft.EntityFramework;
using Microsoft.EntityFrameworkCore;
using SendCV.Models;
using System.Configuration;
//using System.Configuration;

namespace SendCV.Context
{
    public class SendCVContext: DbContext
    { 
        public SendCVContext()
        {

        }
        public DbSet<CompanyCredentials> CompanyCredentials { get; set; }
        public DbSet<CompanyAddress> CompanyAddresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            const string SERVER = @"Server=(localdb)\mssqllocaldb;Database=SendCvDBTest;Trusted_Connection=True;MultipleActiveResultSets=true";

#else

        const string SERVER = @"Server=(localdb)\mssqllocaldb;Database=SendCvDB;Trusted_Connection=True;MultipleActiveResultSets=true";

#endif
            //TODO: canot read configurationmanager connString
            //var x = ConfigurationManager.ConnectionStrings[SERVER].ConnectionString;
            optionsBuilder.UseSqlServer(SERVER);

        }
    }
}
