using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SLG.Models;

namespace SLG.Data;

public partial class SLGC : DbContext
{
    public SLGC()
    {
    }

    public SLGC(DbContextOptions<SLGC> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }


    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purchase_Order> Purchase_Orders { get; set; }

    public virtual DbSet<Purchase_Order_Detail> Purchase_Order_Details { get; set; }

   

    public virtual DbSet<Sale_Order> Sale_Orders { get; set; }

    public virtual DbSet<Sale_Order_Detail> Sale_Order_Details { get; set; }

    public virtual DbSet<Transfer> Transfers { get; set; }

    public virtual DbSet<Transfer_Detail> Transfer_Details { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Name=SLG");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.category_id).HasName("PK__category__D54EE9B4C97CD641");

            entity.ToTable("Category");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.category_name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.created_on).HasColumnType("date");
            entity.Property(e => e.last_modified).HasColumnType("date");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.customer_id).HasName("PK__customer__CD65CB85B0A0220F");

            entity.Property(e => e.address).HasColumnType("text");
            entity.Property(e => e.customer_name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.email)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.product_id).HasName("PK__product__47027DF5AED1C8E3");

            entity.ToTable("Product");

            entity.Property(e => e.description).HasColumnType("text");
            entity.Property(e => e.name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.unit_price).HasColumnType("money");

            entity.HasOne(d => d.category).WithMany(p => p.Products)
                .HasForeignKey(d => d.category_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_category");
        });

        modelBuilder.Entity<Purchase_Order>(entity =>
        {
            entity.HasKey(e => e.purchase_id).HasName("PK__purchase__87071CB979BDBC6D");

            entity.ToTable("Purchase_Order");

            entity.HasIndex(e => e.doc_name, "IX_Purchase_Order")
            .IsUnique();

            entity.Property(e => e.cost)
                .HasDefaultValueSql("((0.99))")
                .HasColumnType("money");
            entity.Property(e => e.create_date).HasColumnType("date");
            entity.Property(e => e.doc_name)
            .HasComputedColumnSql("('P'+CONVERT([varchar](50),[purchase_id]))")
                .IsUnicode(false);
            entity.Property(e => e.state)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('RFQ')");

            
            entity.HasOne(d => d.vendor).WithMany(p => p.Purchase_Orders)
                .HasForeignKey(d => d.vendor_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_purchase_order_vendor_master");
        });

        modelBuilder.Entity<Purchase_Order_Detail>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_purchase_order_details");

            entity.Property(e => e.price).HasColumnType("money");

            entity.HasOne(d => d.product).WithMany(p => p.Purchase_Order_Details)
                .HasForeignKey(d => d.product_id)
                .HasConstraintName("FK_purchase_order_details_product");

            entity.HasOne(d => d.purchase).WithMany(p => p.Purchase_Order_Details)
                .HasForeignKey(d => d.purchase_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_purchase_order_details_purchase_order");
        });

        

        modelBuilder.Entity<Sale_Order>(entity =>
        {
            entity.HasKey(e => e.sale_id).HasName("PK__sale_ord__E1EB00B23F3E713F");

            entity.ToTable("Sale_Order");

            entity.HasIndex(e => e.name, "IX_Sale_Order_1").IsUnique();

            entity.Property(e => e.date_created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.name)
                .HasMaxLength(50)
                .HasComputedColumnSql("('S' + CAST([Id] AS nvarchar(50)))")
                .IsUnicode(false);
            entity.Property(e => e.state)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('Quotation')");
            entity.Property(e => e.total_amount)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("money");

            entity.HasOne(d => d.customer).WithMany(p => p.Sale_Orders)
                .HasForeignKey(d => d.customer_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sale_order_customers");

            
        });

        modelBuilder.Entity<Sale_Order_Detail>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_sale_order_details");

            entity.Property(e => e.price)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("money");
            entity.Property(e => e.quantity).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.product).WithMany(p => p.Sale_Order_Details)
                .HasForeignKey(d => d.product_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sale_order_details_product");

            entity.HasOne(d => d.sale).WithMany(p => p.Sale_Order_Details)
                .HasForeignKey(d => d.sale_id)
                .HasConstraintName("FK_sale_order_details_sale_order");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__transfer__3214EC2768AF2B94");

            entity.Property(e => e.Doc_name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Source_Document)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.created_date).HasColumnType("date");
            entity.Property(e => e.operation_type)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.backorder_doc).WithMany(p => p.Inversebackorder_doc)
                .HasForeignKey(d => d.backorder_doc_id)
                .HasConstraintName("FK_transfers_transfers");
        });

        modelBuilder.Entity<Transfer_Detail>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__transfer__3213E83FFF168FCA");

            entity.HasOne(d => d.product).WithMany(p => p.Transfer_Details)
                .HasForeignKey(d => d.product_id)
                .HasConstraintName("FK_transfer_details_product");

            entity.HasOne(d => d.transfer).WithMany(p => p.Transfer_Details)
                .HasForeignKey(d => d.transfer_id)
                .HasConstraintName("FK_transfer_details_transfers");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.vendor_id).HasName("PK__vendor_m__0F7D2B7841498D66");

            entity.Property(e => e.email_address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.vendor_address).HasColumnType("text");
            entity.Property(e => e.website)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
