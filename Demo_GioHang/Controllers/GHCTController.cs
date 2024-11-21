using Demo_GioHang.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo_GioHang.Controllers
{
    public class GHCTController : Controller
    {
        GioHangDBContext _db;
        public GHCTController(GioHangDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("Account");
            if (user == null)
            {
                return Content("Đăng nhập đi");
            }
            else
            {
                //lấy toàn bộ thông tin tk đã đăn nhập
                var getAcc = _db.Accounts.FirstOrDefault(x => x.UserName == user);
                //lấy gỏ hàng twnng ứng vs acc đăng nhjp
                var gioHang = _db.GioHangs.FirstOrDefault(x => x.AccountID == getAcc.Id);
                var ghctData = _db.GHCTs.Where(x => x.GioHangId == gioHang.Id);
                return View(ghctData);  
            }
        }
    }
}
