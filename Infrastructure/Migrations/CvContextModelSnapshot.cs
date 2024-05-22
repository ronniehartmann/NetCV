﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(CvContext))]
    partial class CvContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Content", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("Contents");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Key = "PROFILE_FULLNAME",
                            Value = "Edit the value in the administrator panel."
                        },
                        new
                        {
                            Id = 2L,
                            Key = "PROFILE_EMPLOYMENT",
                            Value = "Edit the value in the administrator panel."
                        },
                        new
                        {
                            Id = 3L,
                            Key = "ABOUT_TEXT",
                            Value = "Edit the value in the administrator panel."
                        },
                        new
                        {
                            Id = 4L,
                            Key = "ABOUT_BIRTH_DATE",
                            Value = "25/10/2004"
                        },
                        new
                        {
                            Id = 5L,
                            Key = "ABOUT_EMAIL",
                            Value = "Edit the value in the administrator panel."
                        },
                        new
                        {
                            Id = 6L,
                            Key = "ABOUT_EMAIL_LINK",
                            Value = "mailto:maxmuster@example.com"
                        },
                        new
                        {
                            Id = 7L,
                            Key = "ABOUT_PHONE",
                            Value = "Edit the value in the administrator panel."
                        },
                        new
                        {
                            Id = 8L,
                            Key = "ABOUT_PHONE_LINK",
                            Value = "tel:0123456789"
                        },
                        new
                        {
                            Id = 9L,
                            Key = "ABOUT_GITHUB",
                            Value = "Edit the value in the administrator panel."
                        },
                        new
                        {
                            Id = 10L,
                            Key = "ABOUT_GITHUB_LINK",
                            Value = "https://github.com/maxmuster"
                        },
                        new
                        {
                            Id = 11L,
                            Key = "ABOUT_RESIDENCE",
                            Value = "Edit the value in the administrator panel."
                        });
                });

            modelBuilder.Entity("Domain.Models.Experience", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Experiences");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Company = "Example",
                            StartDate = new DateOnly(2020, 8, 1),
                            Text = "Edit the value in the administrator panel."
                        },
                        new
                        {
                            Id = 2L,
                            Company = "Elpmaxe",
                            EndDate = new DateOnly(2020, 7, 31),
                            StartDate = new DateOnly(2016, 1, 1),
                            Text = "Edit the value in the administrator panel."
                        });
                });

            modelBuilder.Entity("Domain.Models.Hobby", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Icon")
                        .HasColumnType("longtext");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Hobbies");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Icon = "code",
                            Text = "Edit the value in the administrator panel."
                        },
                        new
                        {
                            Id = 2L,
                            Icon = "code",
                            Text = "Edit the value in the administrator panel."
                        });
                });

            modelBuilder.Entity("Domain.Models.Skill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Level = 70,
                            Name = "Edit the value in the administrator panel."
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
