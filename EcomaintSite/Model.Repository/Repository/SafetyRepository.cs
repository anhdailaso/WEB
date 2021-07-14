using Biz.Lib.Helpers;
using Model.Data;
using Model.Interface.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace Model.Repository.Repository
{
    public class SafetyRepository : ISafetyRepository, IDisposable
    {
        Model.Data.Model1 db;
        public SafetyRepository() => db = new Model1();
        public SafetyRepository(Model1 context) => db = context;

        public IEnumerable<MyEcomainViewModel> GetMyEcomain(string username, int languages, string tngay, string dngay, string ms_nx,string may,int giaidoan)
        {

            List<MyEcomainViewModel> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@TNgay", tngay));
                listParameter.Add(new SqlParameter("@DNgay", dngay));
                listParameter.Add(new SqlParameter("@UserName", username));
                listParameter.Add(new SqlParameter("@MsNXuong", ms_nx));
                listParameter.Add(new SqlParameter("@MsMay", may == ""? "-1":may));
                listParameter.Add(new SqlParameter("@GiaiDoan", giaidoan));
                listParameter.Add(new SqlParameter("@NNgu", languages));
                list = DBUtils.ExecuteSPList<MyEcomainViewModel>("spGetPBTGSTT", listParameter, AppName.Model1);
            }
            catch (Exception ex)
            {
            }
            return list;
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