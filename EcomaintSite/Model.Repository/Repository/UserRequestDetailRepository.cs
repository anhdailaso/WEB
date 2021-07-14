using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using Model.Interface;
using System.Web.Mvc;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using Biz.Lib.Helpers;

namespace Model.Repository
{
    public class UserRequestDetailRepository : IUserRequestDetailRepository, IDisposable
    {
        Model.Data.Model1 db;
        public UserRequestDetailRepository() => db = new Model1();

        public UserRequestDetailRepository(Model1 context) => db = context;

        public IEnumerable<GetRequestInfomationObj> GetRequestInfomationbySTTVD(Nullable<int> STTVD, string username)
        {
            IEnumerable<GetRequestInfomationObj> resulst = db.Database.SqlQuery<GetRequestInfomationObj>("GetRequestInfomationbySTTVD @STTVD,@username", new object[]
             {
                new SqlParameter("@STTVD", STTVD),
                new SqlParameter("@username", username)
             }).ToList();
            return resulst;
        }
        public IEnumerable<GetRequestInfomationObj> GetRequestInfomation(Nullable<int> ID, string username) =>
       db.Database.SqlQuery<GetRequestInfomationObj>("GetRequestInfomation @ID,@username", new object[]
       {
                new SqlParameter("@ID", ID),
                new SqlParameter("@username", username)
       }).ToList();
        public IEnumerable<UserRequestDetail> GetRequestDetailsByPlan(int planID) =>
            db.UserRequestDetail.Where(x => x.PlanID == planID).Count() > 0 ? db.UserRequestDetail.Where(x => x.PlanID == planID) : null;
        public IEnumerable<ApprovedRequestObj> ApprovedRequest(string user, Nullable<int> option, Nullable<int> lang, byte[] image, Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, string workSiteID, string typeOfDeviceID)
        {
            List<ApprovedRequestObj> list = null;
            try
            {
                list = db.Database.SqlQuery<ApprovedRequestObj>("ApprovedRequest @username, @option, @lang, @image, @fromDate, @toDate, @workSiteID, @typeOfDeviceID", new object[]
                {
                    new SqlParameter("@username", user),
                    new SqlParameter("@option", option),
                    new SqlParameter("@lang", lang), image == null ? new SqlParameter("@image", new byte[0]) :  new SqlParameter("@image", image),
                    new SqlParameter("@fromDate", fromDate),
                    new SqlParameter("@toDate", toDate),
                    new SqlParameter("@workSiteID", workSiteID),
                    new SqlParameter("@typeOfDeviceID", typeOfDeviceID)
                }).ToList();
            }
            catch (Exception)
            {
            }
            return list;

        }
        //db.Database.SqlQuery<ApprovedRequestObj>("ApprovedRequest @username, @option, @lang, @image, @fromDate, @toDate, @workSiteID, @typeOfDeviceID", new object[]
        //{
        //    new SqlParameter("@username", user),
        //    new SqlParameter("@option", option),
        //    new SqlParameter("@lang", lang), image == null ? new SqlParameter("@image", new byte[0]) :  new SqlParameter("@image", image),
        //    new SqlParameter("@fromDate", fromDate),
        //    new SqlParameter("@toDate", toDate),
        //    new SqlParameter("@workSiteID", workSiteID),
        //    new SqlParameter("@typeOfDeviceID", typeOfDeviceID)
        //}).ToList();
        public string SoYeuCau(int stt)
        {
            string resulst = "";
            try
            {
                resulst = Convert.ToString(SqlHelper.ExecuteScalar(DBUtils.BizConnectionString(), System.Data.CommandType.Text, "SELECT SO_YEU_CAU FROM dbo.YEU_CAU_NSD WHERE STT = " + stt + ""));
            }
            catch (Exception ex)
            {
                resulst = "";
            }
            return resulst;
        }
        public  string GetFullNAmeByUserName(string username)
        {
            string resulst = "";
            try
            {
                resulst = Convert.ToString(SqlHelper.ExecuteScalar(DBUtils.BizConnectionString(), System.Data.CommandType.Text, "SELECT FULL_NAME FROM dbo.USERS WHERE USERNAME = '" + username + "'"));
            }
            catch (Exception ex)
            {
                resulst = "";
            }
            return resulst;
        }

        public SelectList DanhSachNguyenNhan()
        {
            DataTable tb = new DataTable();
            tb.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString, System.Data.CommandType.Text, "SELECT MS_NGUYEN_NHAN AS 'Value',TEN_NGUYEN_NHAN AS 'Text' FROM NGUYEN_NHAN_DUNG_MAY ORDER BY TEN_NGUYEN_NHAN"));
            var listItem = tb.AsEnumerable().Select(
                  x => new SelectListItem
                  {
                      Text = x.Field<string>("Text"),
                      Value = x.Field<int>("Value").ToString()
                  });
            return new SelectList(listItem, "Value", "Text", null);
        }

        public SelectList DanhSachLoaiBaoTri()
        {
            DataTable tb = new DataTable();
            tb.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString, "GetLoaiYeuCauBaoTriAll", 0));
            var listItem = tb.AsEnumerable().Select(
                  x => new SelectListItem
                  {
                      Text = x.Field<string>("TEN_LOAI_YEU_CAU_BT"),
                      Value = x.Field<int>("MS_LOAI_YEU_CAU_BT").ToString()
                  });
            return new SelectList(listItem, "Value", "Text", null);
        }

        public SelectList DanhSachUuTien()
        {
            DataTable tb = new DataTable();
            tb.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString, "H_YEU_CAU_NSD_MUC_UU_TIEN"));
            var listItem = tb.AsEnumerable().Select(
                  x => new SelectListItem
                  {
                      Text = x.Field<string>("TEN_UU_TIEN"),
                      Value = x.Field<int>("MS_UU_TIEN").ToString()
                  });
            return new SelectList(listItem, "Value", "Text", null);
        }
        public void SaveRequestInfomation(UserRequestDetail requestInfo) => db.UserRequestDetail.Add(requestInfo);

        public void SaveChanges() => db.SaveChanges();

        public void ApproveRequests(List<UserRequestDetail> lst)
        {
            foreach (UserRequestDetail obj in lst)
            {
                db.UserRequestDetail.Attach(obj);
                db.Entry(obj).Property(x => x.EmailByApproveRequested).IsModified = true;
                db.Entry(obj).Property(x => x.ApprovedRequestBy).IsModified = true;
                db.Entry(obj).Property(x => x.DateApproveRequested).IsModified = true;
                db.Entry(obj).Property(x => x.ApprovalNote).IsModified = true;
                db.Entry(obj).Property(x => x.PriorityID).IsModified = true;
                db.Entry(obj).Property(x => x.IsExecuteApproveRequested).IsModified = true;
            }
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
