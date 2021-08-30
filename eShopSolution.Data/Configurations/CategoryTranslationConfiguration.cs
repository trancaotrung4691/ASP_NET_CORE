using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.Data.Configurations
{
    public class CategoryTranslationConfiguration : IEntityTypeConfiguration<CategoryTranslation>
    {

        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.ToTable("CategoryTranslations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(EntityConfigurationConstants.NAME_MAX_LENGTH);
            builder.Property(x => x.SeoAlias).IsRequired().HasMaxLength(EntityConfigurationConstants.SEO_ALIAS_MAX_LENGTH);
            builder.Property(x => x.SeoDescription).IsRequired().HasMaxLength(EntityConfigurationConstants.SEO_DESCRIPTION_MAX_LENGTH);
            builder.Property(x => x.SeoTitle).IsRequired().HasMaxLength(EntityConfigurationConstants.SEO_TITLE_MAX_LENGTH);
            builder.HasOne(x => x.Category).WithMany(x => x.CategoryTranslations).HasForeignKey(x => x.CategoryId);
            builder.HasOne(x => x.Language).WithMany(x => x.CategoryTranslations).HasForeignKey(x => x.LanguageId);
        }
    }
}