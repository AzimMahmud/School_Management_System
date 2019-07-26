using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(School_Management_System.Startup))]
namespace School_Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
