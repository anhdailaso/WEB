using Microsoft.AspNet.Identity;
using Model.Interface;
using System;
using System.Linq;
using System.Web.Mvc;

namespace EcomaintSite.Controllers
{
    public class DashboardController : Controller
    {
        IUserRepository user;
        IChartRepository chart;
        public DashboardController(IUserRepository _user, IChartRepository _chart)
        {
            user = _user;
            chart = _chart;
        }
        [Authorize]
        public ActionResult Index()
        {
            var us = user.GetUserByID(User.Identity.GetUserName());
            ViewBag.TenNguoiDung = us.FullName;
            ViewBag.Username = User.Identity.GetUserName();
            ViewBag.PhongBan = user.GetPhongBan(Convert.ToInt32(us.DepartmentID));
            ViewBag.SoMay = chart.GetSoMay().ToString();
            return View();
        }

        public JsonResult GetDeviceInfo() =>
            Json(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(chart.GetDeviceInfo(SessionVariable.TypeLanguage).ToList()).Replace("y\":\"", "y\":").Replace("\",\"color\":", ",\"color\":"), JsonRequestBehavior.AllowGet);
     
        public JsonResult GetDeviceStatus() =>
             Json(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(chart.GetDeviceStatus(DateTime.Now, User.Identity.GetUserName(), SessionVariable.TypeLanguage).ToList()).Replace("y\":\"", "y\":").Replace("\",\"color\":", ",\"color\":"), JsonRequestBehavior.AllowGet);
     
        public JsonResult GetSituationWO() => 
            Json(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(chart.GetSituationWO(SessionVariable.TypeLanguage).ToList()).Replace("\"[", "[").Replace("]\"", "]"), JsonRequestBehavior.AllowGet);
    
        public JsonResult GetSituationWOColumn() =>
            Json(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(chart.GetSituationWOColumn(SessionVariable.TypeLanguage).ToList()).Replace("\"[", "[").Replace("]\"", "]"), JsonRequestBehavior.AllowGet);

    }
}


