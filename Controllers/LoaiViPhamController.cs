using GPLX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GPLX.Controllers
{
    public class LoaiViPhamController : Controller
    {
        private readonly AppDBContext _context;

        public LoaiViPhamController(AppDBContext context)
        {
            _context = context;
        }

        // GET: LoaiViPham
        public async Task<IActionResult> Index()
        {
            var loaiViPhams = await _context.LoaiViPhams.ToListAsync();
            return View(loaiViPhams);
        }

        // GET: LoaiViPham/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var loaiViPham = await _context.LoaiViPhams
                .FirstOrDefaultAsync(m => m.MaLoaiViPham == id);
            if (loaiViPham == null)
            {
                return NotFound();
            }

            return View(loaiViPham);
        }

        // GET: LoaiViPham/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiViPham/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenViPham,MucPhat")] LoaiViPham loaiViPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiViPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiViPham);
        }

        // GET: LoaiViPham/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var loaiViPham = await _context.LoaiViPhams.FindAsync(id);
            if (loaiViPham == null)
            {
                return NotFound();
            }
            return View(loaiViPham);
        }

        // POST: LoaiViPham/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoaiViPham,TenViPham,MucPhat")] LoaiViPham loaiViPham)
        {
            if (id != loaiViPham.MaLoaiViPham)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiViPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiViPhamExists(loaiViPham.MaLoaiViPham))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loaiViPham);
        }

        // GET: LoaiViPham/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var loaiViPham = await _context.LoaiViPhams
                .FirstOrDefaultAsync(m => m.MaLoaiViPham == id);
            if (loaiViPham == null)
            {
                return NotFound();
            }

            return View(loaiViPham);
        }

        // POST: LoaiViPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiViPham = await _context.LoaiViPhams.FindAsync(id);
            if (loaiViPham != null)
            {
                _context.LoaiViPhams.Remove(loaiViPham);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiViPhamExists(int id)
        {
            return _context.LoaiViPhams.Any(e => e.MaLoaiViPham == id);
        }
    }
}
