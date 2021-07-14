using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
   public interface IDatabaseRepository  
    {
        IEnumerable<DataBase> GetDatabase();
    }
}
