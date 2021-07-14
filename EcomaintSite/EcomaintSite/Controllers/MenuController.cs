using Microsoft.AspNet.Identity;
using Model.Interface;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomaintSite.Controllers
{   
    public class MenuController : Controller
    {     
        IMenuRepository menu;
        
        public MenuController(IMenuRepository _menu) => menu = _menu;

        public ActionResult Index() => View();

        public ActionResult GetMenu(string name)
        {
            return PartialView("~/Views/_PartialMenu.cshtml", menu.ListAll(User.Identity.GetUserName()));
        }
        public JsonResult GetChildMenu(int ID) => Json(menu.GetChildMenuID(ID, User.Identity.GetUserName()), JsonRequestBehavior.AllowGet);

        public JsonResult GetAllChildMenu(string menuID) => Json(menu.GetAllChildMenuID(menuID, User.Identity.GetUserName()), JsonRequestBehavior.AllowGet);
        
    }
}

