using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRUD_Final_Project_1268474.ViewModels
{
    public class TraineeEditModel
    {
        public int TrainneID { get; set; }
        [Required, StringLength(50), Display(Name = "Trainee Name")]
        public string TrainneName { get; set; }
        [Required, StringLength(70), Display(Name = "Trainee Address")]
        public string TraineeAddress { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public HttpPostedFileBase Picture { get; set; }
        [Display(Name = "IsRunning")]
        public bool IsRunning { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Birth Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [Required, Display(Name = "Course Id")]
        public int CourseID { get; set; }
    }
}