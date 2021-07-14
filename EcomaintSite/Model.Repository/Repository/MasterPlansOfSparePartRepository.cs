using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class MasterPlansOfSparePartRepository : IMasterPlansOfSparePartRepository, IDisposable
    {
        Model.Data.Model1 db;
        public MasterPlansOfSparePartRepository() => db = new Model.Data.Model1();

        public MasterPlansOfSparePartRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<MasterPlansOfSparePart> ListAll() => db.MasterPlansOfSparePart.ToList();

        public void Add(MasterPlansOfSparePart obj) => db.MasterPlansOfSparePart.Add(obj);

        public bool CheckExists(MasterPlansOfSparePart obj) => 
            db.MasterPlansOfSparePart.Any(x => x.PlanID == obj.PlanID && x.WorkID == obj.WorkID && x.ComponentID == obj.ComponentID && x.DeviceID == obj.DeviceID && x.SparePartID == obj.SparePartID);        

        public void Edit(MasterPlansOfSparePart obj)
        {
            var item = db.MasterPlansOfSparePart.Where(x => x.PlanID == obj.PlanID && x.WorkID == obj.WorkID && x.ComponentID == obj.ComponentID && x.DeviceID == obj.DeviceID && x.SparePartID == obj.SparePartID).SingleOrDefault();
            item.WorkID = obj.WorkID;
            item.SparePartID = obj.SparePartID;
            item.DeviceID = obj.DeviceID;
            item.ComponentID = obj.ComponentID;
        }
        public IEnumerable<MasterPlansOfSparePart> GetSparePartByPlanID(int planID) => db.MasterPlansOfSparePart.Where(x => x.PlanID == planID);

        public IEnumerable<MasterPlansOfSparePart> GetSparePartByWorkPlan(int planID, int workID, string componentID)=> db.MasterPlansOfSparePart.Where(x => x.PlanID == planID && x.WorkID == workID && x.ComponentID == componentID);

        public void AddRange(IEnumerable<MasterPlansOfSparePart> lst) => db.MasterPlansOfSparePart.AddRange(lst);

        public void Delete(int ID) => db.MasterPlansOfSparePart.RemoveRange(db.MasterPlansOfSparePart.Where(x => x.PlanID == ID).ToList());   

        public void SaveChanges() => db.SaveChanges();

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
