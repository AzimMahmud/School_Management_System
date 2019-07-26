using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using School_Management_System.Areas.AdminArea.Models;
using School_Management_System.Areas.AdminArea.ViewModels;
using School_Management_System.Models;

namespace School_Management_System.Areas.AdminArea.Controllers
{
    [RouteArea("AdminArea")]
    [RoutePrefix("Student")]
    [Authorize]
    public class StudentController : Controller
    {

        private ApplicationUserManager _userManager;
        private readonly SMSEntities _db = new SMSEntities();

        private int _rollNo;
        private int _setRollNo()
        {
            var roll = _db.AssignStudentdToClasses.OrderByDescending(c=>c.RollNo).Take(1).FirstOrDefault();

            if (roll == null)
            {
                _rollNo = 1;
            }
            else
            {
                _rollNo = roll.RollNo + 1;
            }
            return _rollNo;
        }


       
        private int _setRegNo()
        {
            var student = _db.Students.OrderByDescending(c => c.StudentRegID).Take(1).FirstOrDefault();
            var regid = 0;

            if (student != null)
            {
                regid  = Int16.Parse(student.StudentRegID.Substring(6,student.StudentRegID.Length - 6)) + 1;
            }
            else
            {
                regid = 1;
            }

           




            return regid;
        }

        public StudentController()
        {
        }
        public StudentController(ApplicationUserManager userManager)
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

        // GET: AdminArea/Student
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Add")]
        [HttpGet]
        public ActionResult AdmitStudent()
        {
            var parents = (from p in _db.Parents
                join u in _db.AspNetUsers on p.UserID equals u.Id
                select new
                {
                    u.UserName,
                    p.ParentID
                }).AsEnumerable();


            var classes = _db.Classes.Join(_db.Broker_ClassSection, c => c.ClassID, s => s.ClassID, (c, s) => new
            {
                ClassID = c.ClassID,
                ClassName = c.ClassName 
            }).AsEnumerable();


            ViewBag.ParentID = new SelectList(parents, "ParentID", "UserName");
            ViewBag.ClassID = new SelectList(classes, "ClassID", "ClassName");
           

            return View();
        }

        [Route("GetSections/{classid}")]
        public  ActionResult GetSections(int classid)
        {

            var section = (from bt in _db.Broker_ClassSection
                join c in _db.Classes on bt.ClassID equals c.ClassID 
                join s in _db.Sections on bt.SectionID equals s.SectionID
                where c.ClassID == classid
                select new 
                {
                    s.SectionID, s.SectionName
                }).AsEnumerable();

            ViewBag.Sections = new SelectList(section, "SectionID", "SectionName");

            return PartialView("_SectionList");
        }


        // POST: /Account/Register
        [Route("Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdmitStudent(StudentVM studentVm)
        {



            Student student = new Student();
            AssignStudentdToClass assignStudent = new AssignStudentdToClass();

            RegisterViewModel model = new RegisterViewModel()
            {
                Email = studentVm.Email,
                Password = studentVm.Password,
                ConfirmPassword = studentVm.ConfirmPassword
            };

            var classSectionID = _db.Broker_ClassSection.Where(c => c.ClassID == studentVm.ClassID).Where(c=>c.SectionID == studentVm.SectionID).SingleOrDefault();


            if (studentVm.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(studentVm.ImageUpload.FileName);
                string extension = Path.GetExtension(studentVm.ImageUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                studentVm.Image = "Areas/AdminArea/Images/" + fileName;
                studentVm.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Areas/AdminArea/Images/"), fileName));
            }


            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    student.StudentName = studentVm.StudentName;


                    student.StudentRegID = "S-" + DateTime.Now.Year + _setRegNo();
                    student.ParentID = studentVm.ParentID;
                    student.BirthDate = studentVm.BirthDate;
                    student.Gender = studentVm.Gender;
                    student.Image = studentVm.Image;
                    student.UserID = user.Id;
                    student.IsActive = true;

                   




                    _db.Students.Add(student);
                    _db.SaveChanges();

                   
                        assignStudent.RollNo = _setRollNo();
                        assignStudent.SessionYear = DateTime.Now.Year.ToString();
                        assignStudent.StudentID = student.StudentID;


                        assignStudent.ClassSectionID = classSectionID.ID;
                        assignStudent.PresentStatus = "pending";

                        _db.AssignStudentdToClasses.Add(assignStudent);
                        _db.SaveChanges();
                    

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    //return RedirectToAction("Index", "Home");
                    return RedirectToAction("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index");
        }

        [Route("ViewAll")]

        public JsonResult ViewAll()
        {
            //List<Class> classData = _db.Classes.ToList<Class>();
            var classes = from s in _db.Students
                where s.IsActive == true
                select new
                {
                    s.StudentID,
                    s.StudentRegID,
                    s.StudentName,
                    s.BirthDate,
                    s.Image
                };


            return Json(new { data = classes }, JsonRequestBehavior.AllowGet);
        }


    }
}