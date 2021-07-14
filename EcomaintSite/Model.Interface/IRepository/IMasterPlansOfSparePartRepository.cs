using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IMasterPlansOfSparePartRepository
    {
        void Add(MasterPlansOfSparePart obj);
        void AddRange(IEnumerable<MasterPlansOfSparePart> lst);
        void Edit(MasterPlansOfSparePart obj);
        void Delete(int ID);
        bool CheckExists(MasterPlansOfSparePart obj);
        void SaveChanges();
        IEnumerable<MasterPlansOfSparePart> ListAll();
        IEnumerable<MasterPlansOfSparePart> GetSparePartByPlanID(int planID);
        IEnumerable<MasterPlansOfSparePart> GetSparePartByWorkPlan(int planID, int workID, string componentID);
    }
}
