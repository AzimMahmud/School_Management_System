using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using School_Management_System.Areas.AdminArea.Models;
using School_Management_System.Areas.AdminArea.ViewModels;

namespace School_Management_System
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(config =>
            {
                config.CreateMap<Class, ClassSectionVM>();
                config.CreateMap<ClassSectionVM, Class>();

                config.CreateMap<Section, ClassSectionVM>();
                config.CreateMap<ClassSectionVM, Section>();

                config.CreateMap<ClassSectionVM, Broker_ClassSection>();
                config.CreateMap<Broker_ClassSection, ClassSectionVM>();

                config.CreateMap<SubjectVM, Broker_SubjectTeacher>();
                config.CreateMap<Broker_SubjectTeacher, SubjectVM>();

                config.CreateMap<Parent, ParentVM>();
                config.CreateMap<ParentVM, Parent>();
            });
        }
    }
}
