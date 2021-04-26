using RubikStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubikStore.Controllers
{
    public class GioHangController : Controller
    {
        RubikStoreDataContext data = new RubikStoreDataContext();

        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;

            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }

            return lstGiohang;
        }

        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();

            Giohang sanpham = lstGiohang.Find(n => n.Id == id);

            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLg++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;

            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;

            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => n.iSoLg);
            }

            return tsl;
        }

        private int TongSoLuongSanPham()
        {
            int tsl = 0;

            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;

            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }

            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;

            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;

            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(n => n.dThanhtien);
            }

            return tt;
        }

        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();

            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();

            return View(lstGiohang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();

            return PartialView();
        }

        public ActionResult XoaGiohang(int id)
        {
            List<Giohang> lstGiohang = Laygiohang();

            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.Id == id);

            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.Id == id);
                return RedirectToAction("GioHang");
            }

            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<Giohang> lstGiohang = Laygiohang();

            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.Id == id);

            if (sanpham != null)
            {
                sanpham.iSoLg = int.Parse(collection["txtSoLg"].ToString());
            }

            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();

            lstGiohang.Clear();

            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "RubikStore");
            }

            List<Giohang> lstGiohang = Laygiohang();

            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();

            return View(lstGiohang);
        }

        public ActionResult DatHang(FormCollection collection)
        {
            DonDatHang ddh = new DonDatHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            Rubik rb = new Rubik();

            List<Giohang> gh = Laygiohang();

            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);

            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            ddh.NgayGiao = DateTime.Parse(ngaygiao);
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;

            data.DonDatHangs.InsertOnSubmit(ddh);
            data.SubmitChanges();

            foreach (var item in gh)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();

                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.Id = item.Id;
                ctdh.SoLuong = item.iSoLg;
                ctdh.DonGia = (decimal)item.Gia;


                rb = data.Rubiks.Single(n => n.Id == item.Id);
                rb.SoLgTon -= ctdh.SoLuong;
                data.SubmitChanges();


                data.ChiTietDonHangs.InsertOnSubmit(ctdh);
            }
            
            data.SubmitChanges();

            Session["Giohang"] = null;

            return RedirectToAction("XacnhanDonhang", "GioHang");
        }

        public ActionResult XacnhanDonhang()
        {
            return View();
        }
    }
}