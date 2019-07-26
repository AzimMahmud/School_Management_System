using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using School_Management_System.Areas.AdminArea.Models;
using School_Management_System.Areas.AdminArea.ViewModels;

namespace School_Management_System.Areas.AdminArea.Controllers
{
    [RouteArea("AdminArea")]
    [RoutePrefix("AssignSection")]
    public class ClassSectionCombinationController : Controller
    {
        private SMSEntities _db = new SMSEntities();

        // GET: AdminArea/ClassSectionCombination
        [Route("Details")]
        public ActionResult Details()
        {

            var classes = (from c in _db.Classes
                select new
                {
                   c.ClassID,
                   c.ClassName
                }).AsEnumerable();


            var teachers = (from t in _db.Teachers
                join u in _db.AspNetUsers on t.UserID equals u.Id
                select new
                {
                    t.TeacherID,
                    t.TeacherName
                   
                }).AsEnumerable();


            ViewBag.Classes = new SelectList(classes, "ClassID", "ClassName");
            ViewBag.Teachers = new SelectList(teachers, "TeacherID", "TeacherName");

            return View();
        }

        [Route("ViewAll")]
        public JsonResult ViewAll()
        {
            //List<Class> classData = _db.Classes.ToList<Class>();
            var classes = from c in _db.Classes

                where c.IsActive == true
                select new
                {
                    c.ClassID,
                    c.ClassName,
                    c.ClassName_Numeric,
                    c.IsActive
                };


            return Json( classes.ToArray() , JsonRequestBehavior.AllowGet);
        }



        [Route("LoadAllData")]
        public JsonResult LoadClassSectionData(int? id)
        {
            //List<Class> classData = _db.Classes.ToList<Class>();
            var classes = from cs in _db.Broker_ClassSection
                join c in _db.Classes on cs.ClassID equals c.ClassID
                join s in _db.Sections on cs.SectionID equals s.SectionID 
                where c.IsActive == true && c.ClassID == id
                select new
                {
                    cs.ID,
                    c.ClassID,
                    c.ClassName,
                    s.SectionID,
                    s.SectionName
                };


            return Json(new {data = classes}, JsonRequestBehavior.AllowGet);
        }

        [Route("AssignClassSubject")]
        public ActionResult AssignClassSubject()
        {

            var classes= (from c in _db.Classes select  c).AsEnumerable();

            ViewBag.Classes = new SelectList(classes, "ClassID", "ClassName");
            var sections = (_db.Sections.Select(c => new
            {
                c.SectionID,
                c.SectionName
            }));

            ViewBag.Sections = new SelectList(sections, "SectionID", "SectionName");


            return PartialView("_AddOfEditClassSection");
        }

        [Route("Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClassSection(ClassSectionVM classSectionVM)
        {
            if (ModelState.IsValid)
            {
               

                var classSection = Mapper.Map<Broker_ClassSection>(classSectionVM);

               
                _db.Broker_ClassSection.Add(classSection);
                _db.SaveChanges();
                return Content("Success");


            }

            return Content("fail");
        }

    }
}