using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School_Management_System.Areas.AdminArea.Controllers
{
    [RouteArea("AdminArea")]
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
        // GET: AdminArea/Admin
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}