using Microsoft.EntityFrameworkCore;
using SendCV.Models;
using System.Configuration;

namespace SendCV.Context
{
    class SendCVContext: DbContext
    {
        
        public SendCVContext(DbContextOptions<SendCVContext> options) : base(options) { }
       
        public DbSet<CompanyCredentials> CompanyCredentials { get; set; }
        public DbSet<CompanyAddress> CompanyAddresses { get; set; }
        //protected override void  OnConfiguring(DbContextOptions optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=L-TO-THE-APTOP\\SQLEXPRESS;Database=Maloli;Trusted_Connection=True;MultipleActiveResultSets=true");

        //    //optionsBuilder.ConfigureWarnings(x => x.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["SendCvContext"].ConnectionString);
        }
    }
}
