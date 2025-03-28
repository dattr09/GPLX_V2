using GPLX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace GPLX.Controllers
{
    public class KetQuaThiGplxController : Controller
    {
        private readonly AppDBContext _context;

        public KetQuaThiGplxController(AppDBContext context)
        {
            _context = context;
        }

        // GET: KetQuaThiGplx
        public async Task<IActionResult> Index()
        {
            var ketQuaThiGplxes = await _context.KetQuaThiGplxes
                .Include(k => k.MaDkthiGplxNavigation)
                    .ThenInclude(d => d.CccdNavigation) // Lấy thông tin công dân
                .ToListAsync();

            return View(ketQuaThiGplxes);
        }

        // GET: KetQuaThiGplx/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ketQuaThiGplx = await _context.KetQuaThiGplxes
                .Include(k => k.MaDkthiGplxNavigation)
                .FirstOrDefaultAsync(m => m.MaKetQua == id);
            if (ketQuaThiGplx == null)
            {
                return NotFound();
            }

            return View(ketQuaThiGplx);
        }

        // GET: KetQuaThiGplx/Create
        // GET: KetQuaThiGplx/Create
        public IActionResult Create()
        {
            // Lấy danh sách đăng ký thi với format "CCCD - Họ Tên"
            var dkThiList = _context.DkthiGplxes
                .Include(d => d.CccdNavigation)
                .Select(d => new
                {
                    MaDkthiGplx = d.MaDkthiGplx,
                    DisplayText = d.CccdNavigation.Cccd + " - " + d.CccdNavigation.HoTen
                }).ToList();

            ViewData["MaDkthiGplx"] = new SelectList(dkThiList, "MaDkthiGplx", "DisplayText");

            // Lấy mã lớn nhất hiện có và tăng lên 1
            var lastNumber = _context.KetQuaThiGplxes
                .Where(k => k.MaKetQua != null && k.MaKetQua.StartsWith("KQ")) // Chỉ lấy mã hợp lệ
                .Select(k => k.MaKetQua.Substring(2)) // Bỏ "KQ", lấy phần số
                .ToList() // Chuyển thành danh sách để xử lý
                .Where(k => int.TryParse(k, out _)) // Chỉ giữ số hợp lệ
                .Select(int.Parse) // Chuyển thành số nguyên
                .DefaultIfEmpty(0) // Nếu không có, mặc định là 0
                .Max(); // Lấy số lớn nhất

            string newMaKetQua = $"KQ{(lastNumber + 1):D4}"; // Định dạng KQ0001, KQ0002, ...
            ViewBag.NewMaKetQua = newMaKetQua;

            // Format thành KQ0001, KQ0002, ...
            ViewBag.NewMaKetQua = newMaKetQua;

            return View();
        }

        // POST: KetQuaThiGplx/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKetQua,MaDkthiGplx,DiemLyThuyet,DiemThucHanh,DiemMoPhong,DiemDuongTruong,GhiChu,KetQua")] KetQuaThiGplx ketQuaThiGplx)
        {
            // Tạo mã kết quả tự động theo định dạng KQxxx
            if (string.IsNullOrEmpty(ketQuaThiGplx.MaKetQua))
            {
                var lastRecord = _context.KetQuaThiGplxes
                    .OrderByDescending(k => k.MaKetQua)
                    .FirstOrDefault();

                int newId = 1;
                if (lastRecord != null)
                {
                    string lastMa = lastRecord.MaKetQua.Replace("KQ", "");
                    if (int.TryParse(lastMa, out int lastNumber))
                    {
                        newId = lastNumber + 1;
                    }
                }
                ketQuaThiGplx.MaKetQua = $"KQ{newId:D4}";
            }

            if (ModelState.IsValid)
            {
                _context.Add(ketQuaThiGplx);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState không hợp lệ, giữ lại mã kết quả
            ViewBag.NewMaKetQua = ketQuaThiGplx.MaKetQua;

            // Load lại danh sách đăng ký thi GPLX
            var dkThiList = _context.DkthiGplxes
                .Include(d => d.CccdNavigation)
                .Select(d => new
                {
                    MaDkthiGplx = d.MaDkthiGplx,
                    DisplayText = d.CccdNavigation.Cccd + " - " + d.CccdNavigation.HoTen
                }).ToList();
            ViewData["MaDkthiGplx"] = new SelectList(dkThiList, "MaDkthiGplx", "DisplayText");

            return View(ketQuaThiGplx);
        }




        // GET: KetQuaThiGplx/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ketQuaThiGplx = await _context.KetQuaThiGplxes.FindAsync(id);
            if (ketQuaThiGplx == null)
            {
                return NotFound();
            }
            ViewData["MaDkthiGplx"] = new SelectList(_context.DkthiGplxes, "MaDkthiGplx", "MaDkthiGplx", ketQuaThiGplx.MaDkthiGplx);
            return View(ketQuaThiGplx);
        }

        // POST: KetQuaThiGplx/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKetQua,MaDkthiGplx,DiemLyThuyet,DiemThucHanh,DiemMoPhong,DiemDuongTruong,GhiChu,KetQua")] KetQuaThiGplx ketQuaThiGplx)
        {
            if (id != ketQuaThiGplx.MaKetQua)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ketQuaThiGplx);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KetQuaThiGplxExists(ketQuaThiGplx.MaKetQua))
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
            ViewData["MaDkthiGplx"] = new SelectList(_context.DkthiGplxes, "MaDkthiGplx", "MaDkthiGplx", ketQuaThiGplx.MaDkthiGplx);
            return View(ketQuaThiGplx);
        }

        // GET: KetQuaThiGplx/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ketQuaThiGplx = await _context.KetQuaThiGplxes
                .Include(k => k.MaDkthiGplxNavigation)
                .FirstOrDefaultAsync(m => m.MaKetQua == id);
            if (ketQuaThiGplx == null)
            {
                return NotFound();
            }

            return View(ketQuaThiGplx);
        }

        // POST: KetQuaThiGplx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ketQuaThiGplx = await _context.KetQuaThiGplxes.FindAsync(id);
            _context.KetQuaThiGplxes.Remove(ketQuaThiGplx);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KetQuaThiGplxExists(string id)
        {
            return _context.KetQuaThiGplxes.Any(e => e.MaKetQua == id);
        }
    }
}