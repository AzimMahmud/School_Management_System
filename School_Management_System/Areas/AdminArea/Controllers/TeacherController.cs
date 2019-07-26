using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using School_Management_System.Areas.AdminArea.Models;
using School_Management_System.Areas.AdminArea.ViewModels;
using School_Management_System.Models;

namespace School_Management_System.Areas.AdminArea.Controllers
{
    [RouteArea("AdminArea")]
    [RoutePrefix("Teacher")]
    [Authorize]

    public class TeacherController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly SMSEntities _db = new SMSEntities();


        public TeacherController()
        {
        }
        public TeacherController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [Route("Details")]
        public ActionResult Index()
        {
            return View();
        }


        [Route("LoadAllData")]
        public JsonResult LoadAllTeacher()
        {
            var teacher = from t in _db.Teachers
                where  t.IsActive == true
                select new
                {
                    t.TeacherID,
                   t.TeacherName,
                   t.Designation,
                   t.BirthDay,
                   t.Image
                };


            return Json(new { data = teacher }, JsonRequestBehavior.AllowGet);
        }

        [Route("AddTeacher")]
        public ActionResult AddTeacher()
        {
            return PartialView("_AddTeacherView");
        }


        [Route("Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTeacher(TeacherVM teacherVm)
        {

            Teacher teacher = new Teacher();

            RegisterViewModel model = new RegisterViewModel()
            {
                Email = teacherVm.Email,
                Password = teacherVm.Password,
                ConfirmPassword = teacherVm.ConfirmPassword
            };



            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    teacher.UserID = user.Id;
                    teacher.TeacherName = teacherVm.TeacherName;
                    teacher.Designation = teacherVm.Designation;
                    teacher.BirthDay = teacherVm.BirthDate;
                    teacher.Gender = teacherVm.Gender;
                    teacher.Address = teacherVm.Address;
                    teacher.Image = "dff";
                    teacher.IsActive = true;
                   


                    _db.Teachers.Add(teacher);
                    _db.SaveChanges();



                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    //return RedirectToAction("Index", "Home");
                    return Content("Sucess");
                }
            }

            // If we got this far, something failed, redisplay form
            return Content("fail");
        }


        [Route("EditTeacher")]
        public ActionResult EditTeacher(int? id)
        {
            
            Teacher teacher = _db.Teachers.Find(id);

            //var teachervm = Mapper.Map<Teacher, TeacherVM>(teacher);
           

            return PartialView("_EditTeacherView", teacher);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTeacher(Teacher teacher)
        {

            _db.Entry(teacher).State = EntityState.Modified;
            _db.SaveChanges();


            return RedirectToAction("Index");
        }

        [Route("DeleteTeacher")]
        [HttpPost] 
        public ActionResult DeleteTeacher(int? id)
        {

            Teacher teacher = _db.Teachers.Find(id);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            teacher.IsActive = false;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }



        [Route("ArchiveData")]
        public JsonResult ArchiveData()
        {
            var teacher = from t in _db.Teachers
                where t.IsActive == false
                select new
                {
                    t.TeacherID,
                    t.TeacherName,
                    t.Designation,
                    t.BirthDay,
                    t.Image
                };


            return Json(new { data = teacher }, JsonRequestBehavior.AllowGet);
        }

        [Route("RestoreTeacher")]
        [HttpPost]
        public ActionResult RestoreTeacher(int? id)
        {

            Teacher teacher = _db.Teachers.Find(id);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            teacher.IsActive = false;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("UndoDeletedData")]
        [HttpPost]
        public ActionResult UndoDeletedData(int? id)
        {

            Teacher teacher = _db.Teachers.Find(id);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            teacher.IsActive = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}