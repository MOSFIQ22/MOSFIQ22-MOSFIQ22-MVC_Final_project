using CRUD_Final_Project_1268474.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CRUD_Final_Project_1268474.ViewModels;
using System.IO;
using System.Threading;

namespace CRUD_Final_Project_1268474.Controllers
{
    public class TraineeController : Controller
    {
        CourseDbContext db = new CourseDbContext();
       
        public ActionResult Index()
        {
            return View(db.Trainnes.Include(x => x.Course).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Courses = db.Courses.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(TraineeInputModel t)
        {
            if (ModelState.IsValid)
            {
                var trainee = new Trainne
                {
                    TrainneName = t.TrainneName,
                    TraineeAddress = t.TraineeAddress,
                    Email = t.Email,
                    BirthDate = t.BirthDate,
                    IsRunning = t.IsRunning,
                    CourseID = t.CourseID
                };
                string ext = Path.GetExtension(t.Picture.FileName);
                string f = Guid.NewGuid() + ext;
                t.Picture.SaveAs(Server.MapPath("~/Uploads/") + f);
                trainee.Picture = f;
                db.Trainnes.Add(trainee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Courses = db.Courses.ToList();
            return View(t);
        }
        public ActionResult Edit(int id)
        {
            var t = db.Trainnes.First(x => x.TrainneID == id);
            ViewBag.Courses = db.Courses.ToList();
            ViewBag.CurrentPic = t.Picture;
            return View(new TraineeEditModel
            {
                TrainneID = t.TrainneID,
                TrainneName = t.TrainneName,
                TraineeAddress = t.TraineeAddress,
                Email = t.Email,
                BirthDate = t.BirthDate,
                IsRunning = t.IsRunning,
                CourseID = t.CourseID
            });
        }
        [HttpPost]
        public ActionResult Edit(TraineeEditModel t)
        {
            Thread.Sleep(2000);
            var trainee = db.Trainnes.First(x => x.TrainneID == t.TrainneID);
            if (ModelState.IsValid)
            {
                trainee.TrainneName = t.TrainneName;
                trainee.TraineeAddress = t.TraineeAddress;
                trainee.Email = t.Email;
                trainee.BirthDate = t.BirthDate;
                trainee.IsRunning = t.IsRunning;
                if (t.Picture != null)
                {
                    string ext = Path.GetExtension(t.Picture.FileName);
                    string f = Guid.NewGuid() + ext;
                    t.Picture.SaveAs(Server.MapPath("~/Uploads/") + f);
                    trainee.Picture = f;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentPic = trainee.Picture;
            ViewBag.Courses = db.Courses.ToList();
            return View(t);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Trainnes.Include(x => x.Course).First(x => x.TrainneID == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Trainne t = new Trainne { TrainneID = id };
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}