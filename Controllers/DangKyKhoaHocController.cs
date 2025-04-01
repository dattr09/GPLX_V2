using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GPLX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GPLX.Controllers
{
    public class DangKyKhoaHocController : Controller
    {
        private readonly AppDBContext _context;

        public DangKyKhoaHocController(AppDBContext context)
        {
            _context = context;
        }

        // GET: DangKyKhoaHoc
        public async Task<IActionResult> Index()
        {
            var dangKyKhoaHocs = _context.DangKyKhoaHocs.Include(d => d.MaLopNavigation);
            return View(await dangKyKhoaHocs.ToListAsync());
        }

        // GET: DangKyKhoaHoc/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var dangKyKhoaHoc = await _context.DangKyKhoaHocs
                .Include(d => d.MaLopNavigation)
                .FirstOrDefaultAsync(m => m.MaDkkh == id);
            if (dangKyKhoaHoc == null)
            {
                return NotFound();
            }

            return View(dangKyKhoaHoc);
        }

        // GET: DangKyKhoaHoc/Create
        public IActionResult Create()
        {
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "DiaDiem");
            return View();
        }

        // POST: DangKyKhoaHoc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDkkh,Cccd,MaLop,NgayDangKy,TrangThaiDangKy,GhiChu")] DangKyKhoaHoc dangKyKhoaHoc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(dangKyKhoaHoc);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Đăng ký khóa học thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Lỗi khi đăng ký khóa học: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ!";
            }

            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "DiaDiem", dangKyKhoaHoc.MaLop);
            return View(dangKyKhoaHoc);
        }

        // GET: DangKyKhoaHoc/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var dangKyKhoaHoc = await _context.DangKyKhoaHocs.FindAsync(id);
            if (dangKyKhoaHoc == null)
            {
                return NotFound();
            }
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "DiaDiem", dangKyKhoaHoc.MaLop);
            return View(dangKyKhoaHoc);
        }

        // POST: DangKyKhoaHoc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDkkh,Cccd,MaLop,NgayDangKy,TrangThaiDangKy,GhiChu")] DangKyKhoaHoc dangKyKhoaHoc)
        {
            if (id != dangKyKhoaHoc.MaDkkh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dangKyKhoaHoc);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật thông tin đăng ký thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DangKyKhoaHocExists(dangKyKhoaHoc.MaDkkh))
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
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "DiaDiem", dangKyKhoaHoc.MaLop);
            return View(dangKyKhoaHoc);
        }

        // GET: DangKyKhoaHoc/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var dangKyKhoaHoc = await _context.DangKyKhoaHocs
                .Include(d => d.MaLopNavigation)
                .FirstOrDefaultAsync(m => m.MaDkkh == id);
            if (dangKyKhoaHoc == null)
            {
                return NotFound();
            }

            return View(dangKyKhoaHoc);
        }

        // POST: DangKyKhoaHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dangKyKhoaHoc = await _context.DangKyKhoaHocs.FindAsync(id);
            if (dangKyKhoaHoc == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy thông tin đăng ký!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.DangKyKhoaHocs.Remove(dangKyKhoaHoc);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa đăng ký khóa học thành công!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Lỗi khi xóa đăng ký khóa học!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DangKyKhoaHocExists(int id)
        {
            return _context.DangKyKhoaHocs.Any(e => e.MaDkkh == id);
        }
    }
}
