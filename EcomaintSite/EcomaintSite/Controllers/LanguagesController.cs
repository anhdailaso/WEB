using Model.Interface;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace EcomaintSite.Controllers
{
    public class LanguagesController : Controller
    {
        ILanguagesRepository lang;
        public LanguagesController(ILanguagesRepository _lang) => lang = _lang;
        
        public class LanguagesObj
        {
            public string Keyword { get; set; }
            public string Text { get; set; }
        }

        public JsonResult GetLanguages(string data, string form, int type)
        {
            SessionVariable.TypeLanguage = type;
            List<LanguagesObj> lstLanguage = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LanguagesObj>>(data);
           
            var lst = lang.GetLanguages(form).ToList();
            lstLanguage.ForEach(x => x.Text = lst.Where(y => y.Keyword.ToLower() == x.Keyword.ToLower()).Count() > 0 ? type == 0 ? lst.Where(y => y.Keyword.ToLower() == x.Keyword.ToLower()).Take(1).SingleOrDefault().VietNam : lst.Where(y => y.Keyword.ToLower() == x.Keyword.ToLower()).Take(1).SingleOrDefault().English : ("@" + x.Keyword + "@"));
            var lstMsg = lst.Where(x => x.Keyword.Contains("msg")).Select(x => new LanguagesObj
            {
                Keyword = x.Keyword,
                Text = type == 0 ? x.VietNam : x.English
            }).ToList<LanguagesObj>();
            lstLanguage.AddRange(lstMsg);
            int count = 0;
            foreach (LanguagesObj item in lstLanguage)
            {
                if (item.Text.Contains("@") && !item.Text.Contains("mnu") && !lang.CheckExists(item.Text, type))
                {
                    lang.Add(new Model.Data.Languages() { ModuleName = "ECOMAIN", VietNam = "@" + item.Keyword + "@", English = "@" + item.Keyword + "@", VietNamOld = "@" + item.Keyword + "@", EnglishOld = "@" + item.Keyword + "@", Form = form, Keyword = item.Keyword, Type = false });
                    count++;
                }
            }

            if (count > 0) lang.SaveChanges();
            return Json(lstLanguage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeThemeValue(string themeStyle, int typeLang)
        {
            SessionVariable.ThemeName = themeStyle;
            SessionVariable.TypeLanguage = typeLang;
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}