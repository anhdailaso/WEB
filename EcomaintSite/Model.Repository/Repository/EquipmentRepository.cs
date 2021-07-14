using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class DeviceRepository : IDeviceRepository, IDisposable 
    {
        Model.Data.Model1 db;
        public DeviceRepository()
        {
            db = new Model1();
        }
        /// <summary>
        /// GetDeviceByRequest
        /// </summary>
        /// <param name="DeviceID">ValueField</param>
        /// <param name="Name">TextField</param>
        /// <returns></returns>
        public IEnumerable<DeviceObjForDropdown> GetDeviceByRequest(string username, string workSiteID, string requestDetailID) => db.Database.SqlQuery<DeviceObjForDropdown>("spGetDeviceByRequest @username, @workSiteID, @requestDetailID", new object[]
            {
                new SqlParameter("@username", username),
                new SqlParameter("@workSiteID", workSiteID),
                new SqlParameter("@requestDetailID", requestDetailID)
            }).ToList();

        public IEnumerable<Device> ListAll() => db.Devices.ToList();
     
        private bool disposed = false;
        protected  void Dispose(bool disposing)
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
