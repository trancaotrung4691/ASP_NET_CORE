using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace eShopSolution.Data.Configurations
{
    public class AppTransactionConfiguration : IEntityTypeConfiguration<AppTransaction>
    {
        public void Configure(EntityTypeBuilder<AppTransaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.AppUser).WithMany(au => au.AppTransactions).HasForeignKey(x => x.UserId);
        }
    }
}