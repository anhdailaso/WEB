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
    public class SparePartComponentRepository : ISparePartComponentRepository, IDisposable
    {
        Model.Data.Model1 db;
        public SparePartComponentRepository() => db = new Model.Data.Model1();

        public SparePartComponentRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<ComponentOfSparePart> ListAll() => db.ComponentOfSparePart.ToList();

        public bool CheckExists(string deviceID, string componentID, string sparePartID) => 
            db.ComponentOfSparePart.Any(x => x.DeviceID == deviceID && x.ComponentID == componentID && x.SparePartID == sparePartID);

        public IEnumerable<ComponentOfSparePart> GetSparePartComponentByID(string deviceID, string componentID, string sparePartID) => 
            db.ComponentOfSparePart.Where(x => x.ComponentID == componentID && x.DeviceID == deviceID && x.SparePartID  == sparePartID);

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

        public void Add(ComponentOfSparePart sparePart) => db.ComponentOfSparePart.Add(sparePart);        

        public void AddRange(IEnumerable<ComponentOfSparePart> lstSparePartComponent) => 
            db.ComponentOfSparePart.AddRange(lstSparePartComponent);        
    }
}
