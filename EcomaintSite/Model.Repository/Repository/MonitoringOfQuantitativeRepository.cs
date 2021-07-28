using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class MonitoringOfQuantitativeRepository : IMonitoringOfQuantitativeRepository, IDisposable
    {
        Model.Data.Model1 db;
        public MonitoringOfQuantitativeRepository() => db = new Model.Data.Model1();

        public MonitoringOfQuantitativeRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<MonitoringOfQuantitative> ListAll() => db.MonitoringOfQuantitative.ToList();

        public void AddRange(IEnumerable<MonitoringOfQuantitative> lst) => db.MonitoringOfQuantitative.AddRange(lst);

        public void Add(MonitoringOfQuantitative obj) => db.MonitoringOfQuantitative.Add(obj);

        public bool CheckExist(MonitoringOfQuantitative obj) => db.MonitoringOfQuantitative.Any(x => x.MonitoringID == obj.MonitoringID && x.MonitoringParamsID == obj.MonitoringParamsID && x.ID == obj.ID && x.DeviceID == obj.DeviceID && x.ID == obj.ID);

        public void Edit(MonitoringOfQuantitative obj)
        {
            var entityProperties = obj.GetType().GetProperties();
            db.Entry(obj).State = EntityState.Modified;
            db.MonitoringOfQuantitative.Attach(obj);
            foreach (var ep in entityProperties)
            {
                if (ep.Name != "MonitoringID")
                    db.Entry(obj).Property(ep.Name).IsModified = true;
            }
        }
        public void Delete(int monitoringID)
        {
                var resulst = db.MonitoringOfQuantitative.Where(x => x.MonitoringID == monitoringID).ToList();
                db.MonitoringOfQuantitative.RemoveRange(resulst);
          
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
