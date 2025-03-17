using GPLX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GPLX.Controllers
{
    public class TrungTamSatHachController : Controller
    {
        private readonly AppDBContext _context;

        public TrungTamSatHachController(AppDBContext context)
        {
            _context = context;
        }

        // GET: TrungTamSatHach
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrungTamSatHaches.ToListAsync());
        }

        // GET: TrungTamSatHach/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trungTamSatHach = await _context.TrungTamSatHaches
                .FirstOrDefaultAsync(m => m.MaTtsh == id);
            if (trungTamSatHach == null)
            {
                return NotFound();
            }

            return View(trungTamSatHach);
        }

        // GET: TrungTamSatHach/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrungTamSatHach/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTtsh,TenTrungTam,DiaChi,SoDt")] TrungTamSatHach trungTamSatHach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trungTamSatHach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trungTamSatHach);
        }

        // GET: TrungTamSatHach/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trungTamSatHach = await _context.TrungTamSatHaches.FindAsync(id);
            if (trungTamSatHach == null)
            {
                return NotFound();
            }
            return View(trungTamSatHach);
        }

        // POST: TrungTamSatHach/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTtsh,TenTrungTam,DiaChi,SoDt")] TrungTamSatHach trungTamSatHach)
        {
            if (id != trungTamSatHach.MaTtsh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trungTamSatHach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrungTamSatHachExists(trungTamSatHach.MaTtsh))
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
            return View(trungTamSatHach);
        }

        // GET: TrungTamSatHach/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trungTamSatHach = await _context.TrungTamSatHaches
                .FirstOrDefaultAsync(m => m.MaTtsh == id);
            if (trungTamSatHach == null)
            {
                return NotFound();
            }

            return View(trungTamSatHach);
        }

        // POST: TrungTamSatHach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trungTamSatHach = await _context.TrungTamSatHaches.FindAsync(id);
            if (trungTamSatHach == null)
            {
                TempData["ErrorMessage"] = "Trung tâm không tồn tại!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.TrungTamSatHaches.Remove(trungTamSatHach);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa trung tâm thành công!";
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "Không thể xóa! Trung tâm này có dữ liệu liên quan.";
            }

            return RedirectToAction(nameof(Index));
        }


        private bool TrungTamSatHachExists(int id)
        {
            return _context.TrungTamSatHaches.Any(e => e.MaTtsh == id);
        }
    }
}
