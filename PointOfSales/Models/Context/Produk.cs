using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace PointOfSales.Models.Context
{
	public class Produk
	{
		[Key]
		public int Id { get; set; }
		public string? Nama_produk { get; set; }
		public int Harga { get; set; }
		public int KategoriId { get; set; }

		public virtual Kategori? Kategori { get; set; }

		
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

	}
}

