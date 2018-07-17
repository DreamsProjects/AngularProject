using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AngularProject.Models
{
    public partial class Summer_projectContext : DbContext
    {
        public virtual DbSet<TblCategory> TblCategory { get; set; }
        public virtual DbSet<TblHighscore> TblHighscore { get; set; }
        public virtual DbSet<TblIngredient> TblIngredient { get; set; }
        public virtual DbSet<TblOrder> TblOrder { get; set; }
        public virtual DbSet<TblOrderInfo> TblOrderInfo { get; set; }
        public virtual DbSet<TblPerson> TblPerson { get; set; }
        public virtual DbSet<TblPizza> TblPizza { get; set; }
        public virtual DbSet<TblPizzaIngredients> TblPizzaIngredients { get; set; }
        public virtual DbSet<TblPost> TblPost { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=Summer project;Trusted_Connection=True;");
            }
        }

        public Summer_projectContext(DbContextOptions<Summer_projectContext> options) : base(options)
        {
        }

        public Summer_projectContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblHighscore>(entity =>
            {
                entity.HasKey(e => e.HighScoreId);

                entity.Property(e => e.HighScoreId)
                    .HasColumnName("HighScoreID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Points)
                .HasColumnName("Points")
                .ValueGeneratedNever();

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.TblHighscore)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__TblHighsc__Perso__4D94879B");
            });

            modelBuilder.Entity<TblIngredient>(entity =>
            {
                entity.HasKey(e => e.IngredientId);

                entity.Property(e => e.IngredientId)
                    .HasColumnName("IngredientID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.TblOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PersonID");

            });

            modelBuilder.Entity<TblOrderInfo>(entity =>
            {
                entity.HasKey(e => e.OrderInfoId);

                entity.Property(e => e.OrderInfoId)
                    .HasColumnName("OrderInfoID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryId)
                .HasColumnName("CategoryID")
                .ValueGeneratedNever();

                entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

                entity.Property(e => e.AmountPerId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");


                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TblOrderInfo)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TblOrderI__Order__5AEE82B9");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.TblOrderInfo)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TblOrderI__Pizza__59FA5E80");
            });

            modelBuilder.Entity<TblPerson>(entity =>
            {
                entity.HasKey(e => e.PersonId);

                entity.Property(e => e.PersonId)
                    .HasColumnName("PersonID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastLoggedIn).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                .HasMaxLength(100)
                .IsUnicode(false);
            });

            modelBuilder.Entity<TblPizza>(entity =>
            {
                entity.HasKey(e => e.PizzaId);

                entity.Property(e => e.PizzaId)
                    .HasColumnName("PizzaID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Photos)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblPizza)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TblPizza__Catego__6E01572D");
            });

            modelBuilder.Entity<TblPizzaIngredients>(entity =>
            {
                entity.HasKey(e => e.PizzaIngredientId);

                entity.Property(e => e.PizzaIngredientId)
                    .HasColumnName("PizzaIngredientID")
                    .ValueGeneratedNever();

                entity.Property(e => e.IngredientId).HasColumnName("IngredientID");

                entity.Property(e => e.IngredientName).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.PizzaName).HasMaxLength(255).IsUnicode(false);

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.TblPizzaIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .HasConstraintName("FK__TblPizzaI__Ingre__52593CB8");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.TblPizzaIngredients)
                    .HasForeignKey(d => d.PizzaId)
                    .HasConstraintName("FK__TblPizzaI__Pizza__534D60F1");
            });

            modelBuilder.Entity<TblPost>(entity =>
            {
                entity.HasKey(e => e.PostId);

                entity.Property(e => e.PostId)
                    .HasColumnName("PostID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Text)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblPost)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TblPost__Categor__5FB337D6");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.TblPost)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TblPost__PersonI__5EBF139D");
            });
        }
    }
}
