using CRUD_Final_Project_1268474.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace CRUD_Final_Project_1268474.Controllers
{  
    public class TraineeCourseRPTController : Controller
    {
        private CourseDbContext db = new CourseDbContext();
        public ActionResult Index()
        {
            return View(db.Courses.Include(b => b.Trainnes).ToList());
            
        }
        public ActionResult RPTCourseTrainee()
        {
            List<Course> allCourse = new List<Course>();
            List<Trainne> allTrainee = new List<Trainne>();
            allCourse = db.Courses.ToList();
            allTrainee = db.Trainnes.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "TrainneCourseReport.rpt"));

            rd.SetDataSource(allCourse);
            rd.SetDataSource(allTrainee);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CustomerList.pdf");
        }
    }
}