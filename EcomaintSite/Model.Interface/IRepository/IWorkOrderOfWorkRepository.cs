using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IWorkOrderOfWorkRepository : IDisposable
    {
        IEnumerable<WorkOrderOfWork> ListAll();
        void AddRange(IEnumerable<WorkOrderOfWork> lst);
        WorkOrderOfWork GetWorkOrderOfWorkByID(string workOrderID, string componentID, int workID);
        IEnumerable<WorkOrderOfWork> GetWorkOrderOfWork(string workOrderID);
        void Add(WorkOrderOfWork obj);
        void SaveChanges();
        void Edit(WorkOrderOfWork obj);
        bool CheckExists(WorkOrderOfWork obj);
        void Delete(string ID);
    }
}
