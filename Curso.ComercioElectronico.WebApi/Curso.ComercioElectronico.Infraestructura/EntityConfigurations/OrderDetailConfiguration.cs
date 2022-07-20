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
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).IsRequired();

            builder.Property(b => b.Quantity)
            .IsRequired();

            builder.Property(b => b.Total)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

            builder.HasOne(b => b.Product)
                .WithMany()
                .HasForeignKey(b => b.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Order)
                .WithMany()
                .HasForeignKey(b => b.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.IsDelete).IsRequired();
        }
    }
}
