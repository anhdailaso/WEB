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
    public class MonitoringOfQualitativeRepository : IMonitoringOfQualitativeRepository, IDisposable
    {
        Model.Data.Model1 db;
        public MonitoringOfQualitativeRepository() => db = new Model.Data.Model1();

        public MonitoringOfQualitativeRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<MonitoringOfQualitative> ListAll() => db.MonitoringOfQualitative.ToList();

        public void Add(MonitoringOfQualitative obj) => db.MonitoringOfQualitative.Add(obj);

        public void AddRange(IEnumerable<MonitoringOfQualitative> lst) => db.MonitoringOfQualitative.AddRange(lst);        

        public void Edit(MonitoringOfQualitative obj)
        {
            var entityProperties = obj.GetType().GetProperties();
            db.Entry(obj).State = EntityState.Modified;
            db.MonitoringOfQualitative.Attach(obj);
            foreach (var ep in entityProperties)
            {
                if (ep.Name != "MonitoringID")
                    db.Entry(obj).Property(ep.Name).IsModified = true;
            }

        }

        public void Delete(int ID) => db.MonitoringOfQualitative.RemoveRange(db.MonitoringOfQualitative.Where(x => x.MonitoringID == ID));        

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
