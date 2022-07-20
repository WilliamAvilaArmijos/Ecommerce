using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Dominio;
using Curso.ComercioElectronico.Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructura
{
    public class ComercioElectronicoDbContext:DbContext
    {
        public ComercioElectronicoDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // 1.configurar proveedor
           // 2.configurar conexion
           //var conexion = @"server=(localdb)\mssqllocaldb;database=cursonet.comercioelectronico;trusted_connection=true";
           // optionsbuilder.usesqlserver(conexion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        
        }
    }
}
