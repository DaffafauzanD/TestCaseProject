using System;
using Microsoft.EntityFrameworkCore;
using PointOfSales.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PointOfSales.Controllers
{
	public class ProdukController : Controller
	{
        private readonly ApplicationDbContext _context;

        public ProdukController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Create()
		{
			ViewData["KategoriId"] = new SelectList(_context.Kategori, "Nama_Kategori", "Nama_Kategori");
            return View();
		}

		//public IActionResult Create()
		//{
		//	return View();
		//}

		public IActionResult List()
		{
			var produk = _context.Produk.Include(i => i.Kategori).ToList();
			return View(produk);
		}

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}

