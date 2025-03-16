using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GPLX.Models;

namespace GPLX.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

public IActionResult Index()
{
    var model = new HomeViewModel
    {
        Articles = new List<Article>
        {
            new Article { Title = "Những điểm mới của Luật Trật tự, an toàn giao thông đường bộ", Url = "https://www.baobaclieu.vn/phap-luat/nhung-diem-moi-cua-luat-trat-tu-an-toan-giao-thong-duong-bo-97294.html" },
            new Article { Title = "Nghị định 168 giúp giảm tai nạn giao thông", Url = "https://thoibaotaichinhvietnam.vn/nghi-dinh-168-giup-giam-thieu-tai-nan-nang-cao-y-thuc-chap-hanh-phap-luat-ve-giao-thong-168779.html" }
        },
        TrafficFineLinks = new List<Article>
        {
            new Article { Title = "Tra cứu trên Cục Đăng kiểm Việt Nam", Url = "https://cafef.vn/5-cach-tra-cuu-xe-bi-phat-nguoi-vi-pham-giao-thong-moi-nhat-nam-2025-188250204143116936.chn" },
            new Article { Title = "Phạt Nguội Giao Thông", Url = "https://phatnguoigiaothong.net/" }
        }
    };

    return View(model);
}


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
