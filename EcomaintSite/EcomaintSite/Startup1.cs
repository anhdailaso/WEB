using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System.Web.Services.Description;
using Microsoft.AspNet.Identity.EntityFramework;
[assembly: Microsoft.Owin.OwinStartup(typeof(EcomaintSite.Startup1))]
namespace EcomaintSite
{
    public partial class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString("/sign-in"),
                LogoutPath = new Microsoft.Owin.PathString("/sign-in"),                
                ExpireTimeSpan = TimeSpan.FromMinutes(5), //Timeout
                SlidingExpiration = true
            });
        }
    }
}
