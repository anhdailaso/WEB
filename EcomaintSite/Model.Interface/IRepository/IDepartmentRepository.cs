using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
   public interface IDepartmentRepository
    {
        IEnumerable<GetDepartmentObj> GetDepartment(bool all, string unitID, string username);
    }
}
