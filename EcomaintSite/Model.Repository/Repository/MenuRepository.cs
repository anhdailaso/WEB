using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace Model.Repository
{
    public class MenuRepository : IMenuRepository, IDisposable
    {
        Model.Data.Model1 db;
        public MenuRepository() => db = new Model.Data.Model1();
        //public IEnumerable<WebMenu> ListAll() => db.WebMenu.Where(x => x.Hide == false).OrderBy(x => x.Index).ToList();
        public IEnumerable<WebMenu> ListAll(string UserName)
        {
           return db.Database.SqlQuery<WebMenu>("GetNhomMenu_Web @UserName", new object[]
            {
                new SqlParameter("@UserName", UserName)
            }).ToList();
        }
        public IEnumerable<WebMenu> GetChildMenuID(int ID,string UserName)
        {
            List<WebMenu> WebChild= ListAll(UserName).Where(x => x.Root == ID).ToList();
            return WebChild;
        }
        public IEnumerable<WebMenu> GetAllChildMenuID(string menuID, string UserName) =>
            db.Database.SqlQuery<WebMenu>("GetAllChildMenu @menuID", new object[]
            {
                new SqlParameter("@menuID", menuID)
            }).ToList();

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
