using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Data;
namespace Model.Repository
{
    public class PreventiveMaintenanceOfWorkRepository : IPreventiveMaintenanceOfWorkRepository, IDisposable
    {
        Model.Data.Model1 db;
        public PreventiveMaintenanceOfWorkRepository() => db = new Model.Data.Model1();

        public PreventiveMaintenanceOfWorkRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<PreventiveMaintenanceOfWork> ListAll() => db.PreventiveMaintenanceOfWork.ToList();

        public IEnumerable<PreventiveMaintenanceOfWork> GetPreventiveMaintenanceOfWork(string deviceID, int typeOfMaintenanceID) =>
            db.PreventiveMaintenanceOfWork.Where(x => x.TypeOfMaintenanceID == typeOfMaintenanceID && x.DeviceID == deviceID);
   
        public PreventiveMaintenanceOfWork GetPreventiveMaintenanceOfWorkByID(string deviceID, int typeOfMaintenanceID, int workID)  =>
            db.PreventiveMaintenanceOfWork.SingleOrDefault(x => x.TypeOfMaintenanceID == typeOfMaintenanceID && x.DeviceID == deviceID && x.WorkID == workID);

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
