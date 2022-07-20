using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curso.ComercioElectronico.Infraestructura.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(b => b.Code);

            builder.Property(b => b.Code).HasMaxLength(10).IsRequired();

            builder.Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();
            
            builder.Property(b => b.Address)
            .HasMaxLength(200)
            .IsRequired();

            builder.Property(b => b.Country)
            .HasMaxLength(60)
            .IsRequired();

            builder.Property(b => b.Email)
            .HasMaxLength(100)
            .IsRequired();
        }
    }
}
