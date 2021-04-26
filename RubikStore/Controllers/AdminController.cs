using RubikStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using PagedList.Mvc;

namespace RubikStore.Controllers
{
    public class AdminController : Controller
    {
        RubikStoreDataContext data = new RubikStoreDataContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Rubik(int ? page)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            int pageSize = 4;
            int pageNum = page ?? 1;

            return View(data.Rubiks.ToList().OrderBy(n => n.Id).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var TenDN = collection["UserAdmin"];
            var MatKhau = collection["PassAdmin"];

            AdminAccount ad = data.AdminAccounts.SingleOrDefault(n => n.UserAdmin == TenDN && n.PassAdmin == MatKhau);
            
            if (ad != null)
            {
                Session["TaiKhoanAdmin"] = ad;

                return RedirectToAction("Rubik", "Admin");
            }
            else
            {
                ViewBag.Thongbao = "Tên đăng nhập hoặc mặt khẩu không đúng";
            }

            return View();
        }
    }
}