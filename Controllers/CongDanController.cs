using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GPLX.Models;

namespace GPLX.Controllers
{
    public class CongDanController : Controller
    {
        private readonly AppDBContext _context;

        public CongDanController(AppDBContext context)
        {
            _context = context;
        }

        // GET: CongDan
        public async Task<IActionResult> Index()
        {
            return View(await _context.CongDans.ToListAsync());
        }

        // GET: CongDan/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var congDan = await _context.CongDans
                .FirstOrDefaultAsync(m => m.Cccd == id);
            if (congDan == null)
            {
                return NotFound();
            }

            return View(congDan);
        }

        // GET: CongDan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CongDan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cccd,HoTen,NgaySinh,GioiTinh,QuocTich,DiaChi,SoDienThoai")] CongDan congDan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(congDan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(congDan);
        }

        // GET: CongDan/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var congDan = await _context.CongDans.FindAsync(id);
            if (congDan == null)
            {
                return NotFound();
            }
            return View(congDan);
        }

        // POST: CongDan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Cccd,HoTen,NgaySinh,GioiTinh,QuocTich,DiaChi,SoDienThoai")] CongDan congDan)
        {
            if (id != congDan.Cccd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(congDan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CongDanExists(congDan.Cccd))
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
            return View(congDan);
        }

        // GET: CongDan/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var congDan = await _context.CongDans
                .FirstOrDefaultAsync(m => m.Cccd == id);
            if (congDan == null)
            {
                return NotFound();
            }

            return View(congDan);
        }

        // POST: CongDan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID không hợp lệ!";
                return RedirectToAction(nameof(Index));
            }

            var congDan = await _context.CongDans.FindAsync(id);
            if (congDan == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy công dân!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.CongDans.Remove(congDan);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa thành công!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Lỗi! Không thể xóa do ràng buộc dữ liệu.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CongDanExists(string id)
        {
            return _context.CongDans.Any(e => e.Cccd == id);
        }
    }
}