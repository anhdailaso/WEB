using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class MasterPlanRepository : IMasterPlanRepository, IDisposable
    {
        Model.Data.Model1 db;
        public MasterPlanRepository() => db = new Model.Data.Model1();        

        public MasterPlanRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<MasterPlan> ListAll() => db.MasterPlan.ToList();

        public void Add(MasterPlan obj) => db.MasterPlan.Add(obj);

        public void Edit(MasterPlan obj)
        {
            var item = db.MasterPlan.Where(x => x.ID == obj.ID).SingleOrDefault();
            item.DateCreated = obj.DateCreated;
            item.PriorityID = obj.PriorityID;
            item.DateCompleted = obj.DateCompleted;
            item.DueDate = obj.DueDate;
            item.ReasonForRepairID = obj.ReasonForRepairID;
            item.Name = obj.Name;
        }

        public IEnumerable<GetMasterPlansObj> GetMasterPlans(string user, DateTime fromDate, DateTime toDate, int option, int lang) =>
            db.Database.SqlQuery<GetMasterPlansObj>("GetMasterPlans @username, @fromDate, @toDate, @option, @lang ", new object[] 
            {
                new SqlParameter("@username", user),
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate),
                new SqlParameter("@option", option),
                new SqlParameter("@lang", lang)

            }).ToList();

        //public void Delete(int Index) => db.MasterPlan.Remove(db.MasterPlan.SingleOrDefault(x => x.Index == Index));
        

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

        public void Delete(int Index)
        {
            //throw new NotImplementedException();
        }
    }
}
