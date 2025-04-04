using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GPLX.Models;
using System.Threading.Tasks;
using System.Linq;

namespace GPLX.Controllers
{
    public class LopHocController : Controller
    {
        private readonly AppDBContext _context;

        public LopHocController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var lopHocs = await _context.LopHocs
                .Include(l => l.MaKhoaHocNavigation)
                .Include(l => l.MaGvNavigation)
                .ToListAsync();
            return View(lopHocs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var lopHoc = await _context.LopHocs
                .Include(l => l.MaKhoaHocNavigation)
                .Include(l => l.MaGvNavigation)
                .FirstOrDefaultAsync(m => m.MaLop == id);

            if (lopHoc == null)
                return NotFound();

            return View(lopHoc);
        }

     public IActionResult Create()
{
    ViewData["GiangVien"] = _context.GiangViens.ToList();
    ViewData["KhoaHoc"] = _context.KhoaHocs.ToList();
    return View();
}

    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("NgayBatDau,NgayKetThuc,DiaDiem,ThoiGianHocTrongTuan,SoLuongToiDa,GhiChu,TrangThai,IsOnline,MaKhoaHoc,MaGv")] LopHoc lopHoc)
{
    // Kiểm tra lỗi ModelState trước
    if (!ModelState.IsValid)
    {
        Console.WriteLine("❌ ModelState không hợp lệ!");
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine("Lỗi: " + error.ErrorMessage);
        }

        // Tải lại danh sách giảng viên và khóa học khi có lỗi
        ViewData["GiangVien"] = await _context.GiangViens.ToListAsync();
        ViewData["KhoaHoc"] = await _context.KhoaHocs.ToListAsync();
        return View(lopHoc);
    }

    try
    {
        // Tìm giảng viên và khóa học từ khóa ngoại
        var giangVien = await _context.GiangViens.FindAsync(lopHoc.MaGv);
        var khoaHoc = await _context.KhoaHocs.FindAsync(lopHoc.MaKhoaHoc);

        // Kiểm tra xem giảng viên và khóa học có tồn tại hay không
        if (giangVien == null || khoaHoc == null)
        {
            ModelState.AddModelError("", "Giảng viên hoặc khóa học không tồn tại.");
            ViewData["GiangVien"] = await _context.GiangViens.ToListAsync();
            ViewData["KhoaHoc"] = await _context.KhoaHocs.ToListAsync();
            return View(lopHoc);
        }

        // Gán thuộc tính điều hướng (EF sẽ tự xử lý các thuộc tính này)
        lopHoc.MaGvNavigation = giangVien;
        lopHoc.MaKhoaHocNavigation = khoaHoc;

        // Đảm bảo khóa chính không bị null (nếu có Auto Increment, bỏ dòng này)
        lopHoc.MaLop = 0; // Nếu Auto Increment, EF sẽ tự tạo ID

        // Thêm lớp học vào DbContext
        _context.LopHocs.Add(lopHoc);
        await _context.SaveChangesAsync();

        Console.WriteLine("✅ Dữ liệu đã lưu vào database.");
        return RedirectToAction(nameof(Index));
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Lỗi khi lưu vào database: " + ex.Message);
        ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm lớp học. Vui lòng thử lại.");
    }

    // Tải lại danh sách giảng viên và khóa học khi có lỗi
    ViewData["GiangVien"] = await _context.GiangViens.ToListAsync();
    ViewData["KhoaHoc"] = await _context.KhoaHocs.ToListAsync();
    return View(lopHoc);
}



        public async Task<IActionResult> Edit(int id)
        {
            var lopHoc = await _context.LopHocs.FindAsync(id);
            if (lopHoc == null)
                return NotFound();

            return View(lopHoc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLop,NgayBatDau,NgayKetThuc,DiaDiem,ThoiGianHocTrongTuan,SoLuongToiDa,GhiChu,TrangThai,IsOnline,MaKhoaHoc,MaGv")] LopHoc lopHoc)
        {
            if (id != lopHoc.MaLop)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(lopHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lopHoc);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var lopHoc = await _context.LopHocs
                .Include(l => l.MaKhoaHocNavigation)
                .Include(l => l.MaGvNavigation)
                .FirstOrDefaultAsync(m => m.MaLop == id);

            if (lopHoc == null)
                return NotFound();

            return View(lopHoc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lopHoc = await _context.LopHocs.FindAsync(id);
            if (lopHoc != null)
            {
                _context.LopHocs.Remove(lopHoc);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}