using GPLX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace GPLX.Controllers
{
    [Authorize]
    public class GPLXController : Controller
    {
        private readonly AppDBContext _context;

        public GPLXController(AppDBContext context)
        {
            _context = context;
        }

        // GET: GPLX
        // GET: GPLX
        public async Task<IActionResult> Index()
        {
            var gplxList = await _context.Gplxes
                .Include(g => g.MaKetQuaNavigation)
                    .ThenInclude(kq => kq.MaDkthiGplxNavigation)
                        .ThenInclude(dk => dk.CccdNavigation) // Lấy thông tin Công Dân (Tên, CCCD)
                .Include(g => g.MaKetQuaNavigation)
                    .ThenInclude(kq => kq.MaDkthiGplxNavigation)
                        .ThenInclude(dk => dk.MaLoaiNavigation) // Lấy thông tin Mã Loại GPLX
                .ToListAsync();

            return View(gplxList);
        }

        // GET: GPLX/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ketQuaThi = await _context.KetQuaThiGplxes
                .Include(k => k.Gplxes) // Load danh sách GPLX
                .FirstOrDefaultAsync(m => m.MaKetQua == id);

            if (ketQuaThi == null)
            {
                return NotFound();
            }

            return View(ketQuaThi);
        }


        // GET: GPLX/Create
        public IActionResult Create()
        {
            ViewBag.MaKetQua = new SelectList(_context.KetQuaThiGplxes, "MaKetQua", "MaKetQua");
            return View();
        }


        // POST: GPLX/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaGplx,NgayCap,NgayHetHan")] Gplx gplx)
        {
            // Debug: In dữ liệu nhận được từ form
            Console.WriteLine($"Received Data - MaGplx: {gplx.MaGplx}, NgayCap: {gplx.NgayCap}, NgayHetHan: {gplx.NgayHetHan}");

            // Lấy kết quả thi từ bảng KetQuaThiGplxes dựa trên MaGplx
            var ketQuaThi = _context.KetQuaThiGplxes.FirstOrDefault(k => k.MaKetQua == gplx.MaGplx);

            if (ketQuaThi == null)
            {
                Console.WriteLine("LỖI: Không tìm thấy kết quả thi trong database.");
                ModelState.AddModelError(string.Empty, "Không thể cấp GPLX vì không tìm thấy kết quả thi.");
            }
            else if (ketQuaThi.KetQua != "Đậu")
            {
                Console.WriteLine($"LỖI: Thí sinh có kết quả '{ketQuaThi.KetQua}', không đủ điều kiện cấp GPLX.");
                ModelState.AddModelError(string.Empty, "Không thể cấp GPLX vì thí sinh chưa đậu.");
            }

            // Debug: In ra danh sách lỗi nếu có
            if (!ModelState.IsValid)
            {
                Console.WriteLine("DANH SÁCH LỖI:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            // Nếu hợp lệ, lưu vào database
            if (ModelState.IsValid)
            {
                _context.Add(gplx);
                await _context.SaveChangesAsync();
                Console.WriteLine("GPLX đã được tạo thành công!");
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("Trả về form với lỗi.");
            return View(gplx);
        }




        // GET: GPLX/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gplx = await _context.Gplxes.FindAsync(id);
            if (gplx == null)
            {
                return NotFound();
            }

            // Load danh sách kết quả thi cho dropdown list
            ViewBag.MaKetQua = new SelectList(_context.KetQuaThiGplxes, "MaKetQua", "KetQua", gplx.MaKetQua);

            return View(gplx);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaGplx,NgayCap,NgayHetHan,MaKetQua")] Gplx gplx)
        {
            if (id != gplx.MaGplx)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gplx);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật GPLX thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Gplxes.Any(e => e.MaGplx == gplx.MaGplx))
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

            ViewBag.MaKetQua = new SelectList(_context.KetQuaThiGplxes, "MaKetQua", "KetQua", gplx.MaKetQua);
            return View(gplx);
        }


        // GET: GPLX/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gplx = await _context.Gplxes
                .Include(g => g.MaKetQuaNavigation)
                .FirstOrDefaultAsync(m => m.MaGplx == id);
            if (gplx == null)
            {
                return NotFound();
            }

            return View(gplx);
        }

        // POST: GPLX/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gplx = await _context.Gplxes
                .Include(g => g.ViPhamGplxes) // Load danh sách vi phạm liên quan
                .FirstOrDefaultAsync(m => m.MaGplx == id);

            if (gplx == null)
            {
                return NotFound();
            }

            // Xóa các vi phạm liên quan trước
            _context.ViPhamGplxes.RemoveRange(gplx.ViPhamGplxes);

            // Xóa GPLX
            _context.Gplxes.Remove(gplx);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool GplxExists(string id)
        {
            return _context.Gplxes.Any(e => e.MaGplx == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAutoGplx()
        {
            // Tìm danh sách các thí sinh đậu nhưng chưa có GPLX
            var thiSinhDau = _context.KetQuaThiGplxes
                .Where(k => k.KetQua == "Đậu" && !_context.Gplxes.Any(g => g.MaKetQua == k.MaKetQua))
                .ToList();

            if (!thiSinhDau.Any())
            {
                ModelState.AddModelError(string.Empty, "Không có thí sinh nào đủ điều kiện cấp GPLX.");
                return View();
            }

            foreach (var ketQuaThi in thiSinhDau)
            {
                var newGplx = new Gplx
                {
                    MaGplx = GenerateMaGPLX(), // Hàm tạo mã GPLX 12 ký tự
                    MaKetQua = ketQuaThi.MaKetQua,
                    NgayCap = DateOnly.FromDateTime(DateTime.Now),
                    NgayHetHan = DateOnly.FromDateTime(DateTime.Now.AddYears(10)) // Giả sử GPLX có hạn 10 năm
                };

                _context.Gplxes.Add(newGplx);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private string GenerateMaGPLX()
        {
            var random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
