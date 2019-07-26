using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace School_Management_System.Areas.AdminArea.ViewModels
{
    public class StudentVM
    {

        //Student Table
        public int StudentID { get; set; }
        [Display(Name = "Registration No")]
        public string StudentRegID { get; set; }
        [Display(Name = "Name")]
        public string StudentName { get; set; }

        [Display(Name = "Parent")]
        public int ParentID { get; set; }

        [Display(Name = "Date of Birth")]
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        [Display(Name = "Photo")]
        public string Image { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        // AspNetUser Table

        //[Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone No")]
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }


        // AssignStudentToClass Table
        [Display(Name = "Role No")]
        public int RollNo { get; set; }
        public string SessionYear { get; set; }
        [Display(Name = "Class")]
        public int ClassSectionID { get; set; }
        public int SectionID { get; set; }
        public int ClassID { get; set; }
        public string PresentStatus { get; set; }
    }
}