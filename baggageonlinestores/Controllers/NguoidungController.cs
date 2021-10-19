using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using baggageonlinestores.Models;
namespace baggageonlinestores.Controllers
{
    public class NguoidungController : Controller
    {
        QLbanhangonlineDataContext db = new QLbanhangonlineDataContext();
        //
        // GET: /Nguoidung/
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangki()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangki(FormCollection collection, khachhang kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = string.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if (string.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được trống";
            }
            else if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "phải nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "phải nhập mật khẩu";
            }
            else if (string.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "phải nhập lại mật khẩu";
            }
            if (string.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được bỏ trống";
            }
            if (string.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "phải nhập lại số điện thoại";
            }
            else
            {
                // gắn giá trị cho đối tượng được tạo mới
                kh.HoTen = hoten;
                kh.Taikhoan = tendn;
                kh.Matkhau = matkhau;
                kh.Email = email;
                kh.DiachiKH = diachi;
                kh.DienthoaiKH = dienthoai;
                kh.Ngaysinh = DateTime.Parse(ngaysinh);
                db.khachhangs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangki();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            khachhang kh = db.khachhangs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }

            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = " Phải nhập mật khẩu";
            }
            else
            {
                if (kh != null)
                {
                    ViewBag.Thongbao = "Đăng Nhập Thành Công";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Thongbao = "Tài Khoản Hoặc Mật Khẩu Không Đúng";

            } return View();
        }
    }
}