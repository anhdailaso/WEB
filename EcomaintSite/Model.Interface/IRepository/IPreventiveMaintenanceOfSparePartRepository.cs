using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IPreventiveMaintenanceOfSparePartRepository 
    {
        IEnumerable<PreventiveMaintenanceOfSparePart> ListAll();
        IEnumerable<PreventiveMaintenanceOfSparePart> GetPreventiveMaintenanceOfSparePart(string deviceID, int typeOfMaintenanceID, int workID);
        PreventiveMaintenanceOfSparePart GetPreventiveMaintenanceOfSparePartByID(string deviceID, int typeOfMaintenanceID, int workID, string sparePartID);
        void SaveChanges();
    }
}
