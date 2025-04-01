using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GPLX.Models;
using System.Threading.Tasks;

namespace GPLX.Controllers
{
    public class GiangVienController : Controller
    {
        private readonly AppDBContext _context;

        public GiangVienController(AppDBContext context)
        {
            _context = context;
        }

        // GET: GiangVien
        public async Task<IActionResult> Index()
        {
            return View(await _context.GiangViens.ToListAsync());
        }

        // GET: GiangVien/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var giangVien = await _context.GiangViens.FindAsync(id);
            if (giangVien == null)
            {
                return NotFound();
            }
            return View(giangVien);
        }

        // GET: GiangVien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GiangVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaGv,HoTen,NgaySinh,GioiTinh,Sdt,Email")] GiangVien giangVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giangVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(giangVien);
        }

        // GET: GiangVien/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var giangVien = await _context.GiangViens.FindAsync(id);
            if (giangVien == null)
            {
                return NotFound();
            }
            return View(giangVien);
        }

        // POST: GiangVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaGv,HoTen,NgaySinh,GioiTinh,Sdt,Email")] GiangVien giangVien)
        {
            if (id != giangVien.MaGv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(giangVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(giangVien);
        }

        // GET: GiangVien/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var giangVien = await _context.GiangViens.FindAsync(id);
            if (giangVien == null)
            {
                return NotFound();
            }
            return View(giangVien);
        }

        // POST: GiangVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giangVien = await _context.GiangViens.FindAsync(id);
            if (giangVien != null)
            {
                _context.GiangViens.Remove(giangVien);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
