using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GPLX.Models;
using Microsoft.AspNetCore.Authorization;

namespace GPLX.Controllers
{
    [Authorize]
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
                try
                {
                    _context.Add(congDan);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Thêm công dân thành công!";
                    return RedirectToAction(nameof(Index)); // Chuyển hướng đến Index
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Lỗi khi thêm công dân: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Dữ liệu nhập vào không hợp lệ!";
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