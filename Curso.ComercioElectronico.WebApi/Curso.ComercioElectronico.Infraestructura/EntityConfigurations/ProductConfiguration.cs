﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curso.ComercioElectronico.Infraestructura.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).IsRequired();

            builder.Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(b => b.Description)
            .HasMaxLength(256);

            builder.Property(b => b.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

            builder.HasOne(b => b.ProductType)
                .WithMany()
                .HasForeignKey(b => b.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Brand)
                .WithMany()
                .HasForeignKey(b => b.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.IsDelete).IsRequired();
        }
    }
}
