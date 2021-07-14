using Model.Data;
using Model.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Model.Interface
{
    public interface IUserRequestRepository
    {
        UserRequest GetUserRequest(int ID);
        IEnumerable<GetUserRequestObj> GetUserRequest(string requestedBy, string MS_N_XUONG, string fromDate, string toDate, string username, int languages);
        IEnumerable<GetUserRequestObj> GetCreatedBy();
        string GetNguoiYC(string UserName);
        string CreateRequestID();
        Boolean CheckExistRequest(string ID);
        void SaveRequest(UserRequest obj);
        void SaveChanges();
        IEnumerable<PhuTungViewModel.PhuTung> DanhSachPhuTungTheoMay(int id, string may,int stt_vande);
        IEnumerable<PhuTungViewModel.BoPhan> DanhSachBoPhanTheoMay(int id, string may);
        int ThemChiTietBoPhan(PhuTungViewModel model);
        int ThemChiTietHinh(int stt, string msmay, int stt_vande, string filename);
        int XoaChiTietBoPhan(int ID);
        int XoaYeuCau(int ID);
        int XoaChiTietHinh(int ID);
        string GetPath(int ID);
        SelectList GetNguoiYeuCau(string UserName);
        IEnumerable<DiaDiemViewModel> GetNhaXuong(string UserName, int NNgu, int Coall);
    }
}
