using baggageonlinestores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace baggageonlinestores.Controllers
{
    public class HomeController : Controller
    {
        QLbanhangonlineDataContext data = new QLbanhangonlineDataContext();
        private List<sanpham> LaySPmoi()
        {//sap xep theo ngay cap nhap , sau do lay top @ count
            return data.sanphams.OrderByDescending(a => a.Ngaycapnhat).ToList();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult shop(int ? page)
        {
            //tao bien quy dinh so san pham tre moi trang
            int pageSize = 12;
            //tao bien so trang
            int pageNum = (page ?? 1);
            //lay top 10 sp 
            var spmoi = LaySPmoi();
            return View(spmoi.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult single(int id)
        {
            var sp = from s in data.sanphams where s.Masanpham == id select s;
            return View(sp.Single());
           
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
    }
}