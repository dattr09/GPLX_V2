using GPLX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace GPLX.Controllers
{
    public class KetQuaThiGplxController : Controller
    {
        private readonly AppDBContext _context;

        public KetQuaThiGplxController(AppDBContext context)
        {
            _context = context;
        }

        // GET: KetQuaThiGplx
        public async Task<IActionResult> Index()
        {
            var ketQuaThiGplxes = await _context.KetQuaThiGplxes.Include(k => k.MaDkthiGplxNavigation).ToListAsync();
            return View(ketQuaThiGplxes);
        }

        // GET: KetQuaThiGplx/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ketQuaThiGplx = await _context.KetQuaThiGplxes
                .Include(k => k.MaDkthiGplxNavigation)
                .FirstOrDefaultAsync(m => m.MaKetQua == id);
            if (ketQuaThiGplx == null)
            {
                return NotFound();
            }

            return View(ketQuaThiGplx);
        }

        // GET: KetQuaThiGplx/Create
        public IActionResult Create()
        {
            ViewData["MaDkthiGplx"] = new SelectList(_context.DkthiGplxes, "MaDkthiGplx", "MaDkthiGplx");
            return View();
        }

        // POST: KetQuaThiGplx/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKetQua,MaDkthiGplx,DiemLyThuyet,DiemThucHanh,DiemMoPhong,DiemDuongTruong,GhiChu,KetQua")] KetQuaThiGplx ketQuaThiGplx)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ketQuaThiGplx);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDkthiGplx"] = new SelectList(_context.DkthiGplxes, "MaDkthiGplx", "MaDkthiGplx", ketQuaThiGplx.MaDkthiGplx);
            return View(ketQuaThiGplx);
        }

        // GET: KetQuaThiGplx/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ketQuaThiGplx = await _context.KetQuaThiGplxes.FindAsync(id);
            if (ketQuaThiGplx == null)
            {
                return NotFound();
            }
            ViewData["MaDkthiGplx"] = new SelectList(_context.DkthiGplxes, "MaDkthiGplx", "MaDkthiGplx", ketQuaThiGplx.MaDkthiGplx);
            return View(ketQuaThiGplx);
        }

        // POST: KetQuaThiGplx/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKetQua,MaDkthiGplx,DiemLyThuyet,DiemThucHanh,DiemMoPhong,DiemDuongTruong,GhiChu,KetQua")] KetQuaThiGplx ketQuaThiGplx)
        {
            if (id != ketQuaThiGplx.MaKetQua)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ketQuaThiGplx);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KetQuaThiGplxExists(ketQuaThiGplx.MaKetQua))
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
            ViewData["MaDkthiGplx"] = new SelectList(_context.DkthiGplxes, "MaDkthiGplx", "MaDkthiGplx", ketQuaThiGplx.MaDkthiGplx);
            return View(ketQuaThiGplx);
        }

        // GET: KetQuaThiGplx/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ketQuaThiGplx = await _context.KetQuaThiGplxes
                .Include(k => k.MaDkthiGplxNavigation)
                .FirstOrDefaultAsync(m => m.MaKetQua == id);
            if (ketQuaThiGplx == null)
            {
                return NotFound();
            }

            return View(ketQuaThiGplx);
        }

        // POST: KetQuaThiGplx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ketQuaThiGplx = await _context.KetQuaThiGplxes.FindAsync(id);
            _context.KetQuaThiGplxes.Remove(ketQuaThiGplx);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KetQuaThiGplxExists(string id)
        {
            return _context.KetQuaThiGplxes.Any(e => e.MaKetQua == id);
        }
    }
}
