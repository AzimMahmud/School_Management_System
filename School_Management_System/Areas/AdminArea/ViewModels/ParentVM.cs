using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace School_Management_System.Areas.AdminArea.ViewModels
{
    public class ParentVM
    {
        public int ParentID { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string NID { get; set; }
        public string UserID { get; set; }
        [DefaultValue(1)]
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