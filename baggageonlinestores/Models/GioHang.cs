using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using baggageonlinestores.Models;
using System.Web.Mvc;
namespace baggageonlinestores.Models
{
    public class GioHang
    {
        QLbanhangonlineDataContext db = new QLbanhangonlineDataContext();
        public int iMasanpham { set; get; }
        public string sTensanpham { set; get; }
        public string sAnhbia { set; get; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }

        public GioHang(int Masanpham)
        {
            iMasanpham = Masanpham;
            sanpham sanpham = db.sanphams.Single(n => n.Masanpham == iMasanpham);
            sTensanpham = sanpham.Tensanpham;
            sAnhbia = sanpham.Anhbia;
            dDongia = Double.Parse(sanpham.Giaban.ToString());
            iSoluong = 1;

        }
    }
}