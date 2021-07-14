using Microsoft.AspNet.Identity;
using Model.Combobox;
using Model.Interface;
using Model.Interface.IRepository;
using Model.Repository.Repository;
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
            var us = User.Identity.GetUserName();
            Loadcombo(us);
            return View("~/Views/Safety/HazardReport.cshtml");
        }

        // GET: Safety
        [Authorize]
        public ActionResult ShowStopCard()
        {
            var us = User.Identity.GetUserName();
            Loadcombo(us);
            return View("~/Views/Safety/StopCard.cshtml");

        }

        private void Loadcombo(string user)
        {
            ViewBag.NhaXuong = Combobox().GetCbbDiaDiem(user, SessionVariable.TypeLanguage, 1);

        }
    }
}