using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IMasterPlanRepository
    {
        IEnumerable<MasterPlan> ListAll();
        void Add(MasterPlan obj);
        void Edit(MasterPlan obj);
        void Delete(int Index);
        void SaveChanges();
        IEnumerable<GetMasterPlansObj> GetMasterPlans(string user, DateTime fromDate, DateTime toDate, int option, int lang);
    }
}
