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
    public class ExamController : Controller
    {
        CourseDbContext db = new CourseDbContext();
        public ActionResult Index()
        {
            return View(db.Exams);
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateExam()
        {
            return PartialView("_CreateExam");
        }
        [HttpPost]
        public PartialViewResult CreateExam(Exam b)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                db.Exams.Add(b);
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
        public PartialViewResult EditExam(int id)
        {
            var b = db.Exams.First(x => x.ExamID == id);
            return PartialView("_EditExam", b);
        }
        [HttpPost]
        public PartialViewResult EditExam(Exam b)
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
            return View(db.Exams.First(x => x.ExamID == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int ExamID)
        {
            var b = new Exam { ExamID = ExamID };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}