using Microsoft.AspNet.Identity;
using Model.Combobox;
using Model.Data;
using Model.Interface;
using Model.Interface.IRepository;
using Model.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace EcomaintSite.Controllers
{
    public class SafetyController : Controller
    {

        private ICombobox _Combobox;
        private ISafetyRepository _SafetyRepository;
        private ICombobox Combobox()
        {
            return _Combobox ?? (_Combobox = new Combobox());
        }

        private ISafetyRepository Safety()
            {
            return _SafetyRepository ?? (_SafetyRepository = new SafetyRepository());
        }
        // GET: Safety
        [Authorize]
        public ActionResult ShowHazardReport()
        {
            ViewBag.STT = "-1";
            ViewBag.ListReportParent = Combobox().GetListReportParent(User.Identity.GetUserName());
            ViewBag.ListLocation = Combobox().GetListLocation();
            ViewBag.ListDepartment = Combobox().GetListDepartment();
            ViewBag.ListPersonRef = Combobox().GetListPersonRef(User.Identity.GetUserName());
            HazardReportViewModel model = new HazardReportViewModel();
            model.DocNum = Safety().GetSoPhieuHazard();
            return View("~/Views/Safety/ShowHazardReport.cshtml", model);
        }



        public ActionResult EditHazardReport(string ID)
        {

            ViewBag.ListReportParent = Combobox().GetListReportParent(User.Identity.GetUserName());
            ViewBag.ListLocation = Combobox().GetListLocation();
            ViewBag.ListDepartment = Combobox().GetListDepartment();
            ViewBag.ListPersonRef = Combobox().GetListPersonRef(User.Identity.GetUserName());
            HazardReportViewModel model = Safety().GetListByID(ID);
            model.DocNum = Safety().GetSoPhieuHazard();
            if (model.UnsafeCondition == true) model.CauSafery = 1;
            if (model.UnsafeBehevior == true) model.CauSafery = 2;
            if (model.SatetySuggestion == true) model.CauSafery = 3;
            if (model.NearMiss == true) model.CauSafery = 4;
            if (model.Environment == true) model.CauSafery = 5;
            if (model.TT == true) model.CauSafery = 6;
            if (model.Other == true) model.CauSafery = 7;

            //CauSafery
            //1 UnsafeCondition
            //2 UnsafeBehevior
            //3 SatetySuggestion
            //4 NearMiss
            //5 Environment
            //6 TT
            //7 Other
            if (model.Operation == true) model.Relateddepartments = 2;
            if (model.TechHSE == true) model.Relateddepartments = 3;
            if (model.HR == true) model.Relateddepartments = 5;
            if (model.Acount == true) model.Relateddepartments = 6;
            //Relateddepartments
            //2  Operation
            //3 TechHSE
            //5  HR
            //6 Account

            if (model.BelongContractor == true) model.Belongto = 1;
            if (model.BelongToBSPort == true) model.Belongto = 2;

            //Belongto
            //1 BelongContractor
            //2 BelongtoBSPort
            if (model.HPES1 == true) model.HPES = 1;
            if (model.HPES2 == true) model.HPES = 2;
            if (model.HPES3 == true) model.HPES = 3;
            if (model.HPES4 == true) model.HPES = 4;
            if (model.HPES5 == true) model.HPES = 5;
            if (model.HPES6 == true) model.HPES = 6;


            return View("~/Views/Safety/ShowHazardReport.cshtml", model);
        }

        // GET: Safety
        [Authorize]
        public ActionResult ShowStopCard()
        {
            var us = User.Identity.GetUserName();
            return View("~/Views/Safety/StopCard.cshtml");

        }
            
        [Authorize]
        [HttpPost]
        public JsonResult GetListHazardReport(string tungay,string denngay)
        {
            try
            {
                DateTime a = Convert.ToDateTime(tungay, new CultureInfo("vi-vn"));
                DateTime b = Convert.ToDateTime(denngay, new CultureInfo("vi-vn"));
                IEnumerable<ChooseListHazard> list = Safety().ChooseListHazard(a, b);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}