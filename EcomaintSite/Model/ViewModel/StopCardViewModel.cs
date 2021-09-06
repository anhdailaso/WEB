using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model.Data
{
    public class StopCardViewModel
    {
        public int ID { get; set; }
        public string CreatedBy { get; set; }
        public string DocNum { get; set; }
        public DateTime? DocDate { get; set; }
        public string sDocDate { get; set; }
        public DateTime? DocTime { get; set; }
        public bool AdjPerPPE { get; set; }
        public bool StopJob { get; set; }
        public bool ChangingPosition { get; set; }
        public bool RearrangingJob { get; set; }
        public bool AttachingGrouds { get; set; }
        public bool PerformingLocks { get; set; }
        public bool OtherReaction { get; set; }
        public bool Head { get; set; }
        public bool ArmsAndHands { get; set; }
        public bool EyesAndFace { get; set; }
        public bool Trunk { get; set; }
        public bool Ears { get; set; }
        public bool LegsAndFeet { get; set; }
        public bool RespiratorySystem { get; set; }
        public bool OtherPPE { get; set; }
        public bool HPES1 { get; set; }
        public bool HPES2 { get; set; }
        public bool HPES3 { get; set; }
        public bool HPES4 { get; set; }
        public bool HPES5 { get; set; }
        public bool HPES6 { get; set; }
        public bool StrikingAgaintObjects { get; set; }
        public bool ChokingHazardousAir { get; set; }
        public bool CaughtInOn { get; set; }
        public bool Inhaling { get; set; }
        public bool Falling { get; set; }
        public bool RepettiveMotions { get; set; }
        public bool Contacting { get; set; }
        public bool PositionsToWork { get; set; }
        public bool ContactSharpObject { get; set; }
        public bool FlyingObjects { get; set; }
        public bool ContactingElectric { get; set; }
        public bool EmployeeWork { get; set; }
        public bool NotRight { get; set; }
        public bool Unknow { get; set; }
        public bool UsedIncorrectly { get; set; }
        public bool NotUnderstood { get; set; }
        public bool InUnsafeCondition { get; set; }
        public bool NotFlowed { get; set; }
        public bool NotAequate { get; set; }
        public bool OtherProcedure { get; set; }
        public bool OtherTools { get; set; }
        public bool NotIsolate { get; set; }
        public bool ToolAndEquipment { get; set; }
        public bool NoCollectGarbage { get; set; }
        public bool EmergencyExit { get; set; }
        public bool Slip { get; set; }
        public bool OtherOderLine { get; set; }
        public bool External { get; set; }
        public bool DUNG_CU_THIET_BI { get; set; }
        public bool KHU_VUC_LAM_VIEC { get; set; }
        public bool MOI_TRUONG_LAM_VIEC { get; set; }
        public bool SACH_SE_NGAN_NAP { get; set; }
        public string LOAI_CONG_VIEC { get; set; }
        public string HO_TEN_CA_NHAN_TO_NHOM { get; set; }
        public string HANH_DONG_HANH_VI_AN_TOAN { get; set; }
        public string HANH_VI_KHONG_AN_TOAN { get; set; }
        public string KHAC_PHUC_TUC_THI { get; set; }
        public bool IS_APPROVED { get; set; }
        public string APPROVAL_USER { get; set; }
        public string TOtherPPE { get; set; }
        public string TOtherOrderliness { get; set; }
        public string TOtherProcedure { get; set; }
        public string TOtherReactions { get; set; }
        public string TOtherToolAndEquipment { get; set; }
        public string Description { get; set; }
        public string REPORT_PARENT { get; set; }
        public byte[] Image_1 { get; set; }
        public bool IS_DELETE { get; set; }
        public string NGUOILIENQUAN1 { get; set; }
        public string NGUOILIENQUAN2 { get; set; }
        public int HPES { get; set; }


        public List<StopCard1ViewModel> StopCard1 { get; set; }
        public class StopCard1ViewModel
        {
            public int ID { get; set; }
            public int StopCardID { get; set; }
            public string PreventiveAction { get; set; }

            public string PersonIncharge { get; set; }
            public string TENPersonIncharge { get; set; }

            public DateTime? Deadline { get; set; }
            public DateTime? CompletedDate { get; set; }

            public string Status { get; set; }


            public string PersonReport { get; set; }

            public string Description { get; set; }

            public int MUCUUTIEN { get; set; }
            public string TENMUCUUTIEN { get; set; }

            public string THIETBI { get; set; }
            public string TENTHIETBI { get; set; }

            public int LOAIYEUCAU { get; set; }
            public string TENLOAIYEUCAU { get; set; }

            public int NGUYENNHAN { get; set; }
            public string TENNGUYENNHAN { get; set; }
        }

        public List<StopCard2ViewModel> StopCard2 { get; set; }
        public class StopCard2ViewModel
        {
            public int ID { get; set; }
            public int StopCardID { get; set; }
            public string PermanentAction { get; set; }

            public string PersonIncharge { get; set; }
            public string TENPersonIncharge { get; set; }

            public DateTime? Deadline { get; set; }
            public DateTime? CompletedDate { get; set; }

            public string Status { get; set; }


            public string PersonReport { get; set; }

            public string Description { get; set; }

            public int MUCUUTIEN { get; set; }
            public string TENMUCUUTIEN { get; set; }

            public string THIETBI { get; set; }
            public string TENTHIETBI { get; set; }

            public int LOAIYEUCAU { get; set; }
            public string TENLOAIYEUCAU { get; set; }

            public int NGUYENNHAN { get; set; }
            public string TENNGUYENNHAN { get; set; }
        }
    }
}
