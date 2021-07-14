using Model.Data;
using System.Collections.Generic;
namespace Model.Interface
{
    public interface IPriorityRepository
    {
        IEnumerable<Priority> ListAll();
    }
}
