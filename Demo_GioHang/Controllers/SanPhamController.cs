using Demo_GioHang.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo_GioHang.Controllers
{
    public class SanPhamController : Controller
    {
        //gọi class đại diện cho csdl
        GioHangDBContext _db;
        //tiêm di
        public SanPhamController(GioHangDBContext db)
        {
            _db = db;
        }
        //lấy toàn bộ dữ liệu của sản phẩm
        public IActionResult Index()
        {
            //lấy giá trị sessin có tên username
            var sesionData = HttpContext.Session.GetString("Account");
            if(sesionData==null)
            {
                ViewData["mess"] = "Chưa đăng nhập bạn ôi";
                return View();
            }
            else
            {
                ViewData["mess1"] = "Mời bạn xem sản phẩm";
                var list = _db.SanPhams.ToList();
                return View(list);

            }
           

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SanPham sp)
        {
            _db.SanPhams.Add(sp);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            //tìm thông tin muốn xem chi tiết
            var sp = _db.SanPhams.Find(id);
            return View(sp);
        }

        //tạo view có chứa thông tin cần sửa
        public IActionResult Edit(Guid id)
        {
            //tìm thông tin
            var sp = _db.SanPhams.Find(id);
            return View(sp);
        }
        public IActionResult Edit(SanPham sp)
        {
            _db.SanPhams.Update(sp);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //thêm giỏ hàng
        public IActionResult AddToCart(Guid id, int soLuong) // id là id của sp được add
        {
            //laasy ra thông tin của user tương ứng vs giỏ hàng
            var acc = HttpContext.Session.GetString("Account");
            if(acc==null)
            {
                return Content("Chưa đăng nhập hoặc hết hạn");
            }
            //lấy thông tin của acc => lấy toàn bộ đối tượng
            var getAcc = _db.Accounts.FirstOrDefault(x => x.UserName == acc);

            //lấy giỏ hàng tương ứng
            var gioHang = _db.GioHangs.FirstOrDefault(x => x.AccountID == getAcc.Id);
            if(gioHang==null)
            {
                return Content("Chưa có giỏ hàng");
            }
            //lấy toàn bộ sản phẩm trong giỏ hàng chi tiết của acc
            var userCart = _db.GHCTs.Where(x => x.GioHangId == gioHang.Id).ToList();

            //XỬ LÍ CỘNG DỒN NESU TRÙNG SẢN PHẨM
            //Duyệt GHCT
            bool checkGH = false;
            Guid idGHCT = Guid.NewGuid();
            foreach(var item in userCart)
            {
                //nếu id của sản phẩm trong giỏ hang của user đó trùng với sản phẩm vừa đc add
                if(item.SanPhamId== id)
                {
                    checkGH = true;
                    idGHCT = item.Id;//lấy ra id của ghct để tí mình update
                    break;
                }    
            }

            //nếu mà sản phaarm chưa đc chọn
            if (!checkGH)
            {
                //tạo ra ghct ứng vs sản phâm đó
                GHCT ghct = new GHCT()
                {
                    SanPhamId = id,
                    GioHangId= gioHang.Id,
                    SoLuong = soLuong,
                };
                _db.GHCTs.Add(ghct);
                _db.SaveChanges();
                return RedirectToAction("index");
            }

            // nếu sp đc chọn rồi thì cộng dồn nó lại
            var ghctUpdate = _db.GHCTs.FirstOrDefault(x => x.Id == idGHCT);// tìm theo ghct
            ghctUpdate.SoLuong = ghctUpdate.SoLuong + soLuong;
            _db.SaveChanges();
            return RedirectToAction("index");
                
        }
    }
}
