using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using School_Management_System.Areas.AdminArea.Models;

namespace School_Management_System.Areas.AdminArea.ViewModels
{
    public class ClassSectionVM
    {

        //Class Table
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassName_Numeric { get; set; }

        // Section Table
        public int SectionID { get; set; }
        public string SectionName { get; set; }
        public string NickName { get; set; }

        [DefaultValue(1)]
        public bool IsActive { get; set; }



    }
}