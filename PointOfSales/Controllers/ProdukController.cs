using Microsoft.EntityFrameworkCore;
using PointOfSales.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PointOfSales.Models.Context;


namespace PointOfSales.Controllers
{
    public class ProdukController : Controller
	{
        private readonly ApplicationDbContext _context;

        public ProdukController(ApplicationDbContext context)
		{
			_context = context;
		}

        public void ShowNamaKategori()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id_kategori", "Nama_Kategori");
        }

        public IActionResult Create()
		{
			ShowNamaKategori();
            return View();
		}

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Nama_produk,Harga,KategoriId,CreateAt,UpdateAt")] Produk produk)
		{
			if (ModelState.IsValid)
			{
                _context.Add(produk);
                
                await _context.SaveChangesAsync();
                return RedirectToAction("List", "Produk");
            }
            return View("List",produk);
		}

		public IActionResult List()
        {
            var produk = _context.Produk.Include(i => i.Kategori).ToList();
            return View(produk);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var produk = await _context.Produk.FindAsync(id);
            if(_context == null)
            {
                return NotFound();
            }

            ShowNamaKategori();
            return View(produk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Nama_produk, Harga, KategoriId, CreateAt, UpdateAt")] Produk produk)
        {
            if (id != produk.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdaProduk(produk.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("List", "Produk");
            }
            ShowNamaKategori();
            return View(produk);
       
        }

        private bool AdaProduk(int id)
        {
            return (_context.Produk?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produk == null)
            {
                return NotFound();
            }

            var produk = await _context.Produk
                .Include(p => p.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produk == null)
            {
                return NotFound();
            }

            return View(produk);
        }

        // POST: ProdukTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produk == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Produk'  is null.");
            }
            var produk = await _context.Produk.FindAsync(id);
            if (produk != null)
            {
                _context.Produk.Remove(produk);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("List", "Produk");
        }
    }
}

