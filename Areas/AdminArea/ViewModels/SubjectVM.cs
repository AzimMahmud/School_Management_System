using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Management_System.Areas.AdminArea.ViewModels
{
    public class SubjectVM
    {
        public int ID { get; set; } // Broker_Table  Subject ID
        public string SubjectName { get; set; }
        public int ClassID { get; set; }
        public int ClassName { get; set; }
        public int TeacherID { get; set; }
        public int TeacherName { get; set; }
    }
}