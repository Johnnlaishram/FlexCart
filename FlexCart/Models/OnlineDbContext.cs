using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlexCart.Models;

public partial class OnlineDbContext : DbContext
{
    public OnlineDbContext()
    {
    }

    public OnlineDbContext(DbContextOptions<OnlineDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseProduct> PurchaseProducts { get; set; }

    public virtual DbSet<Return> Returns { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SalesProduct> SalesProducts { get; set; }

    public virtual DbSet<StockLevel> StockLevels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Server=JOHNNY_LAISHRAM\\MSSQLSERVER_NEW;Database=OnlineDB;User ID=sa;Password=Johnny@12;TrustServerCertificate=True;Encrypt=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK_Company");

            entity.ToTable("company");

            entity.Property(e => e.CompanyId)
                .ValueGeneratedNever()
                .HasColumnName("company_id");
            entity.Property(e => e.Addr)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("addr");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mobile");
            entity.Property(e => e.Web)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("web");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CusId).HasName("PK_Customer");

            entity.ToTable("customer");

            entity.Property(e => e.CusId)
                .ValueGeneratedNever()
                .HasColumnName("cus_id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mobile");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
           entity.Property(e => e.Password) // ADD THIS
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");

        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProdCode).HasName("PK_Product");

            entity.ToTable("product");

            entity.Property(e => e.ProdCode)
                .ValueGeneratedNever()
                .HasColumnName("prod_code");
            entity.Property(e => e.Barcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("barcode");
            entity.Property(e => e.ManfDate)
                .HasColumnType("datetime")
                .HasColumnName("manf_date");
            entity.Property(e => e.ProdName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prod_name");
            entity.Property(e => e.ProdPhoto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prod_photo");
            entity.Property(e => e.ProdTypeId).HasColumnName("prod_type_id");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ProdTypeId).HasName("PK_product type");

            entity.ToTable("product_type");

            entity.Property(e => e.ProdTypeId)
                .ValueGeneratedNever()
                .HasColumnName("prod_type_id");
            entity.Property(e => e.ProdType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prod_type");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurCode);

            entity.ToTable("purchase");

            entity.Property(e => e.PurCode)
                .ValueGeneratedNever()
                .HasColumnName("pur_code");
            entity.Property(e => e.NetAmt)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("net_amt");
            entity.Property(e => e.PurchaseDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("purchase_date");
            entity.Property(e => e.Purchaser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("purchaser");
            entity.Property(e => e.VoucherFile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("voucher_file");
        });

        modelBuilder.Entity<PurchaseProduct>(entity =>
        {
            entity.HasKey(e => e.PurProdCode).HasName("PK_purchase product");

            entity.ToTable("purchase_product");

            entity.Property(e => e.PurProdCode)
                .ValueGeneratedNever()
                .HasColumnName("pur_prod_code");
            entity.Property(e => e.BatchNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("batch_no");
            entity.Property(e => e.MfgDate)
                .HasColumnType("datetime")
                .HasColumnName("mfg_date");
            entity.Property(e => e.PurCode).HasColumnName("pur_code");
        });

        modelBuilder.Entity<Return>(entity =>
        {
            entity.ToTable("returns");

            entity.Property(e => e.ReturnId)
                .ValueGeneratedNever()
                .HasColumnName("return_id");
            entity.Property(e => e.ProductCode).HasColumnName("product_code");
            entity.Property(e => e.Quantity)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("quantity");
            entity.Property(e => e.ReturnAmt)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("return_amt");
            entity.Property(e => e.ReturnDate)
                .HasColumnType("datetime")
                .HasColumnName("return_date");
            entity.Property(e => e.ReturnType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("return_type");
            entity.Property(e => e.SalesCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sales_code");
            entity.Property(e => e.SalesDate)
                .HasColumnType("datetime")
                .HasColumnName("sales_date");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SalesCode);

            entity.ToTable("sales");

            entity.Property(e => e.SalesCode)
                .ValueGeneratedNever()
                .HasColumnName("sales_code");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.NetAmt)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("net_amt");
            entity.Property(e => e.PaidAmt)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("paid_amt");
            entity.Property(e => e.SalesDate)
                .HasColumnType("datetime")
                .HasColumnName("sales_date");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<SalesProduct>(entity =>
        {
            entity.HasKey(e => e.SalesProCode).HasName("PK_sales product");

            entity.ToTable("sales_product");

            entity.Property(e => e.SalesProCode)
                .ValueGeneratedNever()
                .HasColumnName("sales_pro_code");
            entity.Property(e => e.ProdCode).HasColumnName("prod_code");
            entity.Property(e => e.Quantity)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("rate");
            entity.Property(e => e.SalesCode).HasColumnName("sales_code");
        });

        modelBuilder.Entity<StockLevel>(entity =>
        {
            entity.HasKey(e => e.StkId);

            entity.ToTable("stock_level");

            entity.Property(e => e.StkId)
                .ValueGeneratedNever()
                .HasColumnName("stk_id");
            entity.Property(e => e.AvailableStock)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("available_stock");
            entity.Property(e => e.ProductCodes).HasColumnName("product_codes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
