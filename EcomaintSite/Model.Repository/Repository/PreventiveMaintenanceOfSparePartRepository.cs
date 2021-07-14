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
    public class PreventiveMaintenanceOfSparePartRepository : IPreventiveMaintenanceOfSparePartRepository, IDisposable
    {
        Model.Data.Model1 db;
        public PreventiveMaintenanceOfSparePartRepository() => db = new Model.Data.Model1();

        public PreventiveMaintenanceOfSparePartRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<PreventiveMaintenanceOfSparePart> ListAll() => db.PreventiveMaintenanceOfSparePart.ToList();

        public IEnumerable<PreventiveMaintenanceOfSparePart> GetPreventiveMaintenanceOfSparePart(string deviceID, int typeOfMaintenanceID, int workID) =>
            db.PreventiveMaintenanceOfSparePart.Where(x => x.TypeOfMaintenanceID == typeOfMaintenanceID && x.DeviceID == deviceID && x.WorkID == workID);

        public PreventiveMaintenanceOfSparePart GetPreventiveMaintenanceOfSparePartByID(string deviceID, int typeOfMaintenanceID, int workID, string sparePartID) => 
            db.PreventiveMaintenanceOfSparePart.SingleOrDefault(x => x.TypeOfMaintenanceID == typeOfMaintenanceID && x.DeviceID == deviceID && x.WorkID == workID && x.SparePartID == sparePartID);
        

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
