using System;
using System.ComponentModel.DataAnnotations;
namespace PointOfSales.Models.Context
{
	public class Kategori
	{
		[Key]
		public int Id_kategori { get; set; }
		public string Nama_Kategori { get; set; }

		public virtual ICollection<Produk>? Produk { get; set; }
	}
}

