using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biz.Lib.Helpers
{
    public enum SessionKeyList
    {
        MessageSuccess = 1,
        MessageError = 2,
        MessageInform = 3,
        MessageUnAuthorize = 4,

        UserObject = 5,
        UserMenu = 6,
        IsInRoleObject = 7,
        UserAccessObject = 8,
        IsFirstLogin = 9,

        UserStatusObject,
        UserMenuData,
        ReportTeamProductivityDailyData,
        ReportTeamProductivityMonthlyData,

        CallModel,
        CallingPhone
    }

    public enum Status
    {
        Active = 1,
        Inactive = -1,
        Pending = 0,
        Locked = -2
    }

    public enum ImportPackageStatus
    {
        Pending = 0,
        Assignment = 1
    }

    public enum QueueDashboardStatus
    {
        Play = 1,
        Pause = -1
    }

    public enum AppName
    {
        BizGeneral,
        BizAuthentication,
        BizSecurity,
        Model1,

    }

    public enum ActionGroup
    {
        SECURITY_MANAGEMENT = 1,
        SECURITY_ACCESS = 2,

        C3_MANAGEMENT = 101,
        C3_ACCESS = 102
    }

    public enum ActionName
    {
        sec_application = 1,
        sec_controller = 2,
        sec_action = 3,
        sec_role = 4,
        sec_user = 5,
        sec_setting = 6,

        c3_user = 101,
    }

    public enum LogStatus
    {
        Success = 1,
        Fail = -1
    }

    public enum ResponseCode
    {
        Success = 1,
        Error = -1,
        Duplicate = 2,
        Lock = 3
    }

    public enum FcTargetResponseCode
    {
        Success = 1,
        Error = -1,
        InvalidTarget = -2,
        InvalidFcCode = -3
    }

    public enum CallProcessingUpdateContractStatusResponseCode
    {
        Success = 1,
        Error = -1,
        InvalidStatus = -2,
        InvalidContract = -3
    }
}
