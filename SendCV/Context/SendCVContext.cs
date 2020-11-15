using Microsoft.EntityFrameworkCore;
using SendCV.Models;
using System.Configuration;

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
            const string SERVER = "SendCVDebugContext";
            //SendCVDebugContext
#else

        const string SERVER = "SendCVContext";

#endif
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings[SERVER].ConnectionString);

        }
    }
}
