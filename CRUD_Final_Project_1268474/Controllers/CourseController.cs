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
    public class CourseController : Controller
    {
        CourseDbContext db = new CourseDbContext();

        public ActionResult Index()
        {
            var data = db.Courses.Include(x=>x.CourseModules.Select(y=>y.Module)).ToList();
            return View(data);
        }
        public ActionResult CreateCourseModule()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCourseModule(Course c, int[] ModuleID)
        {
            if (ModelState.IsValid)
            {
                foreach(var i in ModuleID)
                {
                    c.CourseModules.Add(new CourseModule { ModuleID = i });
                }
                db.Courses.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public PartialViewResult CreateModuleEntry()
        {
            ViewBag.Modules = db.Modules.ToList();
            return PartialView("_ModuleEntry");
        }
    }
    
}