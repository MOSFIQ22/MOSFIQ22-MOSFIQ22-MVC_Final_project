using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUD_Final_Project_1268474.Models
{
    public enum Result {pass=1, fail}
    public enum CourseName { CS= 1, Java, NT, Gave, DDD, PHP}
    public class Course
    {
        public int CourseID { get; set; }
        [Required, StringLength(35), Display(Name = "Batch Name")]
        public string BatchName { get; set; }
        [EnumDataType(typeof(CourseName))]
        public CourseName CourseName { get; set; }
        public decimal CourseCost { get; set; }
        [Required, StringLength(90), Display(Name = "Course Desc")]
        public string CourseDesc { get; set; }
        [Required, StringLength(30), Display(Name = "Course Duration")]
        public string CourseDuration { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date"), Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [Display(Name = "Available")]
        public bool Available { get; set; }
        public virtual ICollection<Trainne> Trainnes { get; set; } = new List<Trainne>();
        public virtual ICollection<CourseModule> CourseModules { get; set; } = new List<CourseModule>();

    }
    public class Module
    {
        public int ModuleID { get; set; }
        [Required, StringLength(40), Display(Name = "Module Name")]
        public string ModuleName { get; set; }
        [Required, StringLength(90), Display(Name = "Module Desc")]
        public string ModuleDesc { get; set; }
        [Required, StringLength(10), Display(Name = "Module Number"),]
        public string ModuleNumber { get; set; }
        public virtual ICollection<CourseModule> CourseModule { get; set; } = new List<CourseModule>();
    }
    public class CourseModule
    {
        [Key, Column(Order = 0), ForeignKey("Course")]
        public int CourseID { get; set; }
        [Key, Column(Order = 1), ForeignKey("Module")]
        public int ModuleID { get; set; }
        public Course Course { get; set; }
        public Module Module { get; set; }
    }
    public class Exam
    {
        public int ExamID { get; set; }
        [Required, StringLength(50),Display(Name = "Exam Name")]
        public string ExamName { get; set; }
        public decimal ExamFee { get; set; }
        public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }
    public class ExamResult
    {
        [Key, Column(Order = 0), ForeignKey("Exam")]
        public int ExamID { get; set; }
        [Key, Column(Order = 1), ForeignKey("Trainne")]
        public int TraineeID { get; set; }
        [EnumDataType(typeof(Result))] 
        public Result Result { get; set; }
        public Exam Exam { get; set; }
        public Trainne Trainne { get; set; }

    }
    public class Trainne
    {
        public int TrainneID { get; set; }
        [Required, StringLength(50), Display(Name = "Trainee Name")]
        public string TrainneName { get; set; }
        [Required, StringLength(70), Display(Name = "Trainee Address")]
        public string TraineeAddress { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(90)]
        public string Picture { get; set; }
        [Display(Name = "Is Running")]
        public bool IsRunning { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Birth Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }
        public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();

    }
    public class CourseDbContext : DbContext
    {
        public CourseDbContext()
        {
            Database.SetInitializer(new CourseDbInitializer());
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<CourseModule> CourseModules { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<Trainne> Trainnes { get; set; }
    }
    public class CourseDbInitializer : DropCreateDatabaseIfModelChanges<CourseDbContext>
    {
        protected override void Seed(CourseDbContext db)
        {
            Course c = new Course {BatchName="CS/ACSL/R-50", CourseName = CourseName.CS, CourseCost = 0.00M, CourseDesc = "Full IT Related Course", CourseDuration = "1 year", StartDate= new DateTime(2021, 01, 05), EndDate=new DateTime(2022, 02, 06), Available=true};
            Module m = new Module { ModuleName = "SQL Server", ModuleDesc = "All Sql Server Related things", ModuleNumber = "One" };
            Exam e = new Exam { ExamName = "Monthly", ExamFee=1100.00M};
            Trainne t = new Trainne { TrainneName = " Nur Sakib", TraineeAddress = "Mirpur-10",Email="nursakib47@gmail.com", IsRunning = true, BirthDate = new DateTime(1997, 01, 03), Picture = "1.jpg" };
            c.Trainnes.Add(t);
            CourseModule cm = new CourseModule { Course = c, Module = m };
            ExamResult ex = new ExamResult { Result =Result.pass, Exam = e, Trainne = t };

            db.Courses.Add(c);
            db.Modules.Add(m);
            db.CourseModules.Add(cm);
            db.Exams.Add(e);
            db.ExamResults.Add(ex);
            db.SaveChanges();
        }
    }
}