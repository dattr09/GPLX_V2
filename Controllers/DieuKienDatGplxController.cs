using GPLX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace GPLX.Controllers
{
    [Authorize]
    public class DieuKienDatGplxController : Controller
    {
        private readonly AppDBContext _context;

        public DieuKienDatGplxController(AppDBContext context)
        {
            _context = context;
        }

        // GET: DieuKienDatGplx
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.DieuKienDatGplxes.Include(d => d.MaLoaiNavigation);
            return View(await appDBContext.ToListAsync());
        }

        // GET: DieuKienDatGplx/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dieuKienDatGplx = await _context.DieuKienDatGplxes
                .Include(d => d.MaLoaiNavigation)
                .FirstOrDefaultAsync(m => m.MaDkdat == id);
            if (dieuKienDatGplx == null)
            {
                return NotFound();
            }

            return View(dieuKienDatGplx);
        }

        // GET: DieuKienDatGplx/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.LoaiGplxes, "MaLoai", "TenLoai");
            return View();
        }

        // POST: DieuKienDatGplx/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai,DiemLyThuyetDat,DiemThucHanhDat,DiemMoPhongDat,DiemDuongTruongDat,GhiChu")] DieuKienDatGplx dieuKienDatGplx)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dieuKienDatGplx);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiGplxes, "MaLoai", "TenLoai", dieuKienDatGplx.MaLoai);
            return View(dieuKienDatGplx);
        }

        // GET: DieuKienDatGplx/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dieuKienDatGplx = await _context.DieuKienDatGplxes.FindAsync(id);
            if (dieuKienDatGplx == null)
            {
                return NotFound();
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiGplxes, "MaLoai", "TenLoai", dieuKienDatGplx.MaLoai);
            return View(dieuKienDatGplx);
        }

        // POST: DieuKienDatGplx/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDkdat,MaLoai,DiemLyThuyetDat,DiemThucHanhDat,DiemMoPhongDat,DiemDuongTruongDat,GhiChu")] DieuKienDatGplx dieuKienDatGplx)
        {
            if (id != dieuKienDatGplx.MaDkdat)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dieuKienDatGplx);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DieuKienDatGplxExists(dieuKienDatGplx.MaDkdat))
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
            ViewData["MaLoai"] = new SelectList(_context.LoaiGplxes, "MaLoai", "TenLoai", dieuKienDatGplx.MaLoai);
            return View(dieuKienDatGplx);
        }

        // GET: DieuKienDatGplx/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dieuKienDatGplx = await _context.DieuKienDatGplxes
                .Include(d => d.MaLoaiNavigation)
                .FirstOrDefaultAsync(m => m.MaDkdat == id);
            if (dieuKienDatGplx == null)
            {
                return NotFound();
            }

            return View(dieuKienDatGplx);
        }

        // POST: DieuKienDatGplx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dieuKienDatGplx = await _context.DieuKienDatGplxes.FindAsync(id);
            _context.DieuKienDatGplxes.Remove(dieuKienDatGplx);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DieuKienDatGplxExists(int id)
        {
            return _context.DieuKienDatGplxes.Any(e => e.MaDkdat == id);
        }
    }
}
