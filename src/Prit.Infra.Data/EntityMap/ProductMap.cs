using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prit.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prit.Infra.Data.EntityMap
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(model => model.Id);

            builder.Property(model => model.Id)
                .ValueGeneratedOnAdd();

            builder.Property(model => model.Description)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(model => model.Name)
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(model => model.Price)
                .HasColumnType("DECIMAL(14,2)")
                .IsRequired();
        }
    }
}
