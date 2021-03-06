using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.Data.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {

        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Languages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(EntityConfigurationConstants.LANGUAGE_NAME_MAX_LENGTH);
            builder.Property(x => x.Id).IsRequired().IsUnicode(false).HasMaxLength(EntityConfigurationConstants.LANGUAGE_ID_MAX_LENGTH);
        }
    }
}