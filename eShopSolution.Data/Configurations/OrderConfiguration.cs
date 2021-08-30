using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ShipPhoneNumber).IsRequired().HasMaxLength(EntityConfigurationConstants.PHONE_NUMBER_MAX_LENGTH);
            builder.Property(x => x.ShipName).IsRequired().HasMaxLength(EntityConfigurationConstants.NAME_MAX_LENGTH);
            builder.Property(x => x.ShipEmail).IsRequired().HasMaxLength(EntityConfigurationConstants.EMAIL_MAX_LENGTH);
            builder.Property(x => x.ShipAddress).IsRequired().HasMaxLength(EntityConfigurationConstants.ADDRESS_MAX_LENGTH);
            builder.Property(x => x.OrderDate).IsRequired();
            builder.HasOne(x => x.AppUser).WithMany(x => x.Orders).HasForeignKey(x => x.UserId);
        }
    }
}