using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class StaffRepository : IStaffRepository, IDisposable
    {
        Model.Data.Model1 db;
        public StaffRepository() => db = new Model.Data.Model1();

        public StaffRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<StaffObjForDropdown> GetStaffRole(int role, string username) => 
            db.Database.SqlQuery<StaffObjForDropdown>("GetStaffRole @role, @username ", new object[]
            {
                new SqlParameter("@role", role),
                new SqlParameter("@username", username)
            }).ToList();        

        public void SaveChanges() => db.SaveChanges();

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
