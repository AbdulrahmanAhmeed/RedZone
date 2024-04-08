
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedZone.Domain.Users;

namespace BuberDinner.Infrastructure.Persistence.Configurations;

internal sealed class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUserTable(builder);

    }

    private void ConfigureUserTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Menus");

        builder.HasKey(m => m.Id);

        
        builder
            .Property(m => m.Name)
            .HasMaxLength(100);

       
    }

}