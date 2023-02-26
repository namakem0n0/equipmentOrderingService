﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using equipmentOrderingService.Data;

#nullable disable

namespace equipmentOrderingService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230225170921_OrderingContractUpdate")]
    partial class OrderingContractUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("equipmentOrderingService.Models.IndustrialPremises", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("FreeArea")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalArea")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("IndustrialPremises");
                });

            modelBuilder.Entity("equipmentOrderingService.Models.OrderingContract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EquipmentQuantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("EquipmentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PremisesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentTypeId");

                    b.HasIndex("PremisesId");

                    b.ToTable("OrderingContracts");
                });

            modelBuilder.Entity("equipmentOrderingService.Models.TechnicalEquipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TechnicalEquipment");
                });

            modelBuilder.Entity("equipmentOrderingService.Models.OrderingContract", b =>
                {
                    b.HasOne("equipmentOrderingService.Models.TechnicalEquipment", "EquipmentType")
                        .WithMany()
                        .HasForeignKey("EquipmentTypeId");

                    b.HasOne("equipmentOrderingService.Models.IndustrialPremises", "Premises")
                        .WithMany()
                        .HasForeignKey("PremisesId");

                    b.Navigation("EquipmentType");

                    b.Navigation("Premises");
                });
#pragma warning restore 612, 618
        }
    }
}