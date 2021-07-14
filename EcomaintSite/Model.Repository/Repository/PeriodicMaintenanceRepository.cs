using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class PeriodicMaintenanceRepository : IPeriodicMaintenanceRepository, IDisposable
    {
        Model.Data.Model1 db;
        public PeriodicMaintenanceRepository() => db = new Model.Data.Model1();

        public PeriodicMaintenanceRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<GetPeriodicMaintenanceObj> GetPeriodicMaintenance(DateTime startDate, DateTime endDate, string workSiteID, string formationID, string typeOfDevice, string groupDevice, string deviceID, string userName, bool timeOut) =>
                db.Database.SqlQuery<GetPeriodicMaintenanceObj>("GetPeriodicMaintenance @startDate, @endDate, @workSiteID, @formationID, @typeOfDevice, @groupDevice, @deviceID, @userName, @timeOut ", new object[]
                {
                    new SqlParameter("@startDate", startDate),
                    new SqlParameter("@endDate", endDate),
                    new SqlParameter("@workSiteID", workSiteID),
                    new SqlParameter("@formationID", formationID),
                    new SqlParameter("@typeOfDevice", typeOfDevice),
                    new SqlParameter("@groupDevice", groupDevice),
                    new SqlParameter("@deviceID", deviceID),
                    new SqlParameter("@userName", userName),
                    new SqlParameter("@timeOut", timeOut)
                }).ToList();


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
