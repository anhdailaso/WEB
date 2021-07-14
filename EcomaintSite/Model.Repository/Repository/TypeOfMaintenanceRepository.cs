using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class TypeOfMaintenanceRepository : ITypeOfMaintenanceRepository, IDisposable
    {
        Model.Data.Model1 db;
        public TypeOfMaintenanceRepository() => db = new Model.Data.Model1();

        public TypeOfMaintenanceRepository(Model1 context) => db = context;

        public IEnumerable<TypeOfMaintenance> ListAll() => db.TypeOfMaintenance.ToList();

        public TypeOfMaintenance GetTypeOfMaintenance(int ID) => db.TypeOfMaintenance.SingleOrDefault(x => x.ID == ID);

        public IEnumerable<TypeOfMaintenance> GetTypeOfMaintenanceByDevice(string deviceID) =>
            db.Database.SqlQuery<TypeOfMaintenance>("GetTypeOfMaintenanceByDevice @deviceID ", new object[]
            {
                new SqlParameter("@deviceID", deviceID)
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
