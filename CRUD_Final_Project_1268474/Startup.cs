using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(CRUD_Final_Project_1268474.Startup))]

namespace CRUD_Final_Project_1268474
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions

            {

                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,

                LoginPath = new PathString("/Account/Login")

            });
        }
    }
}
