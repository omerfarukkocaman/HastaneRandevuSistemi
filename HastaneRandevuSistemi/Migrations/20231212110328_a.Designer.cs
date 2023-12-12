﻿// <auto-generated />
using System;
using HastaneRandevuSistemi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HastaneRandevuSistemi.Migrations
{
    [DbContext(typeof(HastaneRandevuSistemiContext))]
    [Migration("20231212110328_a")]
    partial class a
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Doktor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("odaNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Doktor");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Hasta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DogumTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KimlikNo")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Hasta");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Randevu", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("RandevuTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("doktorId")
                        .HasColumnType("int");

                    b.Property<int>("hastaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Randevu");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Randevu", b =>
                {
                    b.HasOne("HastaneRandevuSistemi.Models.Doktor", "doktor")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HastaneRandevuSistemi.Models.Hasta", "hasta")
                        .WithMany("randevular")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("doktor");

                    b.Navigation("hasta");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Hasta", b =>
                {
                    b.Navigation("randevular");
                });
#pragma warning restore 612, 618
        }
    }
}
