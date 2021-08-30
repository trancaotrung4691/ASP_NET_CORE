using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {

        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(EntityConfigurationConstants.NAME_MAX_LENGTH).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(EntityConfigurationConstants.EMAIL_MAX_LENGTH).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(EntityConfigurationConstants.PHONE_NUMBER_MAX_LENGTH).IsRequired();
            builder.Property(x => x.Message).IsRequired();
        }
    }
}