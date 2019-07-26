using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School_Management_System.Areas.AdminArea.ViewModels
{
    public class TeacherVM
    {
        public int TeacherID { get; set; }

        [Display(Name = "Name")]
        public string TeacherName { get; set; }

        public string Designation { get; set; }

        [Display(Name = "Date of Birth")]
        public string BirthDate { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        [Display(Name = "Photo")]
        public string Image { get; set; }

        public bool IsActive { get; set; }


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

        //[Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]

        public string UserName { get; set; }
    }
}