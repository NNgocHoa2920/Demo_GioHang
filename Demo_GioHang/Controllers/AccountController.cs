using Demo_GioHang.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo_GioHang.Controllers
{
    public class AccountController : Controller
    {
        //gọi class diện cho csdl 
        private readonly GioHangDBContext _db;
        //tiêm DI: tiêm db vào trong controler
        public AccountController(GioHangDBContext db)
        {
            _db = db;
        }

        //lấy ra tát cả danh sác account
        public IActionResult Index()
        {
            //lấy giá trị session có key dc lưu là Account
            //lấy ra userName mà mình lư vào sesison
            var session = HttpContext.Session.GetString("Account"); // lấy ra userName mà đã đc lưu
            if (session == null)  //check chưa đăng nhập
            {
                return Content("Chưa đăng nhập mà dám vào xem");
            }
            else
            {
                ViewData["message"] = $"Chào mừng {session} đến với bình nguyên vô tận";
            }
            //lấy toàn bộ user
            var accountData = _db.Accounts.ToList();
            return View(accountData);
        }

        //Đăng kí 1 account = giống logic của create
        
        public IActionResult DangKy() // tạo ra 1 form đăng kí
        {
            
            return View();
        }
        //tạo ra 1 tài khoản
        [HttpPost]
        public IActionResult DangKy(Account account)
        {
            try
            {
                //tạo 1 account mới 
                _db.Accounts.Add(account);
                //khi tạo 1 account đồng thời sẽ tạo 1 giỏ hàng
                GioHang gioHang = new GioHang()
                {
                    UserName = account.UserName,
                    AccountID = account.Id

                };
                _db.GioHangs.Add(gioHang);
                _db.SaveChanges();
                TempData["Status"] = "Chúc mừng bạn đã tạo tài khoản thành công";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return BadRequest();
            } 
            
        }
        //Tạo đăng nhập loginn
        //tạo ra form đăng nhạp
        public IActionResult Login()
        {
            return View();
        }
        //đăng nhập
        [HttpPost]
        public IActionResult Login(string userName, string passWord)
        {
            //check xem user và pass có nhập hay k
            if (userName == null || passWord == null) 
            {
                return View();// nếu k nhập thì view vẫn giữ = view login
            }
            //nếu nhập thì pphari tìm kiếm xem usernam và pas có tồn tại trong csdl 
            var acc = _db.Accounts.ToList().FirstOrDefault(x=>x.UserName== userName && x.Password==passWord);
            if(acc == null)// k tìm thấy tk trong csdl
            {
                return Content("Đăng nhập không thành công mời kiểm tra lại tài khoản");
            }
            else
            {
                //lưu dữ liệu login vào sesion với key la Account
                HttpContext.Session.SetString("Account",userName);
                return RedirectToAction("Index");
            }
        }
    }
}
