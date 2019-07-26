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
    [RoutePrefix("Class")]
    public class ClassController : Controller
    {

        private readonly SMSEntities _db = new SMSEntities();
        // GET: AdminArea/Class
        [Route("Details")]
        public ActionResult Index()
        {
            return View();
        }


        [Route("ViewAll")]

        public JsonResult ViewAll()
        {
            //List<Class> classData = _db.Classes.ToList<Class>();
            var classes = from c in _db.Classes
                where  c.IsActive == true
                select new
                {
                    c.ClassID,
                    c.ClassName,
                    c.ClassName_Numeric,
                    c.IsActive
                };

            
            return Json(new { data = classes }, JsonRequestBehavior.AllowGet);
        }

        [Route("AddClass")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClass(Class classes)
        {
            if (ModelState.IsValid)
            {
                classes.IsActive = true;
                _db.Classes.Add(classes);
                _db.SaveChanges();
                return  RedirectToAction("Index");
            }

            return RedirectToAction("Index");


        }


        public JsonResult LoadClass(int? id)
        {
            //List<Class> classData = _db.Classes.ToList<Class>();
            var classes = from c in _db.Classes
                where c.ClassID == id 
                select new
                {
                    c.ClassID,
                    c.ClassName,
                    c.ClassName_Numeric,
                    c.IsActive
                };


            return Json( classes , JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditClass(int id)
        {

            Class classes = _db.Classes.Find(id);
            var classvm = Mapper.Map<ClassSectionVM>(classes);

         
            return PartialView("_ClassEdit", classvm);
        }

        [Route("EditClass")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClass(ClassSectionVM classVm)
        {
            Class classes = _db.Classes.SingleOrDefault(c => c.ClassID == classVm.ClassID);
            var classvm = Mapper.Map<Class>(classVm);
            if (classes == null)
            {
                return HttpNotFound();
            }

            classvm.ClassID = classVm.ClassID;
            classvm.ClassName = classVm.ClassName;
            classvm.ClassName_Numeric = classVm.ClassName_Numeric;
            classvm.IsActive = classVm.IsActive;

            _db.SaveChanges();



            return RedirectToAction("Index");
        }

        [Route("DeleteClass")]

        //[ValidateAntiForgeryToken]
        public ActionResult DeleteClass(int? id)
        {
            Class classes = _db.Classes.SingleOrDefault(c => c.ClassID == id);
            if (classes == null)
            {
                return HttpNotFound();
            }

            classes.IsActive = false;

            _db.SaveChanges();



            return RedirectToAction("Index");
        }
    }
}