using Biz.Lib.Helpers;
using Model.Data;
using Model.Interface.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace Model.Repository.Repository
{
    public class SafetyRepository : ISafetyRepository, IDisposable
    {
        Model.Data.Model1 db;
        public SafetyRepository() => db = new Model1();
        public SafetyRepository(Model1 context) => db = context;
        public ST_HazardReport HazardReport(int ID) => db.ST_HazardReport.Where(x => x.ID == ID).FirstOrDefault();
        public IEnumerable<ChooseListHazard> ChooseListHazard(string tngay, string dngay)
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
                listParameter.Add(new SqlParameter("REPORT_PARENT", " "));
                listParameter.Add(new SqlParameter("DOCNUM", ""));
                listParameter.Add(new SqlParameter("External_N", "All"));
                listParameter.Add(new SqlParameter("IS_DELETE", false));
                listParameter.Add(new SqlParameter("NGUOILIENQUAN1", " "));
                listParameter.Add(new SqlParameter("NGUOILIENQUAN2", ""));
                listParameter.Add(new SqlParameter("STOP_WORK", "ALL"));
                list = DBUtils.ExecuteSPList<ChooseListHazard>("VS_ST_HazardReport", listParameter, AppName.Model1);
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        #region Hazard report
        public List<HazardReportViewModel.HazardReport1ViewModel> GetListDetail(string ID)
        {
            List<HazardReportViewModel.HazardReport1ViewModel> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("ID", ID));
                list = DBUtils.ExecuteSPList<HazardReportViewModel.HazardReport1ViewModel>("VS_ST_HazardReport1", listParameter, AppName.Model1);
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
                return list[0];
            }
            catch
            {
                return null;
            }
        }
        public string GetSoPhieuHazard()
        {
            string resulst = "";
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
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
            catch
            {
                resulst = "";
            }
            return resulst;
        }
        public int AddInsertHazardrepot(HazardReportViewModel model)
        {
            try
            {
                ST_HazardReport hazard = new ST_HazardReport();
                hazard.Acount = model.Acount;
                hazard.BelongContractor = model.BelongContractor;
                hazard.BelongToBSPort = model.BelongToBSPort;
                hazard.COMMENT = model.COMMENT;
                hazard.Commercial = model.Commercial;
                hazard.CreatedBy = model.CreatedBy;
                hazard.Createdtime = Convert.ToDateTime(model.sCreatedtime, new CultureInfo("vi-vn"));
                hazard.Department = model.Department;
                hazard.Description = model.Description;
                hazard.DocDate = Convert.ToDateTime(model.sDocDate, new CultureInfo("vi-vn"));
                hazard.DocNum = model.DocNum;
                hazard.DocTime = Convert.ToDateTime(model.DocTime, new CultureInfo("vi-vn"));
                hazard.Environment = model.Environment;

                hazard.HPES1 = model.HPES1;
                hazard.HPES2 = model.HPES2;
                hazard.HPES3 = model.HPES3;
                hazard.HPES4 = model.HPES4;
                hazard.HPES5 = model.HPES4;
                hazard.HPES6 = model.HPES6;

                hazard.HR = model.HR;
                hazard.ID = model.ID;
                hazard.Image_1 = model.Image_1;
                hazard.IS_DELETE = model.IS_DELETE;
                hazard.Location = model.Location;
                hazard.NearMiss = model.NearMiss;
                hazard.NGUOILIENQUAN1 = model.NGUOILIENQUAN1;
                hazard.NGUOILIENQUAN2 = model.NGUOILIENQUAN2;
                hazard.Operation = model.Operation;
                hazard.Other = model.Other;
                hazard.Picture = model.Picture;
                hazard.Procu = model.Procu;

                hazard.RANDOM = model.NGUOILIENQUAN2;
                hazard.REPORT_PARENT = model.REPORT_PARENT;
                hazard.SatetySuggestion = model.SatetySuggestion;
                hazard.Stopwork = model.Stopwork;

                hazard.Tally = model.Tally;
                hazard.TechHSE = model.TechHSE;
                hazard.TT = model.TT;
                hazard.UnsafeBehevior = model.UnsafeBehevior;
                hazard.UnsafeCondition = model.UnsafeCondition;
                hazard.APPROVAL_USER = model.APPROVAL_USER;
                hazard.IS_APPROVED = model.IS_APPROVED;
                db.ST_HazardReport.Add(hazard);
                db.SaveChanges();


                List<ST_HazardReport1> Hazard1 = new List<ST_HazardReport1>();
                model.ListHarzard1.ForEach(x =>
                {
                    Hazard1.Add(new ST_HazardReport1
                    {
                        HazardReportID = hazard.ID,
                        LOAIYEUCAU = x.LOAIYEUCAU,
                        MUCUUTIEN = x.MUCUUTIEN,
                        NGUYENNHAN = x.NGUYENNHAN,
                        PersonIncharge = x.PersonIncharge,
                        PersonReport = x.PersonReport,
                        PreventiveAction = x.PreventiveAction,
                        Status = x.Status,
                        THIETBI = x.THIETBI,
                        CompletedDate = x.CompletedDate,
                        Deadline = x.Deadline,
                        Description = x.Description,
                        ID = x.ID
                    });
                });
                db.ST_HazardReport1.AddRange(Hazard1);
                db.SaveChanges();
                return hazard.ID;
            }
            catch 
            {
                return -1;
            }
        }
        public bool EditHazardrepot(HazardReportViewModel model)
        {
            try
            {
                ST_HazardReport hazard = new ST_HazardReport();
                hazard.APPROVAL_USER = model.APPROVAL_USER;
                hazard.Acount = model.Acount;
                hazard.BelongContractor = model.BelongContractor;
                hazard.BelongToBSPort = model.BelongToBSPort;
                hazard.COMMENT = model.COMMENT;
                hazard.Commercial = model.Commercial;
                hazard.CreatedBy = model.CreatedBy;
                hazard.Createdtime = Convert.ToDateTime(model.sCreatedtime, new CultureInfo("vi-vn"));
                hazard.Department = model.Department;
                hazard.Description = model.Description;
                hazard.DocDate = Convert.ToDateTime(model.sDocDate, new CultureInfo("vi-vn"));
                hazard.DocNum = model.DocNum;
                hazard.DocTime = Convert.ToDateTime(model.DocTime, new CultureInfo("vi-vn"));
                hazard.Environment = model.Environment;

                hazard.HPES1 = model.HPES1;
                hazard.HPES2 = model.HPES2;
                hazard.HPES3 = model.HPES3;
                hazard.HPES4 = model.HPES4;
                hazard.HPES5 = model.HPES5;
                hazard.HPES6 = model.HPES6;

                hazard.HR = model.HR;
                hazard.ID = model.ID;
                hazard.Image_1 = model.Image_1;
                hazard.IS_APPROVED = model.IS_APPROVED;
                hazard.IS_DELETE = model.IS_DELETE;
                hazard.Location = model.Location;
                hazard.NearMiss = model.NearMiss;
                hazard.NGUOILIENQUAN1 = model.NGUOILIENQUAN1;
                hazard.NGUOILIENQUAN2 = model.NGUOILIENQUAN2;
                hazard.Operation = model.Operation;
                hazard.Other = model.Other;
                hazard.Picture = model.Picture;
                hazard.Procu = model.Procu;

                hazard.RANDOM = model.NGUOILIENQUAN2;
                hazard.REPORT_PARENT = model.REPORT_PARENT;
                hazard.SatetySuggestion = model.SatetySuggestion;
                hazard.Stopwork = model.Stopwork;

                hazard.Tally = model.Tally;
                hazard.TechHSE = model.TechHSE;
                hazard.TT = model.TT;
                hazard.UnsafeBehevior = model.UnsafeBehevior;
                hazard.UnsafeCondition = model.UnsafeCondition;


                db.ST_HazardReport.Attach(hazard);
                db.Entry(hazard).Property(x => x.Acount).IsModified = true;
                db.Entry(hazard).Property(x => x.BelongContractor).IsModified = true;
                db.Entry(hazard).Property(x => x.BelongToBSPort).IsModified = true;
                db.Entry(hazard).Property(x => x.COMMENT).IsModified = true;
                db.Entry(hazard).Property(x => x.Commercial).IsModified = true;
                db.Entry(hazard).Property(x => x.Description).IsModified = true;
                db.Entry(hazard).Property(x => x.Environment).IsModified = true;
                db.Entry(hazard).Property(x => x.External).IsModified = true;
                db.Entry(hazard).Property(x => x.HPES1).IsModified = true;
                db.Entry(hazard).Property(x => x.HPES2).IsModified = true;
                db.Entry(hazard).Property(x => x.HPES3).IsModified = true;
                db.Entry(hazard).Property(x => x.HPES4).IsModified = true;
                db.Entry(hazard).Property(x => x.HPES5).IsModified = true;
                db.Entry(hazard).Property(x => x.HPES6).IsModified = true;
                db.Entry(hazard).Property(x => x.HR).IsModified = true;
                db.Entry(hazard).Property(x => x.ID).IsModified = true;
                db.Entry(hazard).Property(x => x.Image_1).IsModified = true;
                db.Entry(hazard).Property(x => x.Location).IsModified = true;
                db.Entry(hazard).Property(x => x.NearMiss).IsModified = true;
                db.Entry(hazard).Property(x => x.NGUOILIENQUAN1).IsModified = true;
                db.Entry(hazard).Property(x => x.NGUOILIENQUAN2).IsModified = true;
                db.Entry(hazard).Property(x => x.Operation).IsModified = true;
                db.Entry(hazard).Property(x => x.Other).IsModified = true;
                db.Entry(hazard).Property(x => x.Procu).IsModified = true;
                db.Entry(hazard).Property(x => x.RANDOM).IsModified = true;
                db.Entry(hazard).Property(x => x.REPORT_PARENT).IsModified = true;
                db.Entry(hazard).Property(x => x.SatetySuggestion).IsModified = true;
                db.Entry(hazard).Property(x => x.Stopwork).IsModified = true;
                db.Entry(hazard).Property(x => x.Tally).IsModified = true;
                db.Entry(hazard).Property(x => x.TechHSE).IsModified = true;
                db.Entry(hazard).Property(x => x.TT).IsModified = true;
                db.Entry(hazard).Property(x => x.UnsafeBehevior).IsModified = true;
                db.Entry(hazard).Property(x => x.UnsafeCondition).IsModified = true;
                db.Entry(hazard).Property(x => x.Department).IsModified = true;
                db.Entry(hazard).Property(x => x.IS_APPROVED).IsModified = true;
                db.Entry(hazard).Property(x => x.APPROVAL_USER).IsModified = true;

                List<ST_HazardReport1> Hazard1 = new List<ST_HazardReport1>();

                model.ListHarzard1.ForEach(x =>
                {
                    Hazard1.Add(new ST_HazardReport1
                    {
                        HazardReportID = hazard.ID,
                        LOAIYEUCAU = x.LOAIYEUCAU,
                        MUCUUTIEN = x.MUCUUTIEN,
                        NGUYENNHAN = x.NGUYENNHAN,
                        PersonIncharge = x.PersonIncharge,
                        PersonReport = x.PersonReport,
                        PreventiveAction = x.PreventiveAction,
                        Status = x.Status,
                        THIETBI = x.THIETBI,
                        CompletedDate = x.CompletedDate,
                        Deadline = x.Deadline,
                        Description = x.Description,
                        ID = x.ID
                    });
                });
                if (Hazard1.Count(x => x.ID < 0) > 0)
                {
                    db.ST_HazardReport1.AddRange(Hazard1.Where(x => x.ID < -1).ToList());
                }
                else
                {
                    
                    foreach (ST_HazardReport1 item in Hazard1)
                    {
                        if (item.ID > 0)
                        {
                            db.ST_HazardReport1.Attach(item);
                            db.Entry(item).Property(x => x.LOAIYEUCAU).IsModified = true;
                            db.Entry(item).Property(x => x.MUCUUTIEN).IsModified = true;
                            db.Entry(item).Property(x => x.NGUYENNHAN).IsModified = true;
                            db.Entry(item).Property(x => x.PersonIncharge).IsModified = true;
                            db.Entry(item).Property(x => x.PersonReport).IsModified = true;
                            db.Entry(item).Property(x => x.PreventiveAction).IsModified = true;
                            db.Entry(item).Property(x => x.THIETBI).IsModified = true;
                            db.Entry(item).Property(x => x.Description).IsModified = true;
                            db.Entry(item).Property(x => x.Status).IsModified = true;
                            db.Entry(item).Property(x => x.CompletedDate).IsModified = true;
                        }
                    }
                    db.SaveChanges();
                    var result = db.ST_HazardReport1.Where(p => p.HazardReportID == hazard.ID);
                    foreach (var item in result)
                    {
                        if (Hazard1.Count(x => x.ID == item.ID) == 0)
                        {
                            db.ST_HazardReport1.Remove(item);
                        }
                    }
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(int ID)
        {
            try
            {
                db.ST_HazardReport1.RemoveRange(db.ST_HazardReport1.Where(x => x.HazardReportID == ID));
                db.ST_HazardReport.Remove(db.ST_HazardReport.Where(x => x.ID == ID).FirstOrDefault());
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool fnCheckAdminUser(string UserName)
        {//cac user trong nhom GROUP_ID=49  moi co quyen dyuyet
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "CHECK_ADMIN_USER");
                sqlcom.Parameters.AddWithValue("USER", UserName);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return false;
                else if (ds.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool fnCheckApprovalUser(string UserName,string form)
        {//User nao duoc duyet form nao
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "CHECK_APPROVAL_USER");
                sqlcom.Parameters.AddWithValue("FORMID", "FrmHazardReport");
                sqlcom.Parameters.AddWithValue("APPROVAL_USER", UserName);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return false;
                else if (ds.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception generatedExceptionName)
            {
                return false;
            }
        }
        public bool CheckSameDepartment(string CreateBy, string ReportParent)
        {//khoong cung phong bang nen khong co quyen duyet
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_DEPT");
                sqlcom.Parameters.AddWithValue("CreatedBy", CreateBy);
                sqlcom.Parameters.AddWithValue("REPORT_PARENT", ReportParent);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return false;
                else if (ds.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool fnCheckCancelApproval(string UserName, string ID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "CHECK_CANCEL_APPROVAL");
                sqlcom.Parameters.AddWithValue("ID", ID);
                sqlcom.Parameters.AddWithValue("APPROVAL_USER", UserName);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return false;
                else if (ds.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        
        public string fnGetListMailApproval(string ID,string ReportParent,string Username)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_MAIL_APPROVAL");
                sqlcom.Parameters.AddWithValue("ID", ID);
                sqlcom.Parameters.AddWithValue("USER", Username);
                sqlcom.Parameters.AddWithValue("REPORT_PARENT", ReportParent);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return "";
                else if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();
                else
                    return "";
            }
            catch(Exception ex)
            {
                return "";
            }
        }
        public string fnGetListMailApprovalAndCreatedBy(string ID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_MAIL_APPROVAL_CREATE_BY");
                sqlcom.Parameters.AddWithValue("ID", ID);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return "";
                else if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();
                else
                    return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string GetIDSafery(string Username)
        {
            try
            {
                string ID = db.ST_Safety.Where(x => x.Userlogin.Equals(Username)).Select(x => x.ID).FirstOrDefault().ToString();
                return ID;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public string GetDepartmentbyReportParent(string User)
        {
            try
            {
                string Department = db.ST_Safety.Where(x => x.Userlogin.Equals(User)).Select(x => x.Department).FirstOrDefault().ToString();
                return Department;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public string fnGetListMailIncharge(string ID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_MAIL_INCHARGE");
                sqlcom.Parameters.AddWithValue("ID", ID);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return "";
                else if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }
        public string fnGET_DEAR_APPROVAL(string ID,string Username,string ReportParent)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_DEAR_APPROVAL");
                sqlcom.Parameters.AddWithValue("ID", ID);
                sqlcom.Parameters.AddWithValue("USER", Username);
                sqlcom.Parameters.AddWithValue("REPORT_PARENT", ReportParent);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return "";
                else if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }
        public string fnGET_DEAR_INCHARGE(string ID, string Username, string ReportParent)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_DEAR_INCHARGE");
                sqlcom.Parameters.AddWithValue("ID", ID);
                sqlcom.Parameters.AddWithValue("USER", Username);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return "";
                else if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }
        public string fnGetApproval(string ID, string User, string ReportParent)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_REPORTER_APPROVAL");
                sqlcom.Parameters.AddWithValue("ID", ID);
                sqlcom.Parameters.AddWithValue("USER", User);
                sqlcom.Parameters.AddWithValue("REPORT_PARENT", ReportParent);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return "";
                else if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }
        public string GET_DEAR_APPROVAL_CREATE_BY(string ID, string UserName, string ReportParent)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_DEAR_APPROVAL_CREATE_BY");
                sqlcom.Parameters.AddWithValue("ID", ID);
                sqlcom.Parameters.AddWithValue("USER", UserName);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return "";
                else if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }
        public string fnGetReporter(string ID, string User, string ReportParent)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_REPORTER");
                sqlcom.Parameters.AddWithValue("ID", ID);
                sqlcom.Parameters.AddWithValue("USER", User);
                sqlcom.Parameters.AddWithValue("REPORT_PARENT", ReportParent);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return "";
                else if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }
        public DataTable fnGetListAction(string ID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_LIST_ACTION");
                sqlcom.Parameters.AddWithValue("ID", ID);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return new DataTable();
                else if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0];
                else
                    return new DataTable();
            }
            catch 
            {
                return new DataTable();
            }
        }
        public DataTable fnGetListActionDone(string ID,string reportID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_LIST_ACTION_DONE");
                sqlcom.Parameters.AddWithValue("ID", ID);
                sqlcom.Parameters.AddWithValue("RID", reportID);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_HazardReport";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count == 0)
                    return new DataTable();
                else if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0];
                else
                    return new DataTable();
            }
            catch
            {
                return new DataTable();
            }
        }
        #endregion

        #region StopCard
        public string GetSoPhieuStopCart()
        {
            string resulst = "";
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlcom = new SqlCommand();
                SqlConnection con = new SqlConnection(DBUtils.BizConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                sqlcom.Connection = con;
                sqlcom.Parameters.AddWithValue("ACTION", "GET_DOCNUM");
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandText = "VS_ST_StopCard";
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                        resulst = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch
            {
                resulst = "";
            }
            return resulst;
        }

        public IEnumerable<ChooseListStopCard> ChooseListStopCard(string tngay, string dngay)
        {
            List<ChooseListStopCard> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("ACTION", "LIST"));
                listParameter.Add(new SqlParameter("TUNGAY", tngay));
                listParameter.Add(new SqlParameter("DENNGAY", dngay));
                listParameter.Add(new SqlParameter("NGUOIPHUTRACH", "All"));
                listParameter.Add(new SqlParameter("NGUOIBAOCAO", "All"));
                listParameter.Add(new SqlParameter("REPORT_PARENT", " "));
                listParameter.Add(new SqlParameter("TRANGTHAI", "All"));
                listParameter.Add(new SqlParameter("DOCNUM", ""));
                listParameter.Add(new SqlParameter("LOAISC", ""));
                listParameter.Add(new SqlParameter("External_N", "All"));
                listParameter.Add(new SqlParameter("PHONGBAN", -1));
                listParameter.Add(new SqlParameter("IS_DELETE", false));
                listParameter.Add(new SqlParameter("NGUOILIENQUAN1", " "));
                listParameter.Add(new SqlParameter("NGUOILIENQUAN2", ""));
        
                list = DBUtils.ExecuteSPList<ChooseListStopCard>("VS_ST_StopCard", listParameter, AppName.Model1);
            }
            catch 
            {
            }
            return list;
        }
        public StopCardViewModel GetListStopCardByID(string ID)
        {
            List<StopCardViewModel> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("ACTION", "GET_BY_ID"));
                listParameter.Add(new SqlParameter("ID", ID));
                list = DBUtils.ExecuteSPList<StopCardViewModel>("VS_ST_StopCard", listParameter, AppName.Model1);
                return list[0];
            }
            catch
            {
                return null;
            }
        }

        public List<StopCardViewModel.StopCard1ViewModel> GetListStopCardDetail1(string ID)
        {
            List<StopCardViewModel.StopCard1ViewModel> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("ID", ID));
                list = DBUtils.ExecuteSPList<StopCardViewModel.StopCard1ViewModel>("VS_ST_StopCard1", listParameter, AppName.Model1);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<StopCardViewModel.StopCard2ViewModel> GetListStopCardDetail2(string ID)
        {
            List<StopCardViewModel.StopCard2ViewModel> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("ID", ID));
                list = DBUtils.ExecuteSPList<StopCardViewModel.StopCard2ViewModel>("VS_ST_StopCard2", listParameter, AppName.Model1);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        #endregion


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