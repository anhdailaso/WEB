using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IOrgUnitRepository
    {
        IEnumerable<GetOrgUnitByUserObj> GetOrgUnitByUser(bool isAll, string username);
        IEnumerable<OrgUnit> ListAll();
        void Add(OrgUnit unit);
        void Delete(string unitID);
        void Edit(OrgUnit unit);
        OrgUnit GetOrgUnit_ID(string unitID);
        bool CheckIDExist(string unitID);
        bool CheckNameExist(string unitID, string name);
        bool CheckShortNameExist(string unitID, string shortName);
        bool CheckDepartmentExist(string unitID);
        bool CheckCostCenterExist(string unitID);
  
    }
}
