using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IWorkOrderDetailByTheStateRepository : IDisposable
    {    
        IEnumerable<WorkOrderDetailsByTheState> ListAll();
        void AddRange(IEnumerable<WorkOrderDetailsByTheState> lst);
        void Add(WorkOrderDetailsByTheState obj);
        void SaveChanges();      
    }
}
