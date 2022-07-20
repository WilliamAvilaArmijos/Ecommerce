﻿// <auto-generated />
using System;
using Curso.ComercioElectronico.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Curso.ComercioElectronico.Infraestructura.Migrations
{
    [DbContext(typeof(ComercioElectronicoDbContext))]
    [Migration("20220628131636_AddBrand")]
    partial class AddBrand
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Curso.ComercioElectronico.Dominio.Catalogo", b =>
                {
                    b.Property<string>("Codigo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codigo");

                    b.ToTable("Catalogos");
                });

            modelBuilder.Entity("Curso.ComercioElectronico.Dominio.Entities.Brand", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Curso.ComercioElectronico.Dominio.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Curso.ComercioElectronico.Dominio.Entities.ProductType", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("ProductTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
