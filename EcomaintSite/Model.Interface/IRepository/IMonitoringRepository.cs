using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IMonitoringRepository
    {
        IEnumerable<Monitoring> ListAll();
        IEnumerable<Monitoring> ListAll(DateTime dt);
        void Add(Monitoring monitor);
        void Edit(Monitoring monitor);
        void Delete(int ID);
        IEnumerable<MonitoringParametersByDevice> GetParameterInfo(int ID);
        IEnumerable<DeviceObjForDropdown> CheckTheParametersDue();
        IEnumerable<MonitoringParametersByDevice> GetMonitoringParametersByDevice(string deviceID, int isDue,string dngay,int msloaicv,int stt);
        IEnumerable<MoningtoringViewModel> GetGiamSatTinhTrang(DateTime tungay, DateTime denngay, string mscn, string msmay);
        IEnumerable<MoningtoringViewModel.ThongSoGiamSat> GetThongSoGSTT(int stt, int dat, string msmay, int loaits);
        IEnumerable<MoningtoringViewModel.ThongSoGiamSat.GiaTri> GetGiaTriDT(int stt, string msmay, string msbp, string msts, int loai);
        IEnumerable<MoningtoringViewModel.ThongSoGiamSat.GiaTriDL> GetGiaTriDL(int stt, string msmay, string msbp, string msts, string Username, int NNgu);
        string CreateSoPhieu(DateTime date);
        int XoaGiamSatTinhTrang(int ID);


    }
}
