//using Microsoft.EntityFramework;
using Microsoft.EntityFrameworkCore;
using SendCV.Models;
using System.Configuration;
//using System.Data.Entity;

namespace SendCV.Context
{
    class SendCVContext: DbContext
    { 
        public SendCVContext()
        {

        }

        public DbSet<CompanyCredentials> CompanyCredentials { get; set; }
        public DbSet<CompanyAddress> CompanyAddresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO: read from config
            //var x = ConfigurationManager.ConnectionStrings["SendCvContext"].ConnectionString;
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SendCvDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
