using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GPLX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GPLX.Controllers
{
    public class KhoaHocController : Controller
    {
        private readonly AppDBContext _context;

        public KhoaHocController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.KhoaHocs.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc == null)
            {
                return NotFound();
            }
            return View(khoaHoc);
        }

        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.LoaiGplxes, "MaLoai", "TenLoai");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenKhoaHoc,ThoiGianHoc,HocPhi,MoTa,MaLoai")] KhoaHoc khoaHoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khoaHoc);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm khóa học thành công!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiGplxes, "MaLoai", "TenLoai", khoaHoc.MaLoai);
            TempData["ErrorMessage"] = "Dữ liệu nhập vào không hợp lệ!";
            return View(khoaHoc);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc == null) return NotFound();

            ViewData["MaLoai"] = new SelectList(_context.LoaiGplxes, "MaLoai", "TenLoai", khoaHoc.MaLoai);
            return View(khoaHoc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKhoaHoc,TenKhoaHoc,ThoiGianHoc,HocPhi,MoTa,MaLoai")] KhoaHoc khoaHoc)
        {
            if (id != khoaHoc.MaKhoaHoc) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["MaLoai"] = new SelectList(_context.LoaiGplxes, "MaLoai", "TenLoai", khoaHoc.MaLoai);
                return View(khoaHoc);
            }

            try
            {
                _context.Update(khoaHoc);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.KhoaHocs.Any(e => e.MaKhoaHoc == id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc == null)
            {
                return NotFound();
            }
            return View(khoaHoc);
        }

   [HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var khoaHoc = await _context.KhoaHocs.FindAsync(id);

    if (khoaHoc == null)
    {
        TempData["ErrorMessage"] = "Không tìm thấy khóa học!";
        return RedirectToAction(nameof(Index));
    }

    try
    {
        _context.KhoaHocs.Remove(khoaHoc);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Xóa khóa học thành công!";
    }
    catch (DbUpdateException)
    {
        TempData["ErrorMessage"] = "Không thể xóa khóa học vì có dữ liệu liên quan!";
    }

    return RedirectToAction(nameof(Index));
}


        private bool KhoaHocExists(int id)
        {
            return _context.KhoaHocs.Any(e => e.MaKhoaHoc == id);
        }
    }
}
