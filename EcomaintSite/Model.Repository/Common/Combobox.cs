using Biz.Lib.Helpers;
using Microsoft.ApplicationBlocks.Data;
using Model.Data;
using Model.Data.ViewModel;
using Model.Interface;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace Model.Combobox
{
    public class Combobox : ICombobox, IDisposable
    {
        Model.Data.Model1 db;
        public Combobox() => db = new Model1();
        public Combobox(Model1 context) => db = context;
        //viec list o day
        public SelectList DanhSachNguyenNhan()
        {
            DataTable tb = new DataTable();
            tb.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString, System.Data.CommandType.Text, "SELECT MS_NGUYEN_NHAN AS 'Value',TEN_NGUYEN_NHAN AS 'Text' FROM NGUYEN_NHAN_DUNG_MAY ORDER BY TEN_NGUYEN_NHAN"));
            var listItem = tb.AsEnumerable().Select(
                  x => new SelectListItem
                  {
                      Text = x.Field<string>("Text"),
                      Value = x.Field<string>("Value")
                  });
            return new SelectList(listItem, "Value", "Text", null);
        }
        public SelectList GetCbbDiaDiem(string Username, int NNgu, int CoAll)
        {
            DataTable tb = new DataTable();
            tb.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString, "GetNhaXuongAll", Username, NNgu, CoAll));
            var listItem = tb.AsEnumerable().Select(
             x => new SelectListItem
             {
                 Text = x.Field<string>("TEN_N_XUONG"),
                 Value = x.Field<string>("MS_N_XUONG")
             });
            return new SelectList(listItem, "Value", "Text", null);
        }
        public SelectList GetCbbHeThong(string Username, int NNgu, int CoAll)
        {
            DataTable tb = new DataTable();
            tb.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString, "GetHeThongAll", CoAll, Username, NNgu));
            var listItem = tb.AsEnumerable().Select(
             x => new SelectListItem
             {
                 Text = x.Field<string>("TEN_HE_THONG"),
                 Value = x.Field<int>("MS_HE_THONG").ToString()
             });
            return new SelectList(listItem, "Value", "Text", null);
        }
        public SelectList GetCbbLoaiMay(string Username, int NNgu, int CoAll)
        {
            DataTable tb = new DataTable();
            tb.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString, "GetLoaiMayAll", CoAll, Username, NNgu));
            var listItem = tb.AsEnumerable().Select(
             x => new SelectListItem
             {
                 Text = x.Field<string>("TEN_LOAI_MAY"),
                 Value = x.Field<string>("MS_LOAI_MAY")
             });
            return new SelectList(listItem, "Value", "Text", null);
        }
        public SelectList GetCbbMay(string Username, int NNgu, int CoAll)
        {
            DataTable tb = new DataTable();
            tb.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString, "SP_Y_GET_MAY", Username, NNgu, "-1", "-1", "-1", "-1", "-1", "-1"));
            tb.Rows.Add("-1", " < ALL > ", "-1");
            tb.DefaultView.Sort = "MS_MAY";
            tb = tb.DefaultView.ToTable();
            var listItem = tb.AsEnumerable().Select(
             x => new SelectListItem
             {
                 Text = x.Field<string>("TEN_MAY"),
                 Value = x.Field<string>("MS_MAY")
             });
            return new SelectList(listItem, "Value", "Text", null);
        }
        public SelectList NhanVienKT(string Username)
        {
            if (Username == "") Username = "Admin";
            UserRepository us = new UserRepository();
            string s = us.GetUserByID(Username).StaffID;
            DataTable tb = new DataTable();
            tb.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString, "MGetCNVaiTroUsers", "-1", Username));
            var listItem = tb.AsEnumerable().Select(
             x => new SelectListItem
             {
                 Text = x.Field<string>("HO_TEN_CONG_NHAN"),
                 Value = x.Field<string>("MS_CONG_NHAN")
             });
            return new SelectList(listItem, "Value", "Text", s != "" ? s : null);
        }
        public SelectList GetCbbLoaiCV(string Username, int NNgu, int CoAll)
        {
            DataTable dtTmp = new DataTable();
            string sSql =
                        " SELECT T1.MS_LOAI_CV,T1.TEN_LOAI_CV FROM LOAI_CONG_VIEC T1 INNER JOIN NHOM_LOAI_CONG_VIEC T2 ON " +
                        " T1.MS_LOAI_CV = T2.MS_LOAI_CV INNER JOIN USERS T3 ON T2.GROUP_ID = T3.GROUP_ID " +
                        " WHERE USERNAME = '" + Username + "' UNION  SELECT -1, ' < ALL > ' " +
                        " ORDER BY TEN_LOAI_CV";
            dtTmp.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString, CommandType.Text, sSql));
            var listItem = dtTmp.AsEnumerable().Select(
             x => new SelectListItem
             {
                 Text = x.Field<string>("TEN_LOAI_CV"),
                 Value = x.Field<int>("MS_LOAI_CV").ToString()
             });
            return new SelectList(listItem, "Value", "Text", null);
        }
        public string GetEmailByNhaXuong(int stt, string msnx, string loaiyc, string username, string mailthem)
        {
            //kiểm tra mail thêm
            string smail = mailthem.Trim();
            if (smail.StartsWith(";"))
            {
                smail = smail.Substring(1).Trim();
            }
            if (smail.EndsWith(";"))
            {
                smail = smail.Substring(0, smail.Length - 1).Trim();
            }
            string resulst = Convert.ToString(SqlHelper.ExecuteScalar(db.Database.Connection.ConnectionString, "spGetEmailYCSD", stt, msnx, loaiyc, username, smail));
            smail = resulst.Trim();
            if (smail.StartsWith(";"))
            {
                smail = smail.Substring(1).Trim();
            }
            if (smail.EndsWith(";"))
            {
                smail = smail.Substring(0, smail.Length - 1).Trim();
            }
            return smail;
        }
        public List<EmailViewModel> AutoCompleteMail()
        {
            List<EmailViewModel> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                list = DBUtils.ExecuteSPList<EmailViewModel>("sp_GetAutocompleteMail", listParameter, AppName.Model1);
            }
            catch (Exception)
            {
            }
            return list;
        }
        public void SendEmail(string address, string subject, string message, string link)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString, CommandType.Text, "SELECT MAIL_FROM,PASS_MAIL,SMTP_MAIL,PORT_MAIL,LINK_WEB FROM dbo.THONG_TIN_CHUNG"));
            string str = dt.Rows[0]["PASS_MAIL"].ToString();
            string password = "";
            const int _CODE_ = 354;
            for (int i = 0; i < str.Length; i++)
            {
                password += System.Convert.ToChar(((int)System.Convert.ToChar(str.Substring(i, 1)) / 2) - _CODE_).ToString();
            }
            message += string.Format(link, dt.Rows[0]["LINK_WEB"].ToString(), dt.Rows[0]["LINK_WEB"].ToString());
            string email = dt.Rows[0]["MAIL_FROM"].ToString();
            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient(dt.Rows[0]["SMTP_MAIL"].ToString());
            msg.From = new MailAddress(email,"CMMS");
            var mail = address.Split(';');
            foreach (var item in mail)
            {
                msg.To.Add(new MailAddress(item));
            }
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.BodyEncoding = Encoding.UTF8;
            //msg.Priority = MailPriority.High;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
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
