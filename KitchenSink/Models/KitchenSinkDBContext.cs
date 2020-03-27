using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KitchenSink.Models
{
    public partial class KitchenSinkDBContext : DbContext
    {
        public KitchenSinkDBContext()
        {
        }

        public KitchenSinkDBContext(DbContextOptions<KitchenSinkDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Cuisine> Cuisine { get; set; }
        public virtual DbSet<Drinks> Drinks { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Recommendation> Recommendation { get; set; }
        public virtual DbSet<UserItems> UserItems { get; set; }
        public virtual DbSet<UserPreferences> UserPreferences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=kitchensink.database.windows.net,1433;Database=;Initial Catalog=KitchenSinkDB;Persist Security Info=False;User ID=KitchenSink;Password=KSink2020!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Cuisine>(entity =>
            {
                entity.Property(e => e.Cuisine1)
                    .IsRequired()
                    .HasColumnName("Cuisine")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Drinks>(entity =>
            {
                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NonAlcoholic).HasColumnName("Non-Alcoholic");
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.Property(e => e.DbgenreId).HasColumnName("DBGenreId");

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Recommendation>(entity =>
            {
                entity.HasOne(d => d.Drink)
                    .WithMany(p => p.Recommendation)
                    .HasForeignKey(d => d.DrinkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recommendation_Drink");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Recommendation)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recommendation_Genre");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Recommendation)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recommendation_Cuisine");
            });

            modelBuilder.Entity<UserItems>(entity =>
            {
                entity.HasKey(e => e.UserItems1);

                entity.Property(e => e.UserItems1)
                    .HasColumnName("UserItems")
                    .ValueGeneratedNever();

                entity.Property(e => e.DrinkId)
                    .HasColumnName("DrinkID")
                    .HasMaxLength(50);

                entity.Property(e => e.MovieId)
                    .HasColumnName("MovieID")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.RecipeId)
                    .HasColumnName("RecipeID")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserItems_AspNetUsers");
            });

            modelBuilder.Entity<UserPreferences>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.ExcludedCuisine).HasMaxLength(50);

                entity.Property(e => e.ExcludedDrink).HasMaxLength(50);

                entity.Property(e => e.ExcludedGenre).HasMaxLength(50);

                entity.Property(e => e.SavedDrink).HasMaxLength(50);

                entity.Property(e => e.SavedMovie)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.SavedRecipe).HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithOne(p => p.UserPreferences)
                    .HasForeignKey<UserPreferences>(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPreferences_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
