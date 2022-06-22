﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SMSEntities : DbContext
    {
        public SMSEntities()
            : base("name=SMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Accountant> Accountants { get; set; }
        public virtual DbSet<ArchivedStudent> ArchivedStudents { get; set; }
        public virtual DbSet<AssignStudentdToClass> AssignStudentdToClasses { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Broker_ClassSection> Broker_ClassSection { get; set; }
        public virtual DbSet<Broker_SubjectTeacher> Broker_SubjectTeacher { get; set; }
        public virtual DbSet<Broker_TeacherSection> Broker_TeacherSection { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<Notice> Notices { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<Routine> Routines { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<StudentPayment> StudentPayments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
    }
}