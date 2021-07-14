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
    public class PreventiveMaintenanceRepository : IPreventiveMaintenanceRepository, IDisposable
    {
        Model.Data.Model1 db;
        public PreventiveMaintenanceRepository() => db = new Model.Data.Model1();

        public PreventiveMaintenanceRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<PreventiveMaintenance> ListAll() => db.PreventiveMaintenance.ToList();

        public PreventiveMaintenance GetPreventiveMaintenanceByID(string deviceID, int typeOfMaintenanceID)=> 
            db.PreventiveMaintenance.SingleOrDefault(x => x.TypeOfMaintenanceID == typeOfMaintenanceID && x.DeviceID == deviceID);

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