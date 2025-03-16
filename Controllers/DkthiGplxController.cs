using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GPLX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GPLX.Controllers
{
    public class DkthiGplxController : Controller
    {
        private readonly AppDBContext _context;

        public DkthiGplxController(AppDBContext context)
        {
            _context = context;
        }

        // GET: DkthiGplx
        public async Task<IActionResult> Index()
        {
            var dktGplx = _context.DkthiGplxes
                .Include(d => d.CccdNavigation)
                .Include(d => d.MaLoaiNavigation)
                .Include(d => d.MaTtshNavigation);
            return View(await dktGplx.ToListAsync());
        }

        // GET: DkthiGplx/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var dktGplx = await _context.DkthiGplxes
                .Include(d => d.CccdNavigation)
                .Include(d => d.MaLoaiNavigation)
                .Include(d => d.MaTtshNavigation)
                .FirstOrDefaultAsync(m => m.MaDkthiGplx == id);

            if (dktGplx == null)
                return NotFound();

            return View(dktGplx);
        }

        // GET: DkthiGplx/Create
        public IActionResult Create()
        {
            LoadDropdownData();
            return View();
        }

        // POST: DkthiGplx/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
       public async Task<IActionResult> Create([Bind("Cccd,MaLoai,NgayThi,MaTtsh")] DkthiGplx dktGplx)
{
    if (!ModelState.IsValid)
    {
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine(error.ErrorMessage); // Debug lỗi
        }

        LoadDropdownData(dktGplx); 
        return View(dktGplx);
    }

    _context.Add(dktGplx);
    await _context.SaveChangesAsync();
    TempData["Success"] = "Thêm đăng ký thi GPLX thành công!";
    return RedirectToAction(nameof(Index));
}

        // GET: DkthiGplx/Edit/5
        public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
        return NotFound();

    var dktGplx = await _context.DkthiGplxes.FindAsync(id);
    if (dktGplx == null)
        return NotFound();

    // Load dropdown với giá trị hiện tại
    LoadDropdownData(dktGplx);

    return View(dktGplx);
}


        // POST: DkthiGplx/Edit/5
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("MaDkthiGplx,Cccd,MaLoai,NgayThi,MaTtsh")] DkthiGplx dktGplx)
{
    if (id != dktGplx.MaDkthiGplx)
        return NotFound();

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(dktGplx);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật đăng ký thi GPLX thành công!";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DktGplxExists(dktGplx.MaDkthiGplx))
                return NotFound();
            else
                ModelState.AddModelError("", "Lỗi cập nhật dữ liệu. Vui lòng thử lại!");
        }
    }

    // 🔥 Load lại dropdown nếu có lỗi nhập liệu
    LoadDropdownData(dktGplx);
    return View(dktGplx);
}



        // GET: DkthiGplx/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var dktGplx = await _context.DkthiGplxes
                .Include(d => d.CccdNavigation)
                .Include(d => d.MaLoaiNavigation)
                .Include(d => d.MaTtshNavigation)
                .FirstOrDefaultAsync(m => m.MaDkthiGplx == id);

            if (dktGplx == null)
                return NotFound();

            return View(dktGplx);
        }

        // POST: DkthiGplx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
 public async Task<IActionResult> DeleteConfirmed(int id)
{
    try
    {
        var dktGplx = await _context.DkthiGplxes.FindAsync(id);
        if (dktGplx == null)
        {
            TempData["Error"] = "Không tìm thấy đăng ký thi để xóa!";
            return RedirectToAction(nameof(Index));
        }

        _context.DkthiGplxes.Remove(dktGplx);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Xóa đăng ký thi GPLX thành công!";
    }
    catch (DbUpdateException)
    {
        // Lỗi do ràng buộc dữ liệu (ví dụ: khóa ngoại)
        TempData["Error"] = "Không thể xóa đăng ký thi GPLX vì đang có dữ liệu liên quan!";
    }
    catch (Exception ex)
    {
        // Ghi log lỗi nếu cần
        TempData["Error"] = "Đã xảy ra lỗi trong quá trình xóa!";
    }

    return RedirectToAction(nameof(Index));
}


        private bool DktGplxExists(int id)
        {
            return _context.DkthiGplxes.Any(e => e.MaDkthiGplx == id);
        }

        // Load dữ liệu cho dropdown list
        private void LoadDropdownData(DkthiGplx? dktGplx = null)
        {
            ViewData["Cccd"] = new SelectList(_context.CongDans, "Cccd", "HoTen", dktGplx?.Cccd);
            ViewData["MaLoai"] = new SelectList(_context.LoaiGplxes, "MaLoai", "TenLoai", dktGplx?.MaLoai);
            ViewData["MaTtsh"] = new SelectList(_context.TrungTamSatHaches, "MaTtsh", "TenTrungTam", dktGplx?.MaTtsh);
        }
    }
}
