using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IPreventiveMaintenanceRepository
    {
        IEnumerable<PreventiveMaintenance> ListAll();
        PreventiveMaintenance GetPreventiveMaintenanceByID(string deviceID, int typeOfMaintenanceID);
        void SaveChanges();
    }
}