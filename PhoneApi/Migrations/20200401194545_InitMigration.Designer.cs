﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhoneApi.DBRepository;

namespace PhoneApi.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20200401194545_InitMigration")]
    partial class InitMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("PhoneApi.Models.Cabinet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CabinetNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cabinets");
                });

            modelBuilder.Entity("PhoneApi.Models.CabinetPhone", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("CabinetId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PhoneId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CabinetId");

                    b.HasIndex("PhoneId");

                    b.ToTable("CabinetPhones");
                });

            modelBuilder.Entity("PhoneApi.Models.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Patronymic")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PhoneApi.Models.EmployeeCabinetPhone", b =>
                {
                    b.Property<long>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("CabinetPhoneId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmployeeId", "CabinetPhoneId");

                    b.HasIndex("CabinetPhoneId");

                    b.ToTable("EmployeeCabinetPhones");
                });

            modelBuilder.Entity("PhoneApi.Models.Phone", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("PhoneApi.Models.CabinetPhone", b =>
                {
                    b.HasOne("PhoneApi.Models.Cabinet", "Cabinet")
                        .WithMany("CabinetPhones")
                        .HasForeignKey("CabinetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhoneApi.Models.Phone", "Phone")
                        .WithMany("CabinetPhones")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhoneApi.Models.EmployeeCabinetPhone", b =>
                {
                    b.HasOne("PhoneApi.Models.CabinetPhone", "CabinetPhone")
                        .WithMany("EmployeeCabinetPhones")
                        .HasForeignKey("CabinetPhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhoneApi.Models.Employee", "Employee")
                        .WithMany("EmployeeCabinetPhones")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}