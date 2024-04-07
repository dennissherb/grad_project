﻿// <auto-generated />
using System;
using Datalayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Datalayer.Migrations
{
    [DbContext(typeof(MyProjectContext))]
    [Migration("20240407160706_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Datalayer.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("accounts_id");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("accounts_date_of_birth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("accounts_email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("accounts_password");

                    b.Property<string>("PermGroup")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("accounts_perm_group");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("accounts_user_name");

                    b.HasKey("Id");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("Datalayer.Models.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pages_id");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("pages_author_id");

                    b.Property<string>("Content")
                        .HasColumnType("longtext")
                        .HasColumnName("pages_content");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("fkproducts_id");

                    b.Property<string>("Tags")
                        .HasColumnType("longtext")
                        .HasColumnName("pages_tags");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("pages_type");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("Datalayer.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("products_id");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("products_category");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("products_company");

                    b.Property<byte[]>("Image")
                        .HasColumnType("longblob")
                        .HasColumnName("products_image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("products_name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("products_price");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Datalayer.Models.Page", b =>
                {
                    b.HasOne("Datalayer.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
