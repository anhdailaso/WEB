using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IPreventiveMaintenanceOfWorkRepository
    {
        IEnumerable<PreventiveMaintenanceOfWork> ListAll();
        IEnumerable<PreventiveMaintenanceOfWork> GetPreventiveMaintenanceOfWork(string deviceID, int typeOfMaintenanceID);
        PreventiveMaintenanceOfWork GetPreventiveMaintenanceOfWorkByID(string deviceID, int typeOfMaintenanceID, int workID);
        void SaveChanges();
    }
}
