//using Microsoft.EntityFramework;
using Microsoft.EntityFrameworkCore;
using SendCV.Models;
using System.Configuration;
//using System.Data.Entity;

namespace SendCV.Context
{
    class SendCVContext: DbContext
    {

        //public SendCVContext() : base("name=SendCvContext") { }
        public SendCVContext()
        {

        }

        public DbSet<CompanyCredentials> CompanyCredentials { get; set; }
        public DbSet<CompanyAddress> CompanyAddresses { get; set; }
        //protected override void  OnConfiguring(DbContextOptions optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=L-TO-THE-APTOP\\SQLEXPRESS;Database=Maloli;Trusted_Connection=True;MultipleActiveResultSets=true");

        //    //optionsBuilder.ConfigureWarnings(x => x.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["SendCvContext"].ConnectionString);
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var x = ConfigurationManager.ConnectionStrings["SendCvContext"].ConnectionString;
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SendCvDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
