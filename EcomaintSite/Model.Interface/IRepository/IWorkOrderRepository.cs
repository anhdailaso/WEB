using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IWorkOrderRepository : IDisposable
    {
        IEnumerable<WorkOrder> ListAll();
        IEnumerable<GetWorkOrderObj> GetWorkOrder(string msnx,string msmay,string user, DateTime fromDate, DateTime toDate, int type, int lang);
        string GenerateWorkOrderID(string Username, int year, int month);
        string GetMaxWorkOrderID(string ms);
        string GenerateWorkOrderID_New(string Username, int year, int month);
        string GetWorkOrderByOrgUnit(string Username);
        void AutoAddNewStaffIntoWorkOrder(string workOrderID);
        WorkOrder GetWorkOrderByID(string workOrderID);
        void Add(WorkOrder workOrder);
        void AddRange(IEnumerable<WorkOrder> lst);
        void SaveChanges();
        void Edit(WorkOrder obj);
        int XoaPhieuBaoTri(string workOrderID);
    }
}
