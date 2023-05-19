using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiTemplate
{
    public partial class Db_TemplateContext : DbContext
    {
        public Db_TemplateContext()
        {
        }

        public Db_TemplateContext(DbContextOptions<Db_TemplateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<CuatomerArticle> CuatomerArticles { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreArticle> StoreArticles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Db_Template;User=sa;Password=12345;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Acount)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("acount");

                entity.Property(e => e.Pount)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("pount");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__accounts__roleId__2B3F6F97");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("articles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Code)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Stock).HasColumnName("stock");
            });

            modelBuilder.Entity<CuatomerArticle>(entity =>
            {
                entity.ToTable("cuatomerArticle");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Article).HasColumnName("article");

                entity.Property(e => e.Cuatomer).HasColumnName("cuatomer");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.HasOne(d => d.ArticleNavigation)
                    .WithMany(p => p.CuatomerArticles)
                    .HasForeignKey(d => d.Article)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__cuatomerA__artic__00200768");

                entity.HasOne(d => d.CuatomerNavigation)
                    .WithMany(p => p.CuatomerArticles)
                    .HasForeignKey(d => d.Cuatomer)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__cuatomerA__cuato__7F2BE32F");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Account).HasColumnName("account");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Addres)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("addres");

                entity.Property(e => e.LastNames)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("lastNames");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__customers__accou__70DDC3D8");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("rols");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("stores");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Addres)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("addres");

                entity.Property(e => e.Branch)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("branch");
            });

            modelBuilder.Entity<StoreArticle>(entity =>
            {
                entity.ToTable("storeArticle");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Article).HasColumnName("article");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Store).HasColumnName("store");

                entity.HasOne(d => d.ArticleNavigation)
                    .WithMany(p => p.StoreArticles)
                    .HasForeignKey(d => d.Article)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__storeArti__artic__7B5B524B");

                entity.HasOne(d => d.StoreNavigation)
                    .WithMany(p => p.StoreArticles)
                    .HasForeignKey(d => d.Store)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__storeArti__store__7A672E12");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
