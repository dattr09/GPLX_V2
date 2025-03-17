using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GPLX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GPLX.Controllers
{
    public class ViPhamGplxController : Controller
    {
        private readonly AppDBContext _context;

        public ViPhamGplxController(AppDBContext context)
        {
            _context = context;
        }

        // GET: ViPhamGplx
        public async Task<IActionResult> Index()
        {
            var viPhamGplxes = _context.ViPhamGplxes.Include(v => v.MaGplxNavigation).Include(v => v.MaLoaiViPhamNavigation);
            return View(await viPhamGplxes.ToListAsync());
        }

        // GET: ViPhamGplx/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viPhamGplx = await _context.ViPhamGplxes
                .Include(v => v.MaGplxNavigation)
                .Include(v => v.MaLoaiViPhamNavigation)
                .FirstOrDefaultAsync(m => m.MaViPham == id);

            if (viPhamGplx == null)
            {
                return NotFound();
            }

            return View(viPhamGplx);
        }

        // GET: ViPhamGplx/Create
        public IActionResult Create()
        {
            // Load danh sách GPLX và Loại Vi Phạm cho dropdown list
            ViewData["MaGplx"] = new SelectList(_context.Gplxes, "MaGplx", "MaGplx");
            ViewData["MaLoaiViPham"] = new SelectList(_context.LoaiViPhams, "MaLoaiViPham", "TenViPham");
            return View();
        }

        // POST: ViPhamGplx/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaViPham,MaGplx,MaLoaiViPham,NgayViPham,TrangThai")] ViPhamGplx viPhamGplx)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState không hợp lệ! Kiểm tra dữ liệu nhập vào.");
                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"- {error.Key}: {subError.ErrorMessage}");
                    }
                }
                ViewData["MaGplx"] = new SelectList(_context.Gplxes, "MaGplx", "MaGplx", viPhamGplx.MaGplx);
                ViewData["MaLoaiViPham"] = new SelectList(_context.LoaiViPhams, "MaLoaiViPham", "TenViPham", viPhamGplx.MaLoaiViPham);
                return View(viPhamGplx);
            }

            try
            {
                _context.Add(viPhamGplx);
                await _context.SaveChangesAsync();
                Console.WriteLine("Dữ liệu đã được lưu thành công!");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu dữ liệu: {ex.Message}");
            }

            ViewData["MaGplx"] = new SelectList(_context.Gplxes, "MaGplx", "MaGplx", viPhamGplx.MaGplx);
            ViewData["MaLoaiViPham"] = new SelectList(_context.LoaiViPhams, "MaLoaiViPham", "TenViPham", viPhamGplx.MaLoaiViPham);
            return View(viPhamGplx);
        }



        // GET: ViPhamGplx/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viPhamGplx = await _context.ViPhamGplxes.FindAsync(id);
            if (viPhamGplx == null)
            {
                return NotFound();
            }

            ViewData["MaGplx"] = new SelectList(_context.Gplxes, "MaGplx", "MaGplx", viPhamGplx.MaGplx);
            ViewData["MaLoaiViPham"] = new SelectList(_context.LoaiViPhams, "MaLoaiViPham", "TenViPham", viPhamGplx.MaLoaiViPham);

            return View(viPhamGplx);
        }


        // POST: ViPhamGplx/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaViPham,MaGplx,MaLoaiViPham,NgayViPham,TrangThai")] ViPhamGplx viPhamGplx)
        {
            if (id != viPhamGplx.MaViPham)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["MaGplx"] = new SelectList(_context.Gplxes, "MaGplx", "MaGplx", viPhamGplx.MaGplx);
                ViewData["MaLoaiViPham"] = new SelectList(_context.LoaiViPhams, "MaLoaiViPham", "TenViPham", viPhamGplx.MaLoaiViPham);
                return View(viPhamGplx);
            }

            try
            {
                _context.Update(viPhamGplx);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViPhamGplxExists(viPhamGplx.MaViPham))
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

        // GET: ViPhamGplx/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viPhamGplx = await _context.ViPhamGplxes
                .Include(v => v.MaGplxNavigation)
                .Include(v => v.MaLoaiViPhamNavigation)
                .FirstOrDefaultAsync(m => m.MaViPham == id);

            if (viPhamGplx == null)
            {
                return NotFound();
            }

            return View(viPhamGplx);
        }

        // POST: ViPhamGplx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viPhamGplx = await _context.ViPhamGplxes.FindAsync(id);
            _context.ViPhamGplxes.Remove(viPhamGplx);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViPhamGplxExists(int id)
        {
            return _context.ViPhamGplxes.Any(e => e.MaViPham == id);
        }
    }
}
