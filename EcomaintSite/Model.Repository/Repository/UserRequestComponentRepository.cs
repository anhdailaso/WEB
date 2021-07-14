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
    public class UserRequestComponentRepository : IUserRequestComponentRepository, IDisposable
    {
        Model.Data.Model1 db;
        public UserRequestComponentRepository()
        {
            db = new Model1();
        }

        public UserRequestComponentRepository(Model1 context)
        {
            db = context;
        }

        public IEnumerable<GetUserRequestComponent> GetUserRequestComponent(Nullable<int> ID, string deviceID, Nullable<int> requestDetailID) =>
            db.Database.SqlQuery<GetUserRequestComponent>("GetUserRequestComponent @ID, @deviceID, @requestDetailID", new object[]
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
