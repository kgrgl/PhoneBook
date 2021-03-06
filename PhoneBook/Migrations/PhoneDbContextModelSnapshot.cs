﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhoneBook.Models;

namespace PhoneBook.Migrations
{
    [DbContext(typeof(PhoneDbContext))]
    partial class PhoneDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("PhoneBook.Models.Contacts", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ContactText")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ContactType")
                        .HasColumnType("int");

                    b.Property<int?>("MatchID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MatchID");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("PhoneBook.Models.Persons", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Firma")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("UUID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("PhoneBook.Models.Contacts", b =>
                {
                    b.HasOne("PhoneBook.Models.Persons", "Match")
                        .WithMany("contacts")
                        .HasForeignKey("MatchID");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("PhoneBook.Models.Persons", b =>
                {
                    b.Navigation("contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
