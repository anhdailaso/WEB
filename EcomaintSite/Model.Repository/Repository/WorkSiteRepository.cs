using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class WorkSiteRepository : IWorkSiteRepository, IDisposable
    {
        Model.Data.Model1 db;
        public WorkSiteRepository() => db = new Model1();

        public IEnumerable<WorkSite> GetWorkSiteByID(string username, string lang) => 
            db.Database.SqlQuery<WorkSite>("GetWorkSiteByID @username, @lang", new object[]
            {
                new SqlParameter("@username", username),
                new SqlParameter("@lang", lang)
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
