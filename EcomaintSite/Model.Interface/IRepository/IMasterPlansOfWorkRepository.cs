using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IMasterPlansOfWorkRepository : IDisposable
    {
        void Add(MasterPlansOfWork obj);
        bool CheckExists(MasterPlansOfWork obj);
        void Edit(MasterPlansOfWork obj);
        void AddRange(IEnumerable<MasterPlansOfWork> lst);
        void Delete(int ID);
        void SaveChanges();
        IEnumerable<MasterPlansOfWork> GetMasterPlansOfWorkByPlanID(int planID);
        IEnumerable<MasterPlansOfWork> ListAll();
    }
}
