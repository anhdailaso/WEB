using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface ITypeOfMaintenanceRepository : IDisposable
    {
        IEnumerable<TypeOfMaintenance> ListAll();
        TypeOfMaintenance GetTypeOfMaintenance(int ID);
        IEnumerable<TypeOfMaintenance> GetTypeOfMaintenanceByDevice(string deviceID);       
    }
}
