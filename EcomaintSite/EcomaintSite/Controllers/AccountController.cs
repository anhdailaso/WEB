using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Model.Data;
using System.Web;
using System.Web.Mvc;
using Model.Data.ViewModel;
using System;
using Model.Interface;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EcomaintSite.Controllers
{
    public class AccountController : Controller
    {
        IWebUserLoginRepository webRepository;
        IUserRepository userRepository;
        IDatabaseRepository dataRepository;
        public AccountController(IUserRepository _user, IDatabaseRepository _data, IWebUserLoginRepository _web)
        {
            userRepository = _user;
            webRepository = _web;
            dataRepository = _data;
        }
        public bool CheckService(string Username)
        {
            string sConnect = new Model1().Database.Connection.ConnectionString;
            string sIP = sConnect.Split(';')[0].Substring(7).Split('\\')[0];
            try
            {
                var client = new UdpClient();
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000);
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(sIP), 65000);
                // endpoint where server is listening
                client.Connect(ep);
                // send data
                byte[] buffer = Encoding.ASCII.GetBytes("LICW");
                client.Send(buffer, buffer.Length);
                // then receive data
                var rev = client.Receive(ref ep);
                string sStr = Encoding.ASCII.GetString(rev);
                string[] sArr = null;
                sArr = sStr.Split('!');
                try
                {
                    //lấy số lượng của user hiện tại
                    Commons.lic = userRepository.SoLuongLogin(Username) + 1;
                    //Commons.licCom = int.Parse(sArr[3]);
                    Commons.licCom = 10;
                }
                catch (Exception ex)
                {
                }
                client.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Login(string ReturnURL, string error = "")
        {
            if (User.Identity.IsAuthenticated == true && error != "")
            {
                RedirectToAction("Index", "Dashboard");
            }
            var model = new LoginViewModel();
            ViewBagInfo(ref model, error, ReturnURL);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string ReturnURL, string error = "")
        {
            CheckService(model.Username);
            string s = Request.Form["Password"];
            // HttpCookie db = Response.Cookies["DatabaseName"];
            HttpCookie us = Response.Cookies["Username"];
            HttpCookie pass = Response.Cookies["Password"];
            try
            {
                // db.Expires = DateTime.Now.AddDays(-1D);
                us.Expires = DateTime.Now.AddDays(-1D);
                pass.Expires = DateTime.Now.AddDays(-1D);
            }
            catch
            {

            }
            if (model.RememberMe == true)
            {
                us = new HttpCookie("Username");
                us.Value = model.Username;
                us.Expires = DateTime.Now.AddDays(15);
                Response.Cookies.Add(us);
                pass = new HttpCookie("Password");
                pass.Value = model.Password;
                pass.Expires = DateTime.Now.AddDays(15);
                Response.Cookies.Add(pass);
            }
            else
            {
                //db.Expires = DateTime.Now.AddDays(-1D);
                us.Expires = DateTime.Now.AddDays(-1D);
                pass.Expires = DateTime.Now.AddDays(-1D);
            }
            //if (Commons.licCom == 0)
            //{
            //    return RedirectToAction("Login", "Account", new { ReturnURL = ReturnURL, error = "Không thể kết nói tới server vui lòng liên hệ lại admin!" });
            //}
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", "Account", new { ReturnURL = ReturnURL, error = "" });
            }

            if (model == null || model.Username == null)
            {
                return RedirectToAction("Login", "Account", new { ReturnURL = ReturnURL, error = "Vui lòng nhập thông tin đăng nhập" });
            }
            //kiểm tra user đăng nhập đã tồn tại hay chưa
            System.Collections.Generic.List<string> d = (System.Collections.Generic.List<string>)HttpContext.Application["UsersLoggedIn"];
            //if (d != null)
            //{
            //    lock (d)
            //    {
            //        if (d.Contains(model.Username.ToLower()))
            //        {
            //            return RedirectToAction("Login", "Account", new { ReturnURL = ReturnURL, error = "User đang đăng nhập" });
            //        }
            //        if (d.Count > Commons.licCom)
            //        {
            //            return RedirectToAction("Login", "Account", new { ReturnURL = ReturnURL, error = "Vượt quá số lượng người dùng "+ d.Count + "/" + Commons.licCom +" " });
            //        }
            //    }
            //}
            return CheckRegistryAndSync(model, ReturnURL);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        protected ActionResult CheckRegistryAndSync(LoginViewModel model, string ReturnURL)
        {
            try
            {
                SessionVariable.DatabaseName = model.DatabaseName;
                var userDB = userRepository.GetUserActiveByID(model.Username);
                ApplicationUser user = null;
                if (userDB == null)
                {
                    return RedirectToAction("Login", "Account", new { ReturnURL = ReturnURL, error = "Tài khoản chưa đăng ký hoặc đang deactive. (liên hệ Admin)" });
                }
                else
                {
                    var userStore = new UserStore<ApplicationUser>(new Model1());
                    var manager = new UserManager<ApplicationUser>(userStore);
                    user = manager.FindByName(model.Username);
                    if (user == null) //đăng ký user tồn tại trong database CMMS nhưng chưa có trong identity
                    {
                        manager.PasswordValidator = new PasswordValidator
                        {
                            RequiredLength = 3,
                            RequireNonLetterOrDigit = false,
                            RequireDigit = false,
                            RequireLowercase = false,
                            RequireUppercase = false,
                        };
                        var userIdentity = new ApplicationUser() { UserName = model.Username };
                        IdentityResult result = manager.Create(userIdentity, GiaiMaDL(userDB.Password));
                        if (!result.Succeeded)
                        {
                            return RedirectToAction("Login", "Account", new { ReturnURL = ReturnURL, error = "Tài khoản chưa đăng ký hoặc đang deactive. (liên hệ Admin)" });
                        }
                    }
                    //else
                    //{
                    user = manager.Find(model.Username, model.Password);
                    if (user == null)
                    {
                        return RedirectToAction("Login", "Account", new { ReturnURL = ReturnURL, error = "Tài khoản hoặc mật khẩu không đúng!" });
                    }
                    System.Collections.Generic.List<string> d = (System.Collections.Generic.List<string>)HttpContext.Application["UsersLoggedIn"];
                    if (d != null)
                    {
                        lock (d)
                        {
                            if (User.Identity.IsAuthenticated == false)
                            {
                                d.Add(model.Username.ToLower());
                            }
                        }
                        // gan sesion user name
                        //tao doi tung cookie
                        Session["UserLoggedIn"] = model.Username;
                        //}
                    }
                    if (User.Identity.IsAuthenticated == false)
                    {
                        var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                        var userSign = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                        authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = model.RememberMe }, userSign);
                        SessionVariable.Username = model.Username;
                        SessionVariable.TypeLanguage = 0;
                        if (!webRepository.CheckExists(User.Identity.Name))
                        {
                            webRepository.Add(user.UserName);
                            try
                            {
                                webRepository.SaveChanges();

                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    return Redirect(ReturnURL ?? "dashboard");
                }
            }
            catch (Exception ex)
            {
                //"Vui lòng nhập đầy đủ thông tin đăng nhập."
                return RedirectToAction("Login", "Account", new { ReturnURL = ReturnURL, error = ex.Message });
            }
        }

        public string GiaiMaDL(string str)
        {

            string sTam = "";
            const int _CODE_ = 354;
            for (int i = 0; i < str.Length; i++)
            {
                sTam += System.Convert.ToChar(((int)System.Convert.ToChar(str.Substring(i, 1)) / 2) - _CODE_).ToString();
            }
            return sTam;
        }

        private void ViewBagInfo(ref LoginViewModel model, string message, string url)
        {
            model = new LoginViewModel
            {
                Databases = new System.Collections.Generic.List<DataBase>()
            };
            model.Databases.AddRange(dataRepository.GetDatabase());
            if (HttpContext.Request.Cookies["DatabaseName"] != null)
            {
                try
                {
                    model.DatabaseName = Request.Cookies["DatabaseName"].Value.ToString();

                    model.Username = Request.Cookies["Username"].Value.ToString();
                    model.Password = Request.Cookies["Password"].Value.ToString();
                    model.RememberMe = true;
                }
                catch (Exception ex)
                {
                    model.DatabaseName = null;
                }
            }
            SelectList cateList = new SelectList(model.Databases, "DatabaseName", "DatabaseName", "SelectedValue");
            ViewBag.listdatabase = cateList;
            TempData["ReturnURL"] = url;
            ViewBag.Error = message;
        }
        [AllowAnonymous]
        public ActionResult LogOut(string ReturnURL)
        {
            var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            webRepository.Delete(User.Identity.Name);
            Session.Abandon();
            //them vao de xoa cac nguoi dung co ten hien tai
            System.Web.HttpContext.Current.Application.Remove(System.Web.HttpContext.Current.User.Identity.Name);
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public JsonResult GetPassWord(string id)
        {
            var userDB = userRepository.GetUserByID(id);
            string ma;
            try
            {
                ma = GiaiMaDL(userDB.Password);
            }
            catch (Exception)
            {
                ma = "!s!a!i!s!";
            }
            return Json(new { pass = ma }, JsonRequestBehavior.AllowGet);
        }
    }
}
