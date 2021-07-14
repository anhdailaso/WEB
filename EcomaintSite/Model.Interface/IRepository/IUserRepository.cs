using Model.Data;
using System;
using System.Collections.Generic;
namespace Model.Interface
{
    public interface IUserRepository : IDisposable 
    {
        User GetUserByID(string username);
        User GetUserActiveByID(string username);

        string GetPhongBan(int msTo);
        int SoLuongLogin(string Username);
        IEnumerable<User> ListAll();
        //string GetPhongBan(string username);
    }
   
}
