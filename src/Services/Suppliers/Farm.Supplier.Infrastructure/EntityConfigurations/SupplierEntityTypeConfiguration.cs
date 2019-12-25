using Farm.Suppliers.Infrastructure.AggregatesModel.SupplierAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farm.Suppliers.Infrastructure.EntityConfigurations
{
    internal class SupplierEntityTypeConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> SupplierConfiguration)
        {
            SupplierConfiguration.ToTable("supplier", SupplierContext.DEFAULT_SCHEMA);

            SupplierConfiguration.HasKey(o => o.Id);

            SupplierConfiguration.Ignore(b => b.DomainEvents);

            SupplierConfiguration.Property(o => o.Id)
                .UseHiLo("supplierseq", SupplierContext.DEFAULT_SCHEMA);

            SupplierConfiguration.Property(b => b.ShortName)
                .HasMaxLength(20)
                .IsRequired();

            SupplierConfiguration.Property(b => b.LongName)
                .HasMaxLength(200)
                .IsRequired();

            SupplierConfiguration.Property(b => b.ZipCode)
                .HasMaxLength(8)
                .IsRequired();
            SupplierConfiguration.Property(b => b.City)
                .HasMaxLength(100)
                .IsRequired();

            SupplierConfiguration.Property(b => b.AddressLine1)
                .HasMaxLength(200)
                .IsRequired();
        }
    }

}