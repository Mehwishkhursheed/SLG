﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SLG.Data;

#nullable disable

namespace SLG.Migrations
{
    [DbContext(typeof(SLGC))]
    [Migration("20240531102929_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SLG.Models.Category", b =>
                {
                    b.Property<int>("category_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("category_id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("category_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("created_on")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<DateTime?>("last_modified")
                        .HasColumnType("date");

                    b.HasKey("category_id")
                        .HasName("PK__category__D54EE9B4C97CD641");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("SLG.Models.Customer", b =>
                {
                    b.Property<int>("customer_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("customer_id"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("customer_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<long?>("phone")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<double?>("record")
                        .HasColumnType("float");

                    b.HasKey("customer_id")
                        .HasName("PK__customer__CD65CB85B0A0220F");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("SLG.Models.Product", b =>
                {
                    b.Property<int>("product_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("product_id"));

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<bool>("discontinued")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("quantity")
                        .HasColumnType("int");

                    b.Property<int?>("reorder_level")
                        .HasColumnType("int");

                    b.Property<decimal?>("unit_price")
                        .HasColumnType("money");

                    b.HasKey("product_id")
                        .HasName("PK__product__47027DF5AED1C8E3");

                    b.HasIndex("category_id");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("SLG.Models.Purchase_Order", b =>
                {
                    b.Property<int>("purchase_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("purchase_id"));

                    b.Property<decimal>("cost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValueSql("((0.99))");

                    b.Property<DateTime?>("create_date")
                        .HasColumnType("date");

                    b.Property<string>("doc_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("payment_method")
                        .HasColumnType("int");

                    b.Property<string>("state")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasDefaultValueSql("('RFQ')");

                    b.Property<int>("vendor_id")
                        .HasColumnType("int");

                    b.HasKey("purchase_id")
                        .HasName("PK__purchase__87071CB979BDBC6D");

                    b.HasIndex("vendor_id");

                    b.HasIndex(new[] { "doc_name" }, "IX_Purchase_Order")
                        .IsUnique();

                    b.ToTable("Purchase_Order", (string)null);
                });

            modelBuilder.Entity("SLG.Models.Purchase_Order_Detail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal?>("price")
                        .HasColumnType("money");

                    b.Property<int?>("product_id")
                        .HasColumnType("int");

                    b.Property<int?>("purchase_id")
                        .HasColumnType("int");

                    b.Property<int?>("quantity")
                        .HasColumnType("int");

                    b.HasKey("ID")
                        .HasName("PK_purchase_order_details");

                    b.HasIndex("product_id");

                    b.HasIndex("purchase_id");

                    b.ToTable("Purchase_Order_Details");
                });

            modelBuilder.Entity("SLG.Models.Sale_Order", b =>
                {
                    b.Property<int>("sale_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("sale_id"));

                    b.Property<int>("customer_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("date_created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("payment_method")
                        .HasColumnType("int");

                    b.Property<string>("state")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasDefaultValueSql("('Quotation')");

                    b.Property<decimal>("total_amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValueSql("((0.00))");

                    b.HasKey("sale_id")
                        .HasName("PK__sale_ord__E1EB00B23F3E713F");

                    b.HasIndex("customer_id");

                    b.HasIndex(new[] { "name" }, "IX_Sale_Order_1")
                        .IsUnique();

                    b.ToTable("Sale_Order", (string)null);
                });

            modelBuilder.Entity("SLG.Models.Sale_Order_Detail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValueSql("((0.00))");

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((1))");

                    b.Property<int>("sale_id")
                        .HasColumnType("int");

                    b.HasKey("ID")
                        .HasName("PK_sale_order_details");

                    b.HasIndex("product_id");

                    b.HasIndex("sale_id");

                    b.ToTable("Sale_Order_Details");
                });

            modelBuilder.Entity("SLG.Models.Transfer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Doc_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Source_Document")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("backorder_doc_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("created_date")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<string>("operation_type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("ID")
                        .HasName("PK__transfer__3214EC2768AF2B94");

                    b.HasIndex("backorder_doc_id");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("SLG.Models.Transfer_Detail", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("demand")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("done")
                        .HasColumnType("int");

                    b.Property<int?>("product_id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("transfer_id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("id")
                        .HasName("PK__transfer__3213E83FFF168FCA");

                    b.HasIndex("product_id");

                    b.HasIndex("transfer_id");

                    b.ToTable("Transfer_Details");
                });

            modelBuilder.Entity("SLG.Models.Vendor", b =>
                {
                    b.Property<int>("vendor_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("vendor_id"));

                    b.Property<long?>("NTN")
                        .HasMaxLength(13)
                        .HasColumnType("bigint");

                    b.Property<string>("email_address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<long?>("phone_number")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("vendor_address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("website")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.HasKey("vendor_id")
                        .HasName("PK__vendor_m__0F7D2B7841498D66");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("SLG.Models.Product", b =>
                {
                    b.HasOne("SLG.Models.Category", "category")
                        .WithMany("Products")
                        .HasForeignKey("category_id")
                        .IsRequired()
                        .HasConstraintName("FK_product_category");

                    b.Navigation("category");
                });

            modelBuilder.Entity("SLG.Models.Purchase_Order", b =>
                {
                    b.HasOne("SLG.Models.Vendor", "vendor")
                        .WithMany("Purchase_Orders")
                        .HasForeignKey("vendor_id")
                        .IsRequired()
                        .HasConstraintName("FK_purchase_order_vendor_master");

                    b.Navigation("vendor");
                });

            modelBuilder.Entity("SLG.Models.Purchase_Order_Detail", b =>
                {
                    b.HasOne("SLG.Models.Product", "product")
                        .WithMany("Purchase_Order_Details")
                        .HasForeignKey("product_id")
                        .HasConstraintName("FK_purchase_order_details_product");

                    b.HasOne("SLG.Models.Purchase_Order", "purchase")
                        .WithMany("Purchase_Order_Details")
                        .HasForeignKey("purchase_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_purchase_order_details_purchase_order");

                    b.Navigation("product");

                    b.Navigation("purchase");
                });

            modelBuilder.Entity("SLG.Models.Sale_Order", b =>
                {
                    b.HasOne("SLG.Models.Customer", "customer")
                        .WithMany("Sale_Orders")
                        .HasForeignKey("customer_id")
                        .IsRequired()
                        .HasConstraintName("FK_sale_order_customers");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("SLG.Models.Sale_Order_Detail", b =>
                {
                    b.HasOne("SLG.Models.Product", "product")
                        .WithMany("Sale_Order_Details")
                        .HasForeignKey("product_id")
                        .IsRequired()
                        .HasConstraintName("FK_sale_order_details_product");

                    b.HasOne("SLG.Models.Sale_Order", "sale")
                        .WithMany("Sale_Order_Details")
                        .HasForeignKey("sale_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_sale_order_details_sale_order");

                    b.Navigation("product");

                    b.Navigation("sale");
                });

            modelBuilder.Entity("SLG.Models.Transfer", b =>
                {
                    b.HasOne("SLG.Models.Transfer", "backorder_doc")
                        .WithMany("Inversebackorder_doc")
                        .HasForeignKey("backorder_doc_id")
                        .HasConstraintName("FK_transfers_transfers");

                    b.Navigation("backorder_doc");
                });

            modelBuilder.Entity("SLG.Models.Transfer_Detail", b =>
                {
                    b.HasOne("SLG.Models.Product", "product")
                        .WithMany("Transfer_Details")
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_transfer_details_product");

                    b.HasOne("SLG.Models.Transfer", "transfer")
                        .WithMany("Transfer_Details")
                        .HasForeignKey("transfer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_transfer_details_transfers");

                    b.Navigation("product");

                    b.Navigation("transfer");
                });

            modelBuilder.Entity("SLG.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SLG.Models.Customer", b =>
                {
                    b.Navigation("Sale_Orders");
                });

            modelBuilder.Entity("SLG.Models.Product", b =>
                {
                    b.Navigation("Purchase_Order_Details");

                    b.Navigation("Sale_Order_Details");

                    b.Navigation("Transfer_Details");
                });

            modelBuilder.Entity("SLG.Models.Purchase_Order", b =>
                {
                    b.Navigation("Purchase_Order_Details");
                });

            modelBuilder.Entity("SLG.Models.Sale_Order", b =>
                {
                    b.Navigation("Sale_Order_Details");
                });

            modelBuilder.Entity("SLG.Models.Transfer", b =>
                {
                    b.Navigation("Inversebackorder_doc");

                    b.Navigation("Transfer_Details");
                });

            modelBuilder.Entity("SLG.Models.Vendor", b =>
                {
                    b.Navigation("Purchase_Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
