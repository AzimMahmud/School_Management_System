using System.Web.Mvc;

namespace School_Management_System.Areas.AdminArea
{
    public class AdminAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
           

            context.MapRoute(
                "AdminArea_Default",
                "AdminArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            

        }
    }
}