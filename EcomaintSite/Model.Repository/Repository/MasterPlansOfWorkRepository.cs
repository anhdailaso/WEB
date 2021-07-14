using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class MasterPlansOfWorkRepository : IMasterPlansOfWorkRepository, IDisposable
    {
        Model.Data.Model1 db;
        public MasterPlansOfWorkRepository() => db = new Model.Data.Model1();

        public MasterPlansOfWorkRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<MasterPlansOfWork> GetMasterPlansOfWorkByPlanID(int planID) => db.MasterPlansOfWork.Where(x => x.PlanID == planID);

        public IEnumerable<MasterPlansOfWork> ListAll() => db.MasterPlansOfWork.ToList();

        public void Add(MasterPlansOfWork obj) => db.MasterPlansOfWork.Add(obj);

        public bool CheckExists(MasterPlansOfWork obj) => db.MasterPlansOfWork.Any(x => x.PlanID == obj.PlanID && x.WorkID == obj.WorkID && x.ComponentID == obj.ComponentID && x.DeviceID == obj.DeviceID);

        public void Edit(MasterPlansOfWork obj)
        {
            var item = db.MasterPlansOfWork.Where(x => x.PlanID == obj.PlanID && x.WorkID == obj.WorkID && x.ComponentID == obj.ComponentID && x.DeviceID == obj.DeviceID).SingleOrDefault();
            item.WorkID = obj.WorkID;
            item.PriorityID = obj.PriorityID;
            item.IsIgnored = obj.IsIgnored;
            item.IsOutsource = obj.IsOutsource;
            item.ComponentID = obj.ComponentID;
        }

        public void AddRange(IEnumerable<MasterPlansOfWork> lst) => db.MasterPlansOfWork.AddRange(lst);

        public void Delete(int ID) => db.MasterPlansOfWork.RemoveRange(db.MasterPlansOfWork.Where(x => x.PlanID == ID).ToList());

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
