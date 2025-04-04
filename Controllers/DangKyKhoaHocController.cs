using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using GPLX.Models;

namespace GPLX.Controllers
{
    public class DangKyKhoaHocController : Controller
    {
        private readonly AppDBContext _context;

        public DangKyKhoaHocController(AppDBContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách đăng ký
        public async Task<IActionResult> Index()
        {
            var dangKyKhoaHocs = _context.DangKyKhoaHocs.Include(d => d.MaLopNavigation);
            return View(await dangKyKhoaHocs.ToListAsync());
        }

        // Chi tiết
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) return NotFound();

            var dangKy = await _context.DangKyKhoaHocs
                .Include(d => d.MaLopNavigation)
                .FirstOrDefaultAsync(m => m.MaDkkh == id);

            if (dangKy == null) return NotFound();

            return View(dangKy);
        }

        // GET: Tạo mới
        public IActionResult Create()
        {
            PopulateViewData();
            return View(new DangKyKhoaHoc());
        }

        // POST: Tạo mới
       [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Cccd,MaLop,NgayDangKy,TrangThaiDangKy,GhiChu")] DangKyKhoaHoc dangKyKhoaHoc)
{
    if (ModelState.IsValid)
    {
        // Kiểm tra nếu giá trị của TrangThaiDangKy hợp lệ
        var validStatuses = new[] { "Đã đăng ký", "Đã huỷ", "Chờ xác nhận" };
        if (!validStatuses.Contains(dangKyKhoaHoc.TrangThaiDangKy))
        {
            ModelState.AddModelError("TrangThaiDangKy", "Trạng thái không hợp lệ. Vui lòng chọn một trạng thái hợp lệ.");
            return View(dangKyKhoaHoc);
        }

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

        // GET: Sửa
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();

            var model = await _context.DangKyKhoaHocs.FindAsync(id);
            if (model == null) return NotFound();

            PopulateViewData(model);
            return View(model);
        }

        // POST: Sửa
        [HttpPost]
public async Task<IActionResult> Edit(int id, DangKyKhoaHoc model)
{
    if (ModelState.IsValid)
    {
        // Kiểm tra nếu giá trị của TrangThaiDangKy hợp lệ
        var validStatusValues = new List<string> { "Đã đăng ký", "Đã huỷ", "Chờ xác nhận" };
        if (model.TrangThaiDangKy == null || !validStatusValues.Contains(model.TrangThaiDangKy))
        {
            ModelState.AddModelError("TrangThaiDangKy", "Trạng thái không hợp lệ.");
            return View(model);
        }

        try
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DangKyKhoaHocExists(model.MaDkkh))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
    }
    return View(model);
}


        // GET: Xóa
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.DangKyKhoaHocs
                .Include(d => d.MaLopNavigation)
                .FirstOrDefaultAsync(m => m.MaDkkh == id);

            if (model == null) return NotFound();

            return View(model);
        }

        // POST: Xác nhận xóa
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.DangKyKhoaHocs.FindAsync(id);
            if (model != null)
            {
                _context.DangKyKhoaHocs.Remove(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy bản ghi để xóa.";
            }

            return RedirectToAction(nameof(Index));
        }

        // Kiểm tra tồn tại
        private bool DangKyKhoaHocExists(int id)
        {
            return _context.DangKyKhoaHocs.Any(e => e.MaDkkh == id);
        }

        // Hàm load dữ liệu cho dropdown
        private void PopulateViewData(DangKyKhoaHoc? model = null)
        {
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "DiaDiem", model?.MaLop);
            ViewData["CongDanList"] = _context.CongDans.ToList();
        }
    }
}
