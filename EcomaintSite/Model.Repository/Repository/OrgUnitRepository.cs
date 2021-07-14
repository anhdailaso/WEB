using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace Model.Repository
{
    public class OrgUnitRepository : IOrgUnitRepository, IDisposable
    {
        Model.Data.Model1 db;
        public OrgUnitRepository() => db = new Model.Data.Model1();

        public IEnumerable<GetOrgUnitByUserObj> GetOrgUnitByUser(bool isAll, string username) =>
           db.Database.SqlQuery<GetOrgUnitByUserObj>("GetOrgUnitByUser @isAll, @username",
           new object[]
           {
                new SqlParameter("@isAll", isAll),
                new SqlParameter("@username", username)
           }).ToList();
    
        public IEnumerable<OrgUnit> ListAll() => db.OrgUnit.ToList();

        public OrgUnit GetOrgUnit_ID(string ID) => db.OrgUnit.SingleOrDefault(x => x.ID == ID);

        public bool CheckIDExist(string ID) => db.OrgUnit.Any(x => x.ID == ID);

        public bool CheckNameExist(string ID, string name) => db.OrgUnit.Any(x => x.Name == name && x.ID != ID);

        public bool CheckShortNameExist(string ID, string shortName) => db.OrgUnit.Any(x => x.ShortName == shortName && x.ID != ID);

        public bool CheckDepartmentExist(string ID) => db.Department.Any(x => x.OrgUnitID == ID);

        public bool CheckCostCenterExist(string ID) => db.Budget.Any(x => x.OrgUnitID == ID);

        public void Add(OrgUnit unit)
        {
            db.OrgUnit.Add(unit);
            db.SaveChanges();
        }
        public void Delete(string ID)
        {
            db.OrgUnit.Remove(db.OrgUnit.SingleOrDefault(x => x.ID == ID));
            db.SaveChanges();
        }
        public void Edit(OrgUnit unit)
        {
            var entityProperties = unit.GetType().GetProperties();
            db.Entry(unit).State = EntityState.Modified;
            db.OrgUnit.Attach(unit);
            db.Entry(unit).Property("Name").IsModified = true;
            db.Entry(unit).Property("ShortName").IsModified = true;
            db.Entry(unit).Property("Address").IsModified = true;
            db.Entry(unit).Property("IsOutsource").IsModified = true;
            db.Entry(unit).Property("Default").IsModified = true;
            db.Entry(unit).Property("Phone").IsModified = true;
            db.Entry(unit).Property("Fax").IsModified = true;
            db.Entry(unit).Property("NameOfEng").IsModified = true;
            db.Entry(unit).Property("NameOfChina").IsModified = true;
            db.Entry(unit).Property("CompactName").IsModified = true;
            db.SaveChanges();
        }
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
