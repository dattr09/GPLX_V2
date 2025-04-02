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

        // GET: DangKyKhoaHoc/  /5
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

     public IActionResult Create()
{
    var model = new DangKyKhoaHoc(); // Tạo đối tượng mới cho DangKyKhoaHoc

    // Lấy dữ liệu công dân và lớp học từ cơ sở dữ liệu
    var congDanList = _context.CongDans.ToList(); // Lấy danh sách công dân
    var lopHocList = _context.LopHocs.ToList();   // Lấy danh sách lớp học

    // Kiểm tra xem dữ liệu có null không
    if (congDanList == null || lopHocList == null)
    {
        TempData["ErrorMessage"] = "Không có dữ liệu công dân hoặc lớp học.";
        return View(model);
    }

    // Truyền danh sách công dân và lớp học vào ViewData
    ViewData["CongDanList"] = congDanList;
    ViewData["MaLop"] = new SelectList(lopHocList, "MaLop", "DiaDiem");

    // Truyền vào View model mới
    return View(model);
}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cccd,MaLop,NgayDangKy,TrangThaiDangKy,GhiChu")] DangKyKhoaHoc dangKyKhoaHoc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(dangKyKhoaHoc); // Thêm đăng ký khóa học vào cơ sở dữ liệu
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Đăng ký khóa học thành công!";
                    return RedirectToAction(nameof(Index)); // Điều hướng đến trang Index
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

            // Truyền lại danh sách công dân và lớp học vào ViewData khi có lỗi
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "DiaDiem", dangKyKhoaHoc.MaLop);
            ViewData["CongDanList"] = _context.CongDans.ToList();

            return View(dangKyKhoaHoc); // Trả lại model đã nhập
        }







        // GET: DangKyKhoaHoc/Edit/5
           // GET: DangKyKhoaHoc/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var dangKyKhoaHoc = await _context.DangKyKhoaHocs
            .Include(d => d.MaLopNavigation) // Load thông tin lớp học
            .FirstOrDefaultAsync(d => d.MaDkkh == id);

        if (dangKyKhoaHoc == null)
        {
            return NotFound();
        }

        // Kiểm tra nếu danh sách LopHocs có dữ liệu không
        var lopHocs = await _context.LopHocs.ToListAsync();
        if (lopHocs.Any())
        {
            ViewData["MaLop"] = new SelectList(lopHocs, "MaLop", "DiaDiem", dangKyKhoaHoc.MaLop);
        }
        else
        {
            ViewData["MaLop"] = new SelectList(Enumerable.Empty<SelectListItem>());
        }

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
                if (!_context.DangKyKhoaHocs.Any(e => e.MaDkkh == dangKyKhoaHoc.MaDkkh))
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
