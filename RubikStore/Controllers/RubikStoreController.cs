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
    public class RubikStoreController : Controller
    {
        RubikStoreDataContext data = new RubikStoreDataContext();

        private List<Rubik> Layrubikmoi(int count)
        {
            return data.Rubiks.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

        // GET: RubikStore
        public ActionResult Index(int ? page)
        {
            //RubikList strR = new RubikList();
            //List<QLRubik> obj = strR.getRubik("");
            //return View(obj);

            //var rubikmoi = Layrubikmoi(5);
            //return View(rubikmoi);

            int pageSize = 3;
            int pageNum = page ?? 1;

            var rubikmoi = Layrubikmoi(data.Rubiks.Count());

            return View(rubikmoi.ToPagedList(pageNum, pageSize));
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }

            file.SaveAs(Server.MapPath("~/Content/Images/" + file.FileName));

            return "/Content/Images/" + file.FileName;
        }

        public ActionResult Create()
        {
            ViewBag.Loai = new SelectList(data.LoaiRubiks.ToList().OrderBy(n => n.Id), "Id", "Loai");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(QLRubik strR)
        {
            if (ModelState.IsValid)
            {
                RubikList R = new RubikList();
                R.addRubik(strR);
                return RedirectToAction("Index");
            }

            return View();
        }
        
        public ActionResult Edit(string id = "")
        {
            RubikList R = new RubikList();
            List<QLRubik> obj = R.getRubik(id);
            return View(obj.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(QLRubik strR)
        {
            RubikList R = new RubikList();
            R.editRubik(strR);
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(string id = "")
        {
            RubikList R = new RubikList();
            List<QLRubik> obj = R.getRubik(id);
            return View(obj.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(QLRubik strR)
        {
            RubikList R = new RubikList();
            R.delBook(strR);
            return RedirectToAction("Index");
        }
        
        public ActionResult Details(string id = "")
        {
            RubikList R = new RubikList();
            List<QLRubik> obj = R.getRubik(id);
            return View(obj.FirstOrDefault());
        }
    }
}