using System;
using Microsoft.EntityFrameworkCore;
using PointOfSales.Models.Context;
namespace PointOfSales.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Produk> Produk { get; set; }
        public DbSet<Kategori> Kategori { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produk>()
                .HasOne<Kategori>(c => c.Kategori)
                .WithMany(i => i.Produk)
                .HasForeignKey(f => f.KategoriId);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is Produk && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Modified)
                {
                    ((Produk)entity.Entity).UpdateAt = now;
                }
                ((Produk)entity.Entity).CreateAt = now;
            }
        }
    }
}

