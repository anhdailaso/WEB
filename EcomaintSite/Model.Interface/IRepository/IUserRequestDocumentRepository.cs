using Model.Data;
using System;
using System.Collections.Generic;
namespace Model.Interface
{
    public interface IUserRequestDocumentRepository : IDisposable
    {
        IEnumerable<GetRequestDocumentObj> GetRequestDocument(Nullable<int> ID, string deviceID, Nullable<int> requestDetailID);
    }
}
