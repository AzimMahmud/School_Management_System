//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace School_Management_System.Areas.AdminArea.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Grade
    {
        public int GradeID { get; set; }
        public string GradeName { get; set; }
        public decimal GradePoint { get; set; }
        public int GradeFrom { get; set; }
        public int GradeUpto { get; set; }
        public string Comment { get; set; }
    }
}
