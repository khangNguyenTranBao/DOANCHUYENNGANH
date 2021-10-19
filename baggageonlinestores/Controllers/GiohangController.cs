using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using baggageonlinestores.Models;
namespace baggageonlinestores.Controllers
{
    public class GiohangController : Controller
    {
        QLbanhangonlineDataContext data = new QLbanhangonlineDataContext();
        //
        // GET: /Giohang/
        public ActionResult Index()
        {
            return View();
        }
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        //Them Gio Hang
        public ActionResult ThemGiohang(int iMasanpham, string strURL)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.Find(n => n.iMasanpham == iMasanpham);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMasanpham);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }
        //Tong So luong
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);

            }
            return iTongSoLuong;
        }
        //Tong Tien
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                dTongTien = lstGiohang.Sum(n => n.dThanhtien);

            }
            return dTongTien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            //if (lstGiohang.Count == 0)
            //{
            //    return RedirectToAction("Index", "QLBanDongHo");
            //}
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        // xóa giỏ hàng
        public ActionResult Xoagiohang(int iMaSp)
        {
            // lây giỏ hàng từ session
            List<GioHang> lstGiohang = Laygiohang();
            // kiểm tra sản phẩm đã có trong giỏ hàng
            GioHang Sanpham = lstGiohang.SingleOrDefault(n => n.iMasanpham == iMaSp);
            // nếu tồn tại thì cho sửa số lượng
            if(Sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMasanpham == iMaSp);
                return RedirectToAction("Giohang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Giohang");
        }
        [HttpGet]
        public ActionResult Dathang()
        {
            // kiểm tra đăng nhập
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "Nguoidung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // lấy giỏ hàng từ session
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
         public ActionResult DatHang(FormCollection collection)
        {
             // thêm đơn hàng
            dondathang ddh = new dondathang();
            khachhang kh = (khachhang)Session["Taikhoan"];
            List<GioHang> gh = Laygiohang();
            ddh.MaKH = kh.MaKH;
            ddh.Ngaydat = DateTime.Now;
            var ngaygiao = string.Format("{0:MM/dd/yy}", collection["Ngaygiao"]);
            ddh.Ngaygiao = DateTime.Parse(ngaygiao);
            ddh.Tinhtranggiaohanag = false;
            ddh.Dathanhtoan = false;
            data.dondathangs.InsertOnSubmit(ddh);
            data.SubmitChanges();
             foreach(var item in gh)
             {
                 chitietdonhang ctdh = new chitietdonhang();
                 ctdh.Madonhang = ddh.Madonhang;
                 ctdh.Mahang = item.iMasanpham;
                 ctdh.Soluong = item.iSoluong;
                 ctdh.Dongia = (decimal)item.dDongia;
                 data.chitietdonhangs.InsertOnSubmit(ctdh);
             }
             data.SubmitChanges();
             Session["Giohang"] = null;
             return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        
        public ActionResult Xacnhandonhang()
         {
             return View();
         }
	}
}