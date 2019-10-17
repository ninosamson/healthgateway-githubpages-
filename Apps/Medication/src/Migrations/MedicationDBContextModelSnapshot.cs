﻿// <auto-generated />
using System;
using HealthGateway.Medication.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Medication.Migrations
{
    [DbContext(typeof(MedicationDBContext))]
    partial class MedicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("Relational:Sequence:.trace_seq", "'trace_seq', '', '1', '1', '1', '999999', 'Int64', 'True'");

            modelBuilder.Entity("HealthGateway.DIN.Models.ActiveIngredient", b =>
                {
                    b.Property<Guid>("ActiveIngredientId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActiveIngredientCode");

                    b.Property<string>("Base")
                        .HasMaxLength(1);

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("DosageUnit")
                        .HasMaxLength(40);

                    b.Property<string>("DosageUnitFrench")
                        .HasMaxLength(80);

                    b.Property<string>("DosageValue")
                        .HasMaxLength(20);

                    b.Property<Guid?>("DrugProductId");

                    b.Property<string>("Ingredient")
                        .HasMaxLength(240);

                    b.Property<string>("IngredientFrench")
                        .HasMaxLength(400);

                    b.Property<string>("IngredientSuppliedInd")
                        .HasMaxLength(1);

                    b.Property<string>("Notes")
                        .HasMaxLength(2000);

                    b.Property<string>("Strength")
                        .HasMaxLength(20);

                    b.Property<string>("StrengthType")
                        .HasMaxLength(40);

                    b.Property<string>("StrengthTypeFrench")
                        .HasMaxLength(80);

                    b.Property<string>("StrengthUnit")
                        .HasMaxLength(40);

                    b.Property<string>("StrengthUnitFrench")
                        .HasMaxLength(80);

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.Property<string>("UpdatedDateTime")
                        .IsRequired();

                    b.HasKey("ActiveIngredientId");

                    b.HasIndex("DrugProductId");

                    b.ToTable("ActiveIngredients");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.Company", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.Property<string>("UpdatedDateTime")
                        .IsRequired();

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.DrugProduct", b =>
                {
                    b.Property<Guid>("DrugProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessionNumber")
                        .HasMaxLength(5);

                    b.Property<string>("AiGroupNumber")
                        .HasMaxLength(10);

                    b.Property<string>("BrandName")
                        .HasMaxLength(200);

                    b.Property<string>("BrandNameFrench")
                        .HasMaxLength(300);

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("Descriptor")
                        .HasMaxLength(150);

                    b.Property<string>("DescriptorFrench")
                        .HasMaxLength(200);

                    b.Property<string>("DrugClass")
                        .HasMaxLength(40);

                    b.Property<string>("DrugClassFrench")
                        .HasMaxLength(80);

                    b.Property<string>("DrugCode");

                    b.Property<string>("DrugIdentificationNumber")
                        .HasMaxLength(29);

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("NumberOfAis")
                        .HasMaxLength(10);

                    b.Property<string>("PediatricFlag")
                        .HasMaxLength(1);

                    b.Property<string>("ProductCategorization")
                        .HasMaxLength(80);

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.Property<string>("UpdatedDateTime")
                        .IsRequired();

                    b.HasKey("DrugProductId");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.Form", b =>
                {
                    b.Property<Guid>("FormId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<Guid?>("DrugProductId");

                    b.Property<string>("PharmaceuticalForm")
                        .HasMaxLength(40);

                    b.Property<int>("PharmaceuticalFormCode");

                    b.Property<string>("PharmaceuticalFormFrench")
                        .HasMaxLength(80);

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.Property<string>("UpdatedDateTime")
                        .IsRequired();

                    b.HasKey("FormId");

                    b.HasIndex("DrugProductId");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.Packaging", b =>
                {
                    b.Property<Guid>("PackagingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<Guid?>("DrugProductId");

                    b.Property<string>("PackageSize")
                        .HasMaxLength(5);

                    b.Property<string>("PackageSizeUnit")
                        .HasMaxLength(40);

                    b.Property<string>("PackageSizeUnitFrench")
                        .HasMaxLength(80);

                    b.Property<string>("PackageType")
                        .HasMaxLength(40);

                    b.Property<string>("PackageTypeFrench")
                        .HasMaxLength(80);

                    b.Property<string>("ProductInformation")
                        .HasMaxLength(80);

                    b.Property<string>("UPC")
                        .HasMaxLength(12);

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.Property<string>("UpdatedDateTime")
                        .IsRequired();

                    b.HasKey("PackagingId");

                    b.HasIndex("DrugProductId");

                    b.ToTable("Packaging");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.PharmaceuticalStd", b =>
                {
                    b.Property<Guid>("PharmaceuticalStdId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<Guid?>("DrugProductId");

                    b.Property<string>("PharmaceuticalStdDesc");

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.Property<string>("UpdatedDateTime")
                        .IsRequired();

                    b.HasKey("PharmaceuticalStdId");

                    b.HasIndex("DrugProductId");

                    b.ToTable("PharmaceuticalStds");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.Route", b =>
                {
                    b.Property<Guid>("RouteId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Administration")
                        .HasMaxLength(40);

                    b.Property<int>("AdministrationCode");

                    b.Property<string>("AdministrationFrench")
                        .HasMaxLength(80);

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<Guid?>("DrugProductId");

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.Property<string>("UpdatedDateTime")
                        .IsRequired();

                    b.HasKey("RouteId");

                    b.HasIndex("DrugProductId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.Schedule", b =>
                {
                    b.Property<Guid>("ScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<Guid?>("DrugProductId");

                    b.Property<string>("ScheduleDesc")
                        .HasMaxLength(40);

                    b.Property<string>("ScheduleDescFrench")
                        .HasMaxLength(80);

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.Property<string>("UpdatedDateTime")
                        .IsRequired();

                    b.HasKey("ScheduleId");

                    b.HasIndex("DrugProductId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.Status", b =>
                {
                    b.Property<Guid>("StatusId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("CurrentStatusFlag")
                        .HasMaxLength(1);

                    b.Property<Guid?>("DrugProductId");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<DateTime>("HistoryDate");

                    b.Property<string>("LotNumber")
                        .HasMaxLength(80);

                    b.Property<string>("StatusDesc")
                        .HasMaxLength(40);

                    b.Property<string>("StatusDescFrench")
                        .HasMaxLength(80);

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.Property<string>("UpdatedDateTime")
                        .IsRequired();

                    b.HasKey("StatusId");

                    b.HasIndex("DrugProductId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.TherapeuticClass", b =>
                {
                    b.Property<Guid>("TherapeuticClassId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ahfs")
                        .HasMaxLength(80);

                    b.Property<string>("AhfsFrench")
                        .HasMaxLength(160);

                    b.Property<string>("Atc")
                        .HasMaxLength(120);

                    b.Property<string>("AtcFrench")
                        .HasMaxLength(240);

                    b.Property<string>("AtcNumber")
                        .HasMaxLength(8);

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<Guid?>("DrugProductId");

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.Property<string>("UpdatedDateTime")
                        .IsRequired();

                    b.HasKey("TherapeuticClassId");

                    b.HasIndex("DrugProductId");

                    b.ToTable("TherapeuticClass");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.VeterinarySpecies", b =>
                {
                    b.Property<Guid>("VeterinarySpeciesId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<Guid?>("DrugProductId");

                    b.Property<string>("Species")
                        .HasMaxLength(80);

                    b.Property<string>("SpeciesFrench")
                        .HasMaxLength(160);

                    b.Property<string>("SubSpecies")
                        .HasMaxLength(80);

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.Property<string>("UpdatedDateTime")
                        .IsRequired();

                    b.HasKey("VeterinarySpeciesId");

                    b.HasIndex("DrugProductId");

                    b.ToTable("VeterinarySpecies");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.ActiveIngredient", b =>
                {
                    b.HasOne("HealthGateway.DIN.Models.DrugProduct", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugProductId");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.Form", b =>
                {
                    b.HasOne("HealthGateway.DIN.Models.DrugProduct", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugProductId");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.Packaging", b =>
                {
                    b.HasOne("HealthGateway.DIN.Models.DrugProduct", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugProductId");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.PharmaceuticalStd", b =>
                {
                    b.HasOne("HealthGateway.DIN.Models.DrugProduct", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugProductId");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.Route", b =>
                {
                    b.HasOne("HealthGateway.DIN.Models.DrugProduct", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugProductId");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.Schedule", b =>
                {
                    b.HasOne("HealthGateway.DIN.Models.DrugProduct", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugProductId");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.Status", b =>
                {
                    b.HasOne("HealthGateway.DIN.Models.DrugProduct", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugProductId");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.TherapeuticClass", b =>
                {
                    b.HasOne("HealthGateway.DIN.Models.DrugProduct", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugProductId");
                });

            modelBuilder.Entity("HealthGateway.DIN.Models.VeterinarySpecies", b =>
                {
                    b.HasOne("HealthGateway.DIN.Models.DrugProduct", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
