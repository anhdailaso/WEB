namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YEU_CAU_NSD_CHI_TIET")]
    public partial class UserRequestDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserRequestDetail()
        {
            UserRequestComponent = new HashSet<UserRequestComponent>();
            UserRequestDocument = new HashSet<UserRequestDocument>();
        }

        [Key]
        [Column("STT", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserRequestID { get; set; }

        [Key]
        [Column("MS_MAY", Order = 1)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("STT_VAN_DE", Order = 2)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("MO_TA_TINH_TRANG")]
        public string Description { get; set; }

        [Column("YEU_CAU")]
        public string Request { get; set; }

        [StringLength(500)]
        [Column("NOI_DUNG_XAC_NHAN")]
        public string ConfirmResult { get; set; }
        
        [StringLength(9)]
        [Column("NGUOI_XAC_NHAN")]
        public string ConfirmedBy { get; set; }

        [StringLength(15)]
        [Column("MS_CACH_TH")]
        public string ExecuteID { get; set; }

        [StringLength(20)]
        [Column("MS_PBT")]
        public string WorkOrderID { get; set; }

        [Column("HANG_MUC_ID_KE_HOACH")]
        public int? PlanID { get; set; }

        [StringLength(9)]
        [Column("MS_CONG_NHAN")]
        public string StaffID { get; set; }

        [Column("NGAY_XU_LY")]
        public DateTime? DateRepaired { get; set; }

        [Column("GIO_XU_LY")]
        public DateTime? HourRepaired { get; set; }

        [StringLength(50)]
        [Column("USERNAME")]
        public string Username { get; set; }

        [Column("MS_UU_TIEN")]
        public int? PriorityID { get; set; }

        [StringLength(50)]
        [Column("USERNAME_DSX")]
        public string ApprovedRequestBy { get; set; }

        [Column("THOI_GIAN_DSX")]
        public DateTime? DateApproveRequested { get; set; }

        [StringLength(250)]
        [Column("Y_KIEN_DSX")]
        public string ApprovalNote { get; set; }

        [Column("THUC_HIEN_DSX")]
        public bool? IsExecuteApproveRequested { get; set; }

        [StringLength(250)]
        [Column("EMAIL_DSX")]
        public string EmailByApproveRequested { get; set; }

        [StringLength(50)]
        [Column("USERNAME_DBT")]
        public string ApprovedMaintenanceBy { get; set; }

        [Column("NGAY_DBT")]
        public DateTime? DateConfirmed { get; set; }

        [StringLength(250)]
        [Column("EMAIL_DBT")]
        public string MaintenanceNote { get; set; }

        [StringLength(250)]
        [Column("Y_KIEN_DBT")]
        public string EmailByMaintenance { get; set; }

        [Column("NGAY_XAY_RA")]
        public DateTime? DateOccurred { get; set; }

        [Column("GIO_XAY_RA")]
        public DateTime? HourOccurred { get; set; }

        [Column("MS_LOAI_YEU_CAU_BT")]
        public int? TypeOfMaintenanceRequestID { get; set; }

        [Column("MS_NGUYEN_NHAN")]
        public int? CauseID { get; set; }

        [StringLength(300)]
        [Column("REF_TYPE")]
        public string REFType { get; set; }

        [StringLength(300)]
        [Column("REF_NO")]
        public string REFNo { get; set; }

        [Column("REF_ID")]
        public int? REFID { get; set; }

        [StringLength(300)]
        [Column("DETAIL_TYPE")]
        public string DetailType { get; set; }

        [Column("LINE_ID")]
        public int? LineID { get; set; }

        public  Device Device { get; set; }

        public  User User { get; set; }

        public  UserRequest UserRequest { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<UserRequestComponent> UserRequestComponent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<UserRequestDocument> UserRequestDocument { get; set; }
    }
}
