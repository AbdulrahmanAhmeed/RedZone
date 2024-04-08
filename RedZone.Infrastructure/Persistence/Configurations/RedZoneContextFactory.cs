using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.Infrastructure.Persistence.Configurations
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<RedZoneDB>
    {
        public RedZoneDB CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RedZoneDB>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-O4872J7;Database=BuberDinners;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=Yes");

            return new RedZoneDB(optionsBuilder.Options);
        }
    }
}
