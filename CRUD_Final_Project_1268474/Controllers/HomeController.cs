using CRUD_Final_Project_1268474.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Final_Project_1268474.Controllers
{
    public class HomeController : Controller
    {
        CourseDbContext db = new CourseDbContext();
        public ActionResult Index()
        {
            var data = db.Courses.ToList();
            return View();
        }
    }
}