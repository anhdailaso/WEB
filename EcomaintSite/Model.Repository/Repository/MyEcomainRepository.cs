using Biz.Lib.Helpers;
using Model.Data;
using Model.Interface.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace Model.Repository.Repository
{
    public class MyEcomainRepository : IMyEcomainRepository, IDisposable
    {
        Model.Data.Model1 db;
        public MyEcomainRepository() => db = new Model1();
        public MyEcomainRepository(Model1 context) => db = context;

        public IEnumerable<PhieuBaoTriViewModel> GetPhieuBaoTris(string username, int languages, string ms_nx, int ms_ht, string ms_loaimay, string ms_may)
        {
            List<PhieuBaoTriViewModel> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@USERNAME", username));
                listParameter.Add(new SqlParameter("@LANGUAGE", languages));
                listParameter.Add(new SqlParameter("@MS_NHA_XUONG",ms_nx));
                listParameter.Add(new SqlParameter("@MS_HE_THONG",ms_ht));
                listParameter.Add(new SqlParameter("@MS_LOAI_MAY",ms_loaimay));
                listParameter.Add(new SqlParameter("@MS_MAY", ms_may));
                listParameter.Add(new SqlParameter("@MS_TINH", "-1"));
                listParameter.Add(new SqlParameter("@MS_QUAN", "-1"));
                listParameter.Add(new SqlParameter("@MS_DUONG", "-1"));
                list = DBUtils.ExecuteSPList<PhieuBaoTriViewModel>("SP_Y_GET_PHIEUBAOTRI", listParameter, AppName.Model1);
            }
            catch (Exception)
            {
            }
            return list;
        }
        public IEnumerable<GiamSatTinhTrangViewModel> GetGSTRs(string username, int languages, string tngay, string dngay, string ms_nx, int ms_ht, string ms_loaimay, string ms_may, int loaiCV)
        {
            List<GiamSatTinhTrangViewModel> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@USERNAME", username));
                listParameter.Add(new SqlParameter("@LANGUAGE", languages));
                listParameter.Add(new SqlParameter("@TU_NGAY", tngay));
                listParameter.Add(new SqlParameter("@DEN_NGAY", dngay));
                listParameter.Add(new SqlParameter("@MS_NHA_XUONG", ms_nx));
                listParameter.Add(new SqlParameter("@MS_HE_THONG", ms_ht));
                listParameter.Add(new SqlParameter("@MS_LOAI_MAY", ms_loaimay));
                listParameter.Add(new SqlParameter("@MS_MAY", ms_may));
                listParameter.Add(new SqlParameter("@MS_LOAI_CV", loaiCV));
                list = DBUtils.ExecuteSPList<GiamSatTinhTrangViewModel>("spGetGSTT", listParameter, AppName.Model1);
            }
            catch (Exception)
            {
            }
            return list;
        }
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