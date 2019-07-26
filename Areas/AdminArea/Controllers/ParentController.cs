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
    [RoutePrefix("Parent")]
    public class ParentController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly SMSEntities _db = new SMSEntities();


        public ParentController()
        {
        }
        public ParentController(ApplicationUserManager userManager)
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
        public JsonResult LoadAllParent()
        {
            var parent = from p in _db.Parents
                where p.IsActive == true
                select new
                {
                    p.ParentID,
                    p.Name,
                    p.NID,
                    p.Profession
                    
                };


            return Json(new { data = parent }, JsonRequestBehavior.AllowGet);
        }

        [Route("AddParent")]
        public ActionResult AddParent()
        {
            return PartialView("_AddParentView");
        }


        [Route("Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddParent(ParentVM parentVM)
        {

            Parent parent = new Parent();

            RegisterViewModel model = new RegisterViewModel()
            {
                Email = parentVM.Email,
                Password = parentVM.Password,
                ConfirmPassword = parentVM.ConfirmPassword
            };



            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email , PhoneNumber = parentVM.PhoneNumber};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    parent.UserID = user.Id;
                    parent.Profession= parentVM.Profession;
                    parent.NID= parentVM.NID;
                    parent.Name = parentVM.Name;
                    parent.IsActive = true;
                   



                    _db.Parents.Add(parent);
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


        [Route("Edit")]
        public ActionResult EditParent(int? id)
        {

            Parent parent = _db.Parents.Find(id);

            var parentvm = Mapper.Map<Parent, ParentVM>(parent);


            return PartialView("_EditParentView", parentvm);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTeacher(ParentVM parentVm)
        {

            var parent = Mapper.Map<Parent>(parentVm);


            if (ModelState.IsValid)
            {
                parent.IsActive = true;
                _db.Entry(parent).State = EntityState.Modified;
                _db.SaveChanges();
                
            }


            return RedirectToAction("Index");
        }

        [Route("DeleteParent")]
        [HttpPost]
        public ActionResult DeleteParent(int? id)
        {

            Parent parent = _db.Parents.Find(id);

            if (parent == null)
            {
                return HttpNotFound();
            }

            parent.IsActive = false;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }



        [Route("ArchiveData")]
        public JsonResult ArchiveData()
        {
            var teacher = from p in _db.Parents
                          where p.IsActive == false
                          select new
                          {
                              p.ParentID,
                              p.Name,
                              p.NID,
                              p.Profession
                          };


            return Json(new { data = teacher }, JsonRequestBehavior.AllowGet);
        }

       

        [Route("UndoDeletedData")]
        [HttpPost]
        public ActionResult UndoDeletedData(int? id)
        {

            Parent parent = _db.Parents.Find(id);

            if (parent == null)
            {
                return HttpNotFound();
            }

            parent.IsActive = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}