using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IStaffRepository
    {
        IEnumerable<StaffObjForDropdown> GetStaffRole(int role, string username);
        void SaveChanges();    
    }
}
