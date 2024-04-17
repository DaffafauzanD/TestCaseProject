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
    }
}

