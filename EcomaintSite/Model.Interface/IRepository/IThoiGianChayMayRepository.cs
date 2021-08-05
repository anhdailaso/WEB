
using Model.Data;
using System.Collections.Generic;

namespace Model.Interface
{
    public interface IThoiGianChayMayRepository
    {
        IEnumerable<THOI_GIAN_CHAY_MAY> GetThoiGianChayMayInfo(string MS_MAY);
        double? SoGioLuyKeHienTai(string msmay);
        THOI_GIAN_CHAY_MAY ThoiGianLuyKeTruoc(string msmay);
        void Add (THOI_GIAN_CHAY_MAY obj);
    }
}
