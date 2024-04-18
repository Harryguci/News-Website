
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using NewsWebsite.Domain.Entities;

namespace NewsWebsite.Migrations
{
    public partial class NewsDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<NewsPost> NewsPosts { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<RecentReadPost> RecentReadPosts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public NewsDbContext() : base() { }
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
            });

            modelBuilder.Entity<NewsPost>(entity =>
            {
                entity.ToTable("NewsPost");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notifications");
            });

            modelBuilder.Entity<RecentReadPost>(entity =>
            {
                entity.ToTable("RecentReadPosts");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
                entity.ToTable("Categories");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
