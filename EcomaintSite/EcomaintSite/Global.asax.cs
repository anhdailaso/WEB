using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace EcomaintSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DependencyResolver.SetResolver(new NinjectResolver());

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Application["UsersLoggedIn"] = new System.Collections.Generic.List<string>();
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 5;
        }
        protected void Session_End(object sender, EventArgs e)
        {
            // NOTE: you might want to call this from the .Logout() method - aswell -, to speed things up
            string userLoggedIn = Session["UserLoggedIn"] == null ? string.Empty : (string)Session["UserLoggedIn"];
            if (userLoggedIn.Length > 0)
            {
                if (Application["UsersLoggedIn"] is System.Collections.Generic.List<string> d)
                {
                    lock (d)
                    {
                        d.Remove(userLoggedIn.ToLower());
                        Model.Repository.WebUserLoginRepository webUserLogin = new Model.Repository.WebUserLoginRepository();
                        webUserLogin.Delete(userLoggedIn);
                        webUserLogin.SaveChanges();
                    }
                }
            }
        }
    }
}
