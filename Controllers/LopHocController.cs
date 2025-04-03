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
            // Truyền danh sách giảng viên và khóa học vào ViewData để hiển thị trong dropdown list
            ViewData["GiangVien"] = _context.GiangViens.ToList();
            ViewData["KhoaHoc"] = _context.KhoaHocs.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLop,NgayBatDau,NgayKetThuc,DiaDiem,ThoiGianHocTrongTuan,SoLuongToiDa,GhiChu,TrangThai,IsOnline,MaKhoaHoc,MaGv")] LopHoc lopHoc)
        {
            if (ModelState.IsValid)
            {
                // Convert NgayBatDau và NgayKetThuc từ string sang DateOnly
                lopHoc.NgayBatDau = DateOnly.Parse(lopHoc.NgayBatDau.ToString());
                lopHoc.NgayKetThuc = DateOnly.Parse(lopHoc.NgayKetThuc.ToString());

                _context.Add(lopHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Load lại danh sách giảng viên và khóa học khi có lỗi
            ViewData["GiangVien"] = _context.GiangViens.ToList();
            ViewData["KhoaHoc"] = _context.KhoaHocs.ToList();

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