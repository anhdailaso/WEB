//using Dynamsoft.Barcode;
using Microsoft.AspNet.Identity;
using Model.Combobox;
using Model.Interface;
using Model.Interface.IRepository;
using Model.Repository.Repository;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using ZXing;
using System.Text;
using QRCodeDecoderLibrary;
using ZXing;
using System.Drawing;
using System.ComponentModel;
using Model.Repository;
//using IronBarCode;

namespace EcomaintSite.Controllers
{
    public class WorkWorkerController : Controller
    {
        IDeviceRepository deviceRepository;
        private ICombobox _Combobox;
        private IMyEcomainRepository _MyEcomainRepository;
        private ICombobox Combobox()
        {
            return _Combobox ?? (_Combobox = new Combobox());
        }
        private IMyEcomainRepository WorkWorker()
        {
            return _MyEcomainRepository ?? (_MyEcomainRepository = new MyEcomainRepository());
        }

        public WorkWorkerController(IDeviceRepository _deviceRepository)
        {
            deviceRepository = _deviceRepository;
        }

        // GET: MyEcomain
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.NhaXuong = Combobox().GetCbbDiaDiem(User.Identity.Name, SessionVariable.TypeLanguage, 1);
            ViewBag.cboDevice = new SelectList(deviceRepository.ListAll().Select(x =>
            new Model.Data.DeviceObjForDropdown
            {
                ID = x.ID,
                Name = x.ID + " - " + (SessionVariable.TypeLanguage == 0 ? x.Name : x.NameOfEng)
            }).ToList(), "ID", "Name");
            return View();
        }
    }
}
