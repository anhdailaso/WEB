using Model.Data;
using Model.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Model.Interface
{
    public interface ICombobox : IDisposable
    {
        SelectList DanhSachNguyenNhan();
        SelectList GetCbbDiaDiem(string UserName,int NNgu, int CoAll);
        SelectList GetCbbHeThong(string UserName, int NNgu, int CoAll);
        SelectList GetCbbLoaiMay(string UserName, int NNgu, int CoAll);
        SelectList GetCbbMay(string UserName, int NNgu, int CoAll);
        SelectList NhanVienKT(string Username);
        SelectList GetCbbLoaiCV(string UserName, int NNgu, int CoAll);

        SelectList GetListReportParent(string UserName);
        SelectList GetListLocation();
        SelectList GetListDepartment();
        SelectList GetListPersonRef(string UserName);

        SelectList LoadListInCharge();
        SelectList LoadListThietBi();
        SelectList LoadListNguyenNhan();
        SelectList LoadListLoaiYC();
        SelectList LoadListUuTien(int NN);

        string GetEmailByNhaXuong(int stt, string msnx, string loaiyc, string username, string mailthem);
        List<EmailViewModel> AutoCompleteMail();
        void SendEmail(string address, string subject, string message,string link);
        void SendEmailCC(string address, string CC, string subject, string message);
    }
}
