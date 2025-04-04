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
            if (dktGplx.NgayThi < DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("NgayThi", "Ngày thi không được ở quá khứ.");
            }

            // Kiểm tra xem đã đăng ký thi loại GPLX này chưa
            bool exists = await _context.DkthiGplxes.AnyAsync(d =>
                d.Cccd == dktGplx.Cccd && d.MaLoai == dktGplx.MaLoai);

            if (exists)
            {
                ModelState.AddModelError("MaLoai", "Bạn đã đăng ký thi loại GPLX này rồi!");
            }

            if (!ModelState.IsValid)
            {
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

            if (dktGplx.NgayThi < DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("NgayThi", "Ngày thi không được ở quá khứ.");
            }

            // Kiểm tra trùng lặp khi chỉnh sửa (loại trừ chính nó)
            bool exists = await _context.DkthiGplxes.AnyAsync(d =>
                d.Cccd == dktGplx.Cccd && d.MaLoai == dktGplx.MaLoai && d.MaDkthiGplx != dktGplx.MaDkthiGplx);

            if (exists)
            {
                ModelState.AddModelError("MaLoai", "Bạn đã đăng ký thi loại GPLX này rồi!");
            }

            if (!ModelState.IsValid)
            {
                LoadDropdownData(dktGplx);
                return View(dktGplx);
            }

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
public async Task<IActionResult> DeleteConfirmed(string id)
{
    try
    {
        // Tìm kết quả thi theo id
        var ketQuaThi = await _context.KetQuaThiGplxes.FindAsync(id);
        if (ketQuaThi == null)
        {
            // Nếu không tìm thấy kết quả thi, thông báo lỗi
            TempData["Error"] = "Không tìm thấy kết quả thi để xóa!";
            return RedirectToAction(nameof(Index));
        }

        // Kiểm tra xem có dữ liệu GPLX liên quan đến kết quả thi này không
        var gplxList = _context.Gplxes.Where(g => g.MaKetQua == id);
        if (gplxList.Any())
        {
            // Nếu có, không thể xóa và thông báo lỗi
            TempData["Error"] = "Không thể xóa kết quả thi vì đang có dữ liệu GPLX liên quan!";
            return RedirectToAction(nameof(Index));
        }

        // Xóa kết quả thi
        _context.KetQuaThiGplxes.Remove(ketQuaThi);
        await _context.SaveChangesAsync();

        // Thông báo thành công sau khi xóa
        TempData["Success"] = "Xóa kết quả thi thành công!";
    }
    catch (DbUpdateException)
    {
        // Lỗi khi cố gắng xóa do ràng buộc dữ liệu (ví dụ: khóa ngoại)
        TempData["Error"] = "Không thể xóa kết quả thi vì có dữ liệu liên quan!";
    }
    catch (Exception)
    {
        // Lỗi tổng quát trong quá trình xóa
        TempData["Error"] = "Đã xảy ra lỗi trong quá trình xóa!";
    }

    // Quay lại trang danh sách
    return RedirectToAction(nameof(Index));
}





        private bool DktGplxExists(int id)
        {
            return _context.DkthiGplxes.Any(e => e.MaDkthiGplx == id);
        }

        // Load dữ liệu cho dropdown list
        private void LoadDropdownData(DkthiGplx? dktGplx = null)
        {
            ViewData["Cccd"] = new SelectList(
                _context.CongDans.Select(cd => new
                {
                    Cccd = cd.Cccd,
                    HoTenCccd = cd.HoTen + " - " + cd.Cccd // Hiển thị cả họ tên và CCCD
                }),
                "Cccd", "HoTenCccd", dktGplx?.Cccd);

            ViewData["MaLoai"] = new SelectList(
                _context.LoaiGplxes, "MaLoai", "MaLoai", dktGplx?.MaLoai); // Chỉ hiển thị Mã Loại

            ViewData["MaTtsh"] = new SelectList(
                _context.TrungTamSatHaches, "MaTtsh", "TenTrungTam", dktGplx?.MaTtsh);
        }
    }
}