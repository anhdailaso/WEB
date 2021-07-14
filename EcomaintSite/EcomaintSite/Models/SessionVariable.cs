using System;
using System.Web;

namespace EcomaintSite
{
    public static class SessionVariable
    {
        public static T ToEnum<T>(this int enumString) => (T)Enum.Parse(typeof(T), enumString.ToString());

        public static string DatabaseName
        {
            get
            {
                return HttpContext.Current.Session["DatabaseName"].ToString();
            }
            set
            {
                HttpContext.Current.Session["DatabaseName"] = value;
            }

            //get
            //{
            //    if (HttpContext.Current.Session["DatabaseName"] == null)
            //    {
            //        try
            //        {
            //            HttpContext.Current.Session["DatabaseName"] = HttpContext.Current.Request.Cookies["ckDatabaseName"].Value ?? "";
            //        }
            //        catch
            //        {
            //            HttpContext.Current.Session["DatabaseName"] = "";
            //        }
            //    }
            //    try
            //    {
            //        return HttpContext.Current.Session["DatabaseName"].ToString();
            //    }
            //    catch
            //    {
            //        return "";
            //    }
            //}
            //set
            //{
            //    HttpContext.Current.Session["TypeLanguage"] = value;
            //}
        }

        public static string Username
        {
            get
            {               
                return HttpContext.Current.Session["Username"].ToString();
            }
            set
            {
                HttpContext.Current.Session["Username"] = value;
            }
        }

        public static string Device
        {
            get
            {
                return HttpContext.Current.Session["Device"].ToString();
            }
            set
            {
                HttpContext.Current.Session["Device"] = value;
            }
        }

        public static int TypeLanguage
        {
            get
            {
                if (HttpContext.Current.Session["TypeLanguage"] == null)
                {
                    try
                    {
                        HttpContext.Current.Session["TypeLanguage"] = HttpContext.Current.Request.Cookies["ckTypeLanguage"].Value ?? "0";
                    }
                    catch
                    {
                        HttpContext.Current.Session["TypeLanguage"] = 0;
                    }
                }
                try
                {
                    return (int)HttpContext.Current.Session["TypeLanguage"];
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["TypeLanguage"] = value;
            }
        }


        public static string ThemeName
        {
            get
            {
                if (HttpContext.Current.Session["ThemeName"] == null)
                {
                    try
                    {
                        HttpContext.Current.Session["ThemeName"] = HttpContext.Current.Request.Cookies["ckThemeStyle"].Value ?? "default";
                    }
                    catch
                    {
                        HttpContext.Current.Session["ThemeName"] = "default";
                    }
                   
                }
                try
                {
                    return HttpContext.Current.Session["ThemeName"].ToString();
                }
                catch
                {
                    return "default";
                }             
            }
            set
            {
                HttpContext.Current.Session["ThemeName"] = value;
            }
        }
    }
}
