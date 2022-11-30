using CRUD_Final_Project_1268474.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading;

namespace CRUD_Final_Project_1268474.Controllers
{
    [Authorize]
    public class ModuleController : Controller
    {
        CourseDbContext db = new CourseDbContext();
        public ActionResult Index()
        {
            return View(db.Modules);
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateModule()
        {
            return PartialView("_CreateModule");
        }
        [HttpPost]
        public PartialViewResult CreateModule(Module b)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                db.Modules.Add(b);
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public PartialViewResult EditModule(int id)
        {
            var b = db.Modules.First(x => x.ModuleID == id);
            return PartialView("_EditModule", b);
        }
        [HttpPost]
        public PartialViewResult EditModule(Module b)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(db.Modules.First(x => x.ModuleID == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int ModuleID)
        {
            var b = new Module { ModuleID = ModuleID };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}