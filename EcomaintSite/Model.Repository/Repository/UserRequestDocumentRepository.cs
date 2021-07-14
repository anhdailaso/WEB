using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using Model.Interface;

namespace Model.Repository
{
    public class UserRequestDocumentRepository : IUserRequestDocumentRepository, IDisposable
    {
        Model.Data.Model1 db;
        public UserRequestDocumentRepository() => db = new Model1();

        public UserRequestDocumentRepository(Model1 context) => db = context;

        public IEnumerable<GetRequestDocumentObj> GetRequestDocument(Nullable<int> ID, string deviceID, Nullable<int> requestDetailID) => 
            db.Database.SqlQuery<GetRequestDocumentObj>("GetUserRequestDocument @ID, @deviceID, @requestDetailID", new object[]
            {
                new SqlParameter("@ID", ID),
                new SqlParameter("@deviceID", deviceID),
                new SqlParameter("@requestDetailID", requestDetailID)
            }).ToList();

        private bool disposed = false;
        protected  void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
