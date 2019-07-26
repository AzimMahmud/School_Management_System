using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using School_Management_System.Areas.AdminArea.Models;
using School_Management_System.Areas.AdminArea.ViewModels;

namespace School_Management_System.Areas.AdminArea.Controllers
{
    [RouteArea("AdminArea")]
    [RoutePrefix("Section")]
    public class SectionController : Controller
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
            var classes = from c in _db.Sections
                          where c.IsActive == true
                          select new
                          {
                              c.SectionID,
                              c.SectionName,
                              c.NickName,
                              c.IsActive
                          };


            return Json(new { data = classes }, JsonRequestBehavior.AllowGet);
        }

        [Route("AddSection")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSection(Section section)
        {
            if (ModelState.IsValid)
            {
                section.IsActive = true;
                _db.Sections.Add(section);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");


        }


        public JsonResult LoadSection(int? id)
        {
            //List<Class> classData = _db.Classes.ToList<Class>();
            var classes = from c in _db.Sections
                          where c.SectionID == id
                          select new
                          {
                              c.SectionID,
                              c.SectionName,
                              c.NickName,
                              c.IsActive
                          };


            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditSection(int id)
        {

            Section section = _db.Sections.Find(id);
            var sectionVm = Mapper.Map<ClassSectionVM>(section);


            return PartialView("_SectionEdit", sectionVm);
        }

        [Route("EditClass")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSection(ClassSectionVM sectionVm)
        {
            Section section = _db.Sections.SingleOrDefault(c => c.SectionID == sectionVm.SectionID);
            var sections = Mapper.Map<Section>(section);
            if (section == null)
            {
                return HttpNotFound();
            }

            sections.SectionID = sectionVm.SectionID;
            sections.SectionName = sectionVm.SectionName;
            sections.NickName = sectionVm.NickName;
            sections.IsActive = sectionVm.IsActive;

            _db.SaveChanges();



            return RedirectToAction("Index");
        }

        [Route("DeleteSection")]

        //[ValidateAntiForgeryToken]
        public ActionResult DeleteSection(int? id)
        {
            Section section = _db.Sections.SingleOrDefault(c => c.SectionID == id);
           
            if (section == null)
            {
                return HttpNotFound();
            }

            section.IsActive = false;

            _db.SaveChanges();



            return RedirectToAction("Index");
        }

        
    }
}