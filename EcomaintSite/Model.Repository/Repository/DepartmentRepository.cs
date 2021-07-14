using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Model.Data;
namespace Model.Repository
{
   public class DepartmentRepository : IDepartmentRepository, IDisposable
    {
        Model.Data.Model1 db;
        public DepartmentRepository() =>  db = new Model.Data.Model1();
        
        public IEnumerable<GetDepartmentObj> GetDepartment(bool all, string unitID, string username) =>
            db.Database.SqlQuery<GetDepartmentObj>("GetDepartment @isALL, @unitID, @username", new object[]
            {
                new SqlParameter("@isALL", all),
                new SqlParameter("@unitID", unitID),
                new SqlParameter("@username", username)
            }).ToList();

        private bool disposed = false;
        protected void Dispose(bool disposing)
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
