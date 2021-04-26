using RubikStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubikStore.Controllers
{
    public class NguoiDungController : Controller
    {
        RubikStoreDataContext data = new RubikStoreDataContext();

        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var HoTen = collection["HoTen"];
            var TaiKhoan = collection["TaiKhoan"];
            var MatKhau = collection["MatKhau"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];
            var Email = collection["Email"];
            var DiaChiKH = collection["DiaChiKH"];
            var DienThoaiKH = collection["DienThoaiKH"];
            var NgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);

            if (String.IsNullOrEmpty(MatKhauXacNhan))
            {
                ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";
            }
            else
            {
                if (!MatKhau.Equals(MatKhauXacNhan))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                }
                else
                {
                    kh.HoTen = HoTen;
                    kh.TaiKhoan = TaiKhoan;
                    kh.MatKhau = MatKhau;
                    kh.Email = Email;
                    kh.DiaChiKH = DiaChiKH;
                    kh.DienThoaiKH = DienThoaiKH;
                    kh.NgaySinh = DateTime.Parse(NgaySinh);

                    data.KhachHangs.InsertOnSubmit(kh);
                    data.SubmitChanges();

                    return RedirectToAction("DangNhap");
                }
            }

            return this.DangKy();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var TaiKhoan = collection["TaiKhoan"];
            var MatKhau = collection["MatKhau"];

            KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.TaiKhoan == TaiKhoan && n.MatKhau == MatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["TaiKhoan"] = kh;
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }

            return RedirectToAction("Index", "RubikStore");
        }
    }
}