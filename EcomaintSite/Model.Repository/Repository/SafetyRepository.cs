using Biz.Lib.Helpers;
using Model.Data;
using Model.Interface.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Model.Repository.Repository
{
    public class SafetyRepository : ISafetyRepository, IDisposable
    {
        Model.Data.Model1 db;
        public SafetyRepository() => db = new Model1();
        public SafetyRepository(Model1 context) => db = context;

        public IEnumerable<ChooseListHazard> ChooseListHazard(DateTime tngay, DateTime dngay)
        {
            List<ChooseListHazard> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("ACTION", "LIST"));
                listParameter.Add(new SqlParameter("TUNGAY", tngay));
                listParameter.Add(new SqlParameter("DENNGAY", dngay));
                listParameter.Add(new SqlParameter("NGUOIPHUTRACH", "All"));
                listParameter.Add(new SqlParameter("NGUOIBAOCAO", "All"));
                listParameter.Add(new SqlParameter("PHONGBAN", -1));
                listParameter.Add(new SqlParameter("TRANGTHAI", "All"));
                listParameter.Add(new SqlParameter("LOAISC", ""));
                listParameter.Add(new SqlParameter("LOAISC2", ""));
                listParameter.Add(new SqlParameter("REPORT_PARENT","" ));
                listParameter.Add(new SqlParameter("DOCNUM","" ));
                listParameter.Add(new SqlParameter("External_N", "All"));
                listParameter.Add(new SqlParameter("IS_DELETE", false));
                listParameter.Add(new SqlParameter("NGUOILIENQUAN1","" ));
                listParameter.Add(new SqlParameter("NGUOILIENQUAN2", ""));
                listParameter.Add(new SqlParameter("STOP_WORK", "0"));
                list = DBUtils.ExecuteSPList<ChooseListHazard>("VS_ST_HazardReport", listParameter, AppName.Model1);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public HazardReportViewModel GetListByID(string ID)
        {
            List<HazardReportViewModel> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("ACTION", "GET_BY_ID"));
                listParameter.Add(new SqlParameter("ID", ID));
                list = DBUtils.ExecuteSPList<HazardReportViewModel>("VS_ST_HazardReport", listParameter, AppName.Model1);
            }
            catch (Exception ex)
            {
            }
            return list[0];
        }

        public string GetSoPhieuHazard()
        {
            string resulst = "";
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(db.Database.Connection.ConnectionString);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_DOCNUM");
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                        resulst = ds.Tables[0].Rows[0][0].ToString();
                }
            }

            catch (Exception generatedExceptionName)
            {
                resulst = "";
            }
            return resulst;

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