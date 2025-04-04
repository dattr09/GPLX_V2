using GPLX.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GPLX.Controllers
{
    [Authorize]
    public class LoaiGplxController : Controller
    {
        private readonly AppDBContext _context;

        public LoaiGplxController(AppDBContext context)
        {
            _context = context;
        }

        // GET: LoaiGplx
        public async Task<IActionResult> Index(string MaLoai, int? DoTuoiDuocCap)
        {
            var loaiGplxQuery = _context.LoaiGplxes.AsQueryable();

            // Tìm kiếm theo Mã Loại nếu có
            if (!string.IsNullOrEmpty(MaLoai))
            {
                loaiGplxQuery = loaiGplxQuery.Where(l => l.MaLoai.Contains(MaLoai));
            }

            // Tìm kiếm theo Độ Tuổi nếu có
            if (DoTuoiDuocCap.HasValue)
            {
                loaiGplxQuery = loaiGplxQuery.Where(l => l.DoTuoiDuocCap == DoTuoiDuocCap);
            }

            var loaiGplxes = await loaiGplxQuery.ToListAsync();
            return View(loaiGplxes);
        }

        // GET: LoaiGplx/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiGplx = await _context.LoaiGplxes
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (loaiGplx == null)
            {
                return NotFound();
            }

            return View(loaiGplx);
        }

        // GET: LoaiGplx/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiGplx/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai,TenLoai,MoTa,DoTuoiDuocCap")] LoaiGplx loaiGplx)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiGplx);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiGplx);
        }

        // GET: LoaiGplx/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiGplx = await _context.LoaiGplxes.FindAsync(id);
            if (loaiGplx == null)
            {
                return NotFound();
            }
            return View(loaiGplx);
        }

        // POST: LoaiGplx/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaLoai,TenLoai,MoTa,DoTuoiDuocCap")] LoaiGplx loaiGplx)
        {
            if (id != loaiGplx.MaLoai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiGplx);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiGplxExists(loaiGplx.MaLoai))
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
            return View(loaiGplx);
        }

        // GET: LoaiGplx/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiGplx = await _context.LoaiGplxes
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (loaiGplx == null)
            {
                return NotFound();
            }

            return View(loaiGplx);
        }

        // POST: LoaiGplx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var loaiGplx = await _context.LoaiGplxes.FindAsync(id);
            if (loaiGplx != null)
            {
                _context.LoaiGplxes.Remove(loaiGplx);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiGplxExists(string id)
        {
            return _context.LoaiGplxes.Any(e => e.MaLoai == id);
        }
    }
}
