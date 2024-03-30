﻿// <auto-generated />
using System;
using BuildVersionsApi.Features.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Persistance.Migrations
{
    [DbContext(typeof(BuildVersionsDbContext))]
    [Migration("20240330145552_SoftDelete")]
    partial class SoftDelete
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("BuildVersionsApi.Features.Domain.Model.BuildVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Build")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Major")
                        .HasColumnType("int");

                    b.Property<int>("Minor")
                        .HasColumnType("int");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Revision")
                        .HasColumnType("int");

                    b.Property<string>("SemanticVersionText")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProjectName");

                    b.ToTable("BuildVersions", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
