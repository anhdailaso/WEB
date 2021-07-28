
using Model.Data;
namespace Model.Interface
{
    public interface IThoiGianChayMayRepository
    {
        THOI_GIAN_CHAY_MAY GetThoiGianChayMayInfo();
        double? SoGioLuyKeHienTai(string msmay);
        THOI_GIAN_CHAY_MAY ThoiGianLuyKeTruoc(string msmay);
        void Add (THOI_GIAN_CHAY_MAY obj);
    }
}
