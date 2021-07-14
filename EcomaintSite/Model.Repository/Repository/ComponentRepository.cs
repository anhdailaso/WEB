using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Model.Data;
namespace Model.Repository
{
    public class ComponentRepository : IComponentRepository, IDisposable
    {
        Model.Data.Model1 db;

        public ComponentRepository() => db = new Model.Data.Model1();
        
        public ComponentRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<Component> ListAll() => db.Component.ToList();
        
        public Component GetComponentByID(string deviceID, string componentID) => db.Component.SingleOrDefault(x =>  x.DeviceID == deviceID && x.ID  == componentID);

        public IEnumerable<Component> GetComponentByDevice(string deviceID, int lang) => 
            db.Database.SqlQuery<Component>("spGetComponentByDevice @deviceID, @lang", new object[]
            {
                new SqlParameter("@deviceID", deviceID),
                new SqlParameter("@lang", lang)
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
