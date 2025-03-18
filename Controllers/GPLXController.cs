using GPLX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GPLX.Controllers
{
    public class GPLXController : Controller
    {
        private readonly AppDBContext _context;

        public GPLXController(AppDBContext context)
        {
            _context = context;
        }

        // GET: GPLX
        public async Task<IActionResult> Index()
        {
            var gplxList = await _context.Gplxes.Include(g => g.MaKetQuaNavigation).ToListAsync();
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
            if (ModelState.IsValid)
            {
                gplx.MaKetQua = "Đậu"; // Gán mặc định
                _context.Add(gplx);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Debug lỗi nếu form không submit
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

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
    }
}
