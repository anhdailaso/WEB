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
    public class WorkOrderOfSparePartRepository : IWorkOrderOfSparePartRepository, IDisposable
    {
        Model.Data.Model1 db;
        public WorkOrderOfSparePartRepository() => db = new Model.Data.Model1();

        public WorkOrderOfSparePartRepository(Model.Data.Model1 context) => db = context;

        public void AddRange(IEnumerable<WorkOrderOfWorkIncludeSparePart> lst) => db.WorkOrderOfWorkIncludeSparePart.AddRange(lst);

        public IEnumerable<WorkOrderOfWorkIncludeSparePart> ListAll() => db.WorkOrderOfWorkIncludeSparePart.ToList();

        public WorkOrderOfWorkIncludeSparePart GetWorkOrderOfSparePartByID(string workOrderID, string componentID, int workID, string sparePartID) =>
            db.WorkOrderOfWorkIncludeSparePart.SingleOrDefault(x => x.WorkOrderID == workOrderID && x.ComponentID == componentID && x.WorkID == workID && x.SparePartID == sparePartID);

        public IEnumerable<WorkOrderOfWorkIncludeSparePart> GetWorkOrderOfSparePart(string workOrderID) =>
            db.WorkOrderOfWorkIncludeSparePart.Where(x => x.WorkOrderID == workOrderID);

        public IEnumerable<WorkOrderOfWorkIncludeSparePart> GetWorkOrderOfSparePartByWork(string workOrderID, string componentID, int workID) =>
            db.WorkOrderOfWorkIncludeSparePart.Where(x => x.WorkOrderID == workOrderID && x.ComponentID == componentID && x.WorkID == workID);

        public void SaveChanges() => db.SaveChanges();

        public void Add(WorkOrderOfWorkIncludeSparePart obj) => db.WorkOrderOfWorkIncludeSparePart.Add(obj);

        public void Edit(WorkOrderOfWorkIncludeSparePart obj)
        {
            var item = db.WorkOrderOfWorkIncludeSparePart.Where(x => x.WorkOrderID == obj.WorkOrderID).SingleOrDefault();
            item.WorkID = obj.WorkID;
            item.ComponentID = obj.ComponentID;
            item.SparePartID = obj.SparePartID;
            item.PlanQuantity = obj.PlanQuantity;
            item.ActualQuantity = obj.ActualQuantity;
        }

        public void Delete(string ID) => db.WorkOrderOfWorkIncludeSparePart.RemoveRange(db.WorkOrderOfWorkIncludeSparePart.Where(x => x.WorkOrderID == ID).ToList());

        public bool CheckExists(WorkOrderOfWorkIncludeSparePart obj) =>
            db.WorkOrderOfWorkIncludeSparePart.Any(x => x.WorkOrderID == obj.WorkOrderID && x.WorkID == obj.WorkID && x.ComponentID == obj.ComponentID && x.SparePartID == obj.SparePartID);

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
