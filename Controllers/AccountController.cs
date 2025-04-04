using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GPLX.Models;

namespace WebsiteBanHang.Controllers
{
  public class AccountController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailSender _emailSender;

    public AccountController(UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager,
                             IEmailSender emailSender)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _emailSender = emailSender;
    }

    // GET: Hiển thị trang đăng nhập
    [HttpGet]
    public IActionResult Login()
    {
      return View();
    }

    // POST: Xử lý đăng nhập
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
          if (!await _userManager.IsEmailConfirmedAsync(user))
          {
            ModelState.AddModelError("", "Email của bạn chưa được xác thực.");
            ViewBag.EmailNotConfirmed = true;
            return View(model);
          }

          var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
          if (result.Succeeded)
          {
            return RedirectToAction("Index", "Home");
          }
        }

        ModelState.AddModelError("", "Đăng nhập không hợp lệ.");
      }

      return View(model);
    }

    // GET: Xác nhận email
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
      if (userId == null || token == null)
      {
        return RedirectToAction("Index", "Home");
      }

      var user = await _userManager.FindByIdAsync(userId);
      if (user == null)
      {
        return NotFound($"Không tìm thấy người dùng với ID '{userId}'.");
      }

      var result = await _userManager.ConfirmEmailAsync(user, token);
      if (result.Succeeded)
      {
        TempData["ConfirmSuccess"] = "Xác nhận email thành công! Bạn có thể đăng nhập.";
        return RedirectToAction("Login");
      }

      TempData["ConfirmFail"] = "Xác nhận email thất bại. Liên kết có thể đã hết hạn.";
      return RedirectToAction("Login");
    }

    // POST: Đăng xuất
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Login");
    }

    // GET: Trang chờ xác thực email
    public IActionResult EmailVerificationPending()
    {
      return View();
    }

    // POST: Gửi lại email xác nhận
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResendConfirmationEmail(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);
      if (user == null || await _userManager.IsEmailConfirmedAsync(user))
      {
        return RedirectToAction("Login");
      }

      var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
      var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
      await _emailSender.SendEmailAsync(email, "Xác nhận lại email", $"Bấm vào <a href='{confirmationLink}'>đây</a> để xác nhận lại tài khoản.");

      TempData["ResendMessage"] = "Email xác nhận đã được gửi lại. Vui lòng kiểm tra email của bạn.";
      return RedirectToAction("Login");
    }

    // POST: Xử lý đăng ký
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
          var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
          await _emailSender.SendEmailAsync(user.Email, "Xác nhận email", $"Vui lòng xác nhận tài khoản của bạn bằng cách <a href='{confirmationLink}'>bấm vào đây</a>.");

          return RedirectToAction("EmailVerificationPending", "Account", new { email = model.Email });
        }

        foreach (var error in result.Errors)
        {
          ModelState.AddModelError("", error.Description);
        }
      }

      return View(model);
    }

  }
}