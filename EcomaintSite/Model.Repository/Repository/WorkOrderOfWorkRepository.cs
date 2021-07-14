using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class WorkOrderOfWorkRepository : IWorkOrderOfWorkRepository, IDisposable
    {
        Model.Data.Model1 db;
        public WorkOrderOfWorkRepository() =>  db = new Model.Data.Model1();
        
        public WorkOrderOfWorkRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<WorkOrderOfWork> ListAll() => db.WorkOrderOfWork.ToList();

        public void AddRange(IEnumerable<WorkOrderOfWork> lst) => db.WorkOrderOfWork.AddRange(lst);
       
        public void Add(WorkOrderOfWork obj) => db.WorkOrderOfWork.Add(obj);

        public void Edit(WorkOrderOfWork obj)
        {
            var item = db.WorkOrderOfWork.Where(x => x.WorkOrderID == obj.WorkOrderID).SingleOrDefault();
            item.WorkID = obj.WorkID;
            item.ComponentID = obj.ComponentID;
            item.Action = obj.Action;
            item.HourPlan = obj.HourPlan;
            item.ActualTime = obj.ActualTime;
            item.AmountOfPeople = obj.AmountOfPeople;
            item.Actual = obj.Actual;
            item.QualityCheck = obj.QualityCheck;
            item.Tool = obj.Tool;
            item.InquiryHR = obj.InquiryHR;
            item.Path = obj.Path;
            item.PlanID = obj.PlanID;
            item.DateCompleted = obj.DateCompleted;
        }

        public void Delete(string ID) => db.WorkOrderOfWork.RemoveRange(db.WorkOrderOfWork.Where(x => x.WorkOrderID == ID).ToList());

        public void SaveChanges() => db.SaveChanges();

        public WorkOrderOfWork GetWorkOrderOfWorkByID(string workOrderID, string componentID, int workID) => db.WorkOrderOfWork.SingleOrDefault(x => x.WorkOrderID == workOrderID && x.ComponentID == componentID && x.WorkID == workID);

        public IEnumerable<WorkOrderOfWork> GetWorkOrderOfWork(string workOrderID) => db.WorkOrderOfWork.Where(x => x.WorkOrderID == workOrderID);

        public bool CheckExists(WorkOrderOfWork obj) => db.WorkOrderOfWork.Any(x => x.WorkOrderID == obj.WorkOrderID && x.WorkID == obj.WorkID && x.ComponentID == obj.ComponentID);

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
