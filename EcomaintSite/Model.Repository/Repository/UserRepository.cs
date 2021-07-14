using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Model.Repository
{
    public class UserRepository : IUserRepository, IDisposable
    {
        Model.Data.Model1 db;
        public UserRepository() => db = new Model1();

        public User GetUserByID(string username) =>
            db.User.Where(x => x.Username == username).Count() > 0 ? db.User.SingleOrDefault(x => x.Username == username) : null;

        public User GetUserActiveByID(string username)
        {
            User resulst =  db.User.Where(x => x.Username == username && x.Active == true).Count() > 0 ? db.User.SingleOrDefault(x => x.Username == username) : null;
            return resulst;
        }
        public IEnumerable<User> ListAll() => db.User.ToList();
        public string GetPhongBan(int msTo)
        {
            try
            {
                return db.Department.Where(p => p.ID.Equals(msTo)).Select(p => p.Name).FirstOrDefault().ToString();

            }
            catch (Exception)
            {
                return "Admin";
            }
        }

        public int SoLuongLogin(string Usename)
        {
            try
            {
                return db.WebUserLogin.Where(x=>x.Username!=Usename).Count();

            }
            catch (Exception)
            {
                return 0;
            }
        }



        private bool disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
