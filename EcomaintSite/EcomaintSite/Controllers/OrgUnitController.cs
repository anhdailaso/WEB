using Model.Data;
using Model.Interface;
using Model.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomaintSite.Controllers
{
    public class OrgUnitController : Controller
    {
        private IOrgUnitRepository db;
        public OrgUnitController(IOrgUnitRepository _db) => db = _db;
        
        // GET: OrgUnit
        [Authorize]
        public ActionResult Index() => View(db.ListAll());

        public JsonResult Save(string data, string action)
        {
            List<OrgUnit> lstUnit = JsonConvert.DeserializeObject<List<OrgUnit>>(data);
            try
            {
                if (action == "add")
                {
                    if (db.CheckIDExist(lstUnit[0].ID)) return Json("Trùng mã");
                }
                if (db.CheckNameExist(lstUnit[0].ID, lstUnit[0].Name)) return Json("Trùng tên đơn vị");
                if (db.CheckShortNameExist(lstUnit[0].ID, lstUnit[0].ShortName)) return Json("Trùng tên ngắn");
                if (action == "add")
                {
                    db.Add(lstUnit[0]);
                }
                else
                {
                    db.Edit(lstUnit[0]);
                }
            }
            catch (Exception ex)
            {
                return Json("failure: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
            return Json("success", JsonRequestBehavior.AllowGet);

        }
        public JsonResult Remove(string id)
        {
            try
            {
                if (db.CheckDepartmentExist(id)) return Json("Đang sử dụng trong dữ liệu Công nhân");
                if (db.CheckCostCenterExist(id)) return Json("Đang sử dụng trong BPCP");
                db.Delete(id);
            }
            catch (Exception ex)
            {
                return Json("failure: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }

    }
}
