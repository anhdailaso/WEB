using Model.Data;
using System;
using System.Collections.Generic;
namespace Model.Interface
{
    public interface IChartRepository
    {
         IEnumerable<GetSituationWOObj> GetSituationWO(Nullable<int> lang);
         IEnumerable<GetSituationWOColumnObj> GetSituationWOColumn(Nullable<int> lang);
         IEnumerable<GetDeviceInfoObj> GetDeviceInfo(Nullable<int> lang);
         IEnumerable<GetDeviceStatusObj> GetDeviceStatus(Nullable<System.DateTime> toDate, string Username, Nullable<int> lang);
        int GetSoMay();
    }
}
