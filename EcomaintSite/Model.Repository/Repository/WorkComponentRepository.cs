using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class WorkComponentRepository : IWorkComponentRepository, IDisposable
    {
        Model.Data.Model1 db;
        public WorkComponentRepository() => db = new Model.Data.Model1();

        public WorkComponentRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<ComponentOfWork> ListAll() => db.ComponentOfWork.ToList();

        public IEnumerable<GetWorkByComponentObj> GetWorkByComponent(string deviceID, string componentID, int lang) =>
            db.Database.SqlQuery<GetWorkByComponentObj>("GetWorkByComponent @deviceID, @componentID, @lang", new object[]
            {
                new SqlParameter("@deviceID", deviceID),
                new SqlParameter("@componentID", componentID),
                new SqlParameter("@lang", lang)
            }).ToList();

        public ComponentOfWork GetWorkComponentByID(string deviceID, string componentID, int workID) =>
            db.ComponentOfWork.SingleOrDefault(x => x.ID == componentID && x.DeviceID == deviceID && x.WorkID  == workID);

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
