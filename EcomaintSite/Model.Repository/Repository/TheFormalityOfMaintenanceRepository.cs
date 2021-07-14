using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class TheFormalityOfMaintenanceRepository : ITheFormalityOfMaintenanceRepository, IDisposable
    {
        Model.Data.Model1 db;
        public TheFormalityOfMaintenanceRepository() => db = new Model.Data.Model1();

        public TheFormalityOfMaintenanceRepository(Model1 context) => db = context;

        public bool IsPeriodicMaintenance(int maintenanceFormID) => db.MaintenanceForm.SingleOrDefault(x => x.ID == maintenanceFormID).Periodic;

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
