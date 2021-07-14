using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
   public class WebUserLoginRepository : IWebUserLoginRepository, IDisposable
    {
        Model.Data.Model1 db;
        public WebUserLoginRepository() => db = new Model1();

        public int Count() => db.WebUserLogin.Count();

        public bool CheckExists(string Username) => db.WebUserLogin.Any(x => x.Username == Username);

        public void Add(string Username) => db.WebUserLogin.Add(new WebUserLogin { TimeLogin = DateTime.Now, Username = Username });

        public void Delete(string Username)
        {
            if (db.WebUserLogin.Where(x => x.Username == Username).Count() > 0)
            {
                db.WebUserLogin.Remove(db.WebUserLogin.SingleOrDefault(x => x.Username == Username));
                db.SaveChanges();
            }
        }
        public void SaveChanges() => db.SaveChanges();

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
