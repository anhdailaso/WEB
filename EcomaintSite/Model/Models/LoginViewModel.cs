using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Data
{
    public class LoginViewModel
    {
        public string DatabaseName { get; set; }
        public string Username {get;set;}
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public List<DataBase> Databases { get; set; }
    }

    public class DataBase
    {
        public string DatabaseName { get; set; }
    }
}
