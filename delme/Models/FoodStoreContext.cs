using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace delme.Models
{
    public partial class FoodStoreContext : DbContext
    {
        public FoodStoreContext()
        {
        }

        public FoodStoreContext(DbContextOptions<FoodStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInvoice> ProductInvoices { get; set; }
        public virtual DbSet<ProductPurchaseOrder> ProductPurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DANIELNAD4B8\\SQLEXPRESS;Database=FoodStore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Building>(entity =>
            {
                entity.HasKey(e => new { e.BuildingName, e.UnitNum })
                    .HasName("PK__Building__14E071A09DC55F41");

                entity.ToTable("Building");

                entity.Property(e => e.BuildingName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("building_name");

                entity.Property(e => e.UnitNum).HasColumnName("unit_num");

                entity.Property(e => e.Capacity).HasColumnName("capacity");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Branch)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("branch");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.HasOne(d => d.BranchNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Branch)
                    .HasConstraintName("FK__Employee__branch__3F466844");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceNum)
                    .HasName("PK__Invoice__F9EE1383E14B9999");

                entity.ToTable("Invoice");

                entity.Property(e => e.InvoiceNum).HasColumnName("invoiceNum");

                entity.Property(e => e.Branch)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("branch");

                entity.HasOne(d => d.BranchNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.Branch)
                    .HasConstraintName("FK__Invoice__branch__31EC6D26");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(e => e.Mfg)
                    .HasName("PK__Manufact__DF50190D2A08C3DE");

                entity.ToTable("Manufacturer");

                entity.Property(e => e.Mfg)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("mfg");

                entity.Property(e => e.MfgDiscount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("mfgDiscount");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.Mfg)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("mfg");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Vendor)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("vendor");

                entity.HasOne(d => d.MfgNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Mfg)
                    .HasConstraintName("FK__Product__mfg__286302EC");

                entity.HasOne(d => d.VendorNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Vendor)
                    .HasConstraintName("FK__Product__vendor__29572725");
            });

            modelBuilder.Entity<ProductInvoice>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.InvoiceNum })
                    .HasName("PK__ProductI__028E3072B3B2EEE1");

                entity.ToTable("ProductInvoice");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.InvoiceNum).HasColumnName("invoiceNum");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.InvoiceNumNavigation)
                    .WithMany(p => p.ProductInvoices)
                    .HasForeignKey(d => d.InvoiceNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductIn__invoi__35BCFE0A");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInvoices)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductIn__produ__34C8D9D1");
            });

            modelBuilder.Entity<ProductPurchaseOrder>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.PoNum })
                    .HasName("PK__ProductP__6DEC04077F9BCCDC");

                entity.ToTable("ProductPurchaseOrder");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.PoNum).HasColumnName("po_num");

                entity.HasOne(d => d.PoNumNavigation)
                    .WithMany(p => p.ProductPurchaseOrders)
                    .HasForeignKey(d => d.PoNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductPu__po_nu__3C69FB99");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPurchaseOrders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductPu__produ__3B75D760");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.PoNum)
                    .HasName("PK__Purchase__0FCD54D5125DA8DC");

                entity.ToTable("PurchaseOrder");

                entity.Property(e => e.PoNum).HasColumnName("po_num");

                entity.Property(e => e.Branch)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("branch");

                entity.HasOne(d => d.BranchNavigation)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.Branch)
                    .HasConstraintName("FK__PurchaseO__branc__38996AB5");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.Branch)
                    .HasName("PK__Store__1F843124B4BA74AF");

                entity.ToTable("Store");

                entity.Property(e => e.Branch)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("branch");

                entity.Property(e => e.BuildingName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("building_name");

                entity.Property(e => e.Region)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("region");

                entity.Property(e => e.UnitNum).HasColumnName("unit_num");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => new { d.BuildingName, d.UnitNum })
                    .HasConstraintName("FK__Store__2F10007B");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Vendor)
                    .HasName("PK__Supplier__42A1EB1C9CEE315E");

                entity.ToTable("Supplier");

                entity.Property(e => e.Vendor)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("vendor");

                entity.Property(e => e.SupplierEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("supplier_email");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
