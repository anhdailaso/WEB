
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IMenuRepository
    {
        IEnumerable<WebMenu> ListAll(string UserName);
        IEnumerable<WebMenu> GetChildMenuID(int ID,string UserName);
        IEnumerable<WebMenu> GetAllChildMenuID(string menuID, string UserName);
    }
}
