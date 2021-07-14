using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IPeriodicMaintenanceRepository : IDisposable
    {
        IEnumerable<GetPeriodicMaintenanceObj> GetPeriodicMaintenance(DateTime startDate, DateTime endDate, string workSiteID, string formationID, string typeOfDevice, string groupDevice, string deviceID, string userName, bool timeOut);
        void SaveChanges();
    }
}
