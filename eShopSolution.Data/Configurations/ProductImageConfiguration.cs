using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.ImagePath).HasMaxLength(EntityConfigurationConstants.IMAGE_PATH_MAX_LENGTH).IsRequired();
            builder.Property(x => x.Caption).HasMaxLength(EntityConfigurationConstants.CAPTION_MAX_LENGTH).IsRequired(false);

            builder.HasOne(x => x.Product).WithMany(product => product.ProductImages).HasForeignKey(x => x.ProductId);
        }
    }
}