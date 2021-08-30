using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(EntityConfigurationConstants.NAME_MAX_LENGTH);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(EntityConfigurationConstants.NAME_MAX_LENGTH);
            builder.Property(x => x.Dob).IsRequired();
        }
    }
}