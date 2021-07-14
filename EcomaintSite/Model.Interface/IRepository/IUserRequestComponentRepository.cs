using Model.Data;
using System;
using System.Collections.Generic;

namespace Model.Interface
{
    public interface IUserRequestComponentRepository
    {
        IEnumerable<GetUserRequestComponent> GetUserRequestComponent(Nullable<int> ID, string deviceID, Nullable<int> requestDetailID);      
    }
}
