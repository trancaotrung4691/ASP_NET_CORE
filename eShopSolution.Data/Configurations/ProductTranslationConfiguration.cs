using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.Data.Configurations
{
    public class ProductTranslationConfiguration : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("ProductTranslations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Product).WithMany(p => p.ProductTranslations).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Language).WithMany(p => p.ProductTranslations).HasForeignKey(x => x.LanguageId);
            builder.Property(x => x.Name).HasMaxLength(EntityConfigurationConstants.NAME_MAX_LENGTH).IsRequired();
            builder.Property(x => x.Details).HasMaxLength(EntityConfigurationConstants.DETAIL_MAX_LENGTH);
            builder.Property(x => x.SeoTitle).HasMaxLength(EntityConfigurationConstants.TITLE_MAX_LENGTH).IsRequired();
            builder.Property(x => x.SeoAlias).HasMaxLength(EntityConfigurationConstants.SEO_ALIAS_MAX_LENGTH).IsRequired();
            builder.Property(x => x.LanguageId).HasMaxLength(EntityConfigurationConstants.LANGUAGE_ID_MAX_LENGTH).IsRequired().IsUnicode(false);
        }
    }
}