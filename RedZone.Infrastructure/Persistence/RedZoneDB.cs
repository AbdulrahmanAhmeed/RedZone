using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RedZone.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.Infrastructure.Persistence
{
    public class RedZoneDB : IdentityDbContext<User>
    {
        public RedZoneDB(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder//.Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(RedZoneDB).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.AddInterceptors(_publishDomainEventsInterceptors);
            base.OnConfiguring(optionsBuilder);
        }

    }

}
