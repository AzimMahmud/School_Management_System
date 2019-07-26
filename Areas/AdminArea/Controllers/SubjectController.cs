using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using School_Management_System.Areas.AdminArea.Models;
using School_Management_System.Areas.AdminArea.ViewModels;

namespace School_Management_System.Areas.AdminArea.Controllers
{
    [RouteArea("AdminArea")]
    [RoutePrefix("Subject")]
    [Authorize]

    public class SubjectController : Controller
    {
        private readonly SMSEntities _db = new SMSEntities();
        // GET: AdminArea/Subject
        [Route("Details")]
        public ActionResult Index()
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
            var subjects = from bt in _db.Broker_SubjectTeacher
                join c in _db.Classes on bt.ClassID equals c.ClassID
                join t in _db.Teachers on bt.TeacherID equals t.TeacherID 
                where c.IsActive == true
                select new
                {

                    c.ClassID,
                    c.ClassName,
                    bt.ID,
                    bt.SubjectName,
                    t.TeacherID,
                    t.TeacherName
                };


            return Json(new {data = subjects }, JsonRequestBehavior.AllowGet);
        }


        //    public ActionResult AddSubject()
        //    {

        //    }
      

        [Route("AddSubject")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSubject(SubjectVM subjectVM)
        {
            if (ModelState.IsValid)
            {
                var subject = Mapper.Map<Broker_SubjectTeacher>(subjectVM);
                _db.Broker_SubjectTeacher.Add(subject);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");


        }


        public JsonResult LoadSubject(int? id)
        {
            var subjects = from bt in _db.Broker_SubjectTeacher
                join c in _db.Classes on bt.ClassID equals c.ClassID
                join t in _db.Teachers on bt.TeacherID equals t.TeacherID
                where c.IsActive == true
                select new
                {

                    c.ClassID,
                    c.ClassName,
                    bt.ID,
                    bt.SubjectName,
                    t.TeacherID,
                    t.TeacherName
                };



            return Json(subjects, JsonRequestBehavior.AllowGet);
        }

        [Route("EditSubject")]

        public ActionResult EditSubject(int? id)
        {

            Broker_SubjectTeacher subjects = _db.Broker_SubjectTeacher.Find(id);
            var subjectList = Mapper.Map<SubjectVM>(subjects);


            if (subjects == null)
            {
                return HttpNotFound();
            }


            ViewBag.Classes = new SelectList(_db.Classes, "ClassID", "ClassName", subjects.ClassID);
            ViewBag.Teachers = new SelectList(_db.Teachers, "TeacherID", "TeacherName", subjects.TeacherID);

            return PartialView("_SubjectEditView", subjectList);
        }

        [Route("EditSubject")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubject(SubjectVM subjectVM)
        {
            if (ModelState.IsValid)
            {
                var subject = Mapper.Map<Broker_SubjectTeacher>(subjectVM);
                _db.Entry(subject).State = EntityState.Modified;
                _db.SaveChanges();
                return Content("Index");

            }



            return RedirectToAction("Index");
        }

        [Route("DeleteSubject")]

        //[ValidateAntiForgeryToken]
        public ActionResult DeleteClass(int? id)
        {
            Broker_SubjectTeacher subject = _db.Broker_SubjectTeacher.Find(id);

            _db.Broker_SubjectTeacher.Remove(subject);

            _db.SaveChanges();



            return RedirectToAction("Index");
        }
    }
}