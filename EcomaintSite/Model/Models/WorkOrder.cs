namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEU_BAO_TRI")]
    public partial class WorkOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkOrder()
        {
            WorkOrderOfSupportingWork = new HashSet<WorkOrderOfSupportingWork>();         
            WorkOrderOfWork = new HashSet<WorkOrderOfWork>();         
            WorkOrderDetailsByTheState = new HashSet<WorkOrderDetailsByTheState>();
            WorkOrderByTheStatus = new HashSet<WorkOrderByTheStatus>();
            //PHIEU_BAO_TRI_DI_CHUYEN_BP = new HashSet<PHIEU_BAO_TRI_DI_CHUYEN_BP>();
            //PHIEU_BAO_TRI_GIAM_SAT_NGHIEM_THU = new HashSet<PHIEU_BAO_TRI_GIAM_SAT_NGHIEM_THU>();
            //PHIEU_BAO_TRI_HINH = new HashSet<PHIEU_BAO_TRI_HINH>();
            //PHIEU_BAO_TRI_PHAN_HOI_THONG_TIN = new HashSet<PHIEU_BAO_TRI_PHAN_HOI_THONG_TIN>();
            //PHIEU_BAO_TRI_SERVICE = new HashSet<PHIEU_BAO_TRI_SERVICE>();
            //PHIEU_BAO_TRI_CLASS = new HashSet<PHIEU_BAO_TRI_CLASS>();
        }

        [Key]
        [StringLength(20)]
        [Column("MS_PHIEU_BAO_TRI")]
        public string ID { get; set; }

        [Column("TINH_TRANG_PBT")]
        public int? TheStatusOfWorkOrderID { get; set; }

        [StringLength(30)]
        [Column("MS_MAY")]
        public string DeviceID { get; set; }

        [Column("MS_LOAI_BT")]
        public int? TypeOfMaintenanceID { get; set; }

        [Column("NGAY_LAP")]
        public DateTime? DateCreated { get; set; }

        [Column("GIO_LAP")]
        public DateTime? TimeCreated { get; set; }

        [StringLength(500)]
        [Column("LY_DO_BT")]
        public string Reason { get; set; }

        [Column("MS_UU_TIEN")]
        public int? PriorityID { get; set; }

        [Column("NGAY_BD_KH")]
        public DateTime? StartDatePlan { get; set; }

        [Column("NGAY_KT_KH")]
        public DateTime? EndDatePlan { get; set; }

        [StringLength(9)]
        [Column("NGUOI_LAP")]
        public string CreatedBy { get; set; }

        [StringLength(50)]
        [Column("USERNAME_NGUOI_LAP")]
        public string UserCreated { get; set; }

        [StringLength(9)]
        [Column("NGUOI_GIAM_SAT")]
        public string MonitoredBy { get; set; }

        [Column("NGAY_HONG")]
        public DateTime? BreakdownStartTime { get; set; }

        [Column("GIO_HONG")]
        public DateTime? BreakdownStartDate { get; set; }

        [StringLength(200)]
        [Column("TT_SAU_BT")]
        public string StateAfterMaintenance { get; set; }

        [Column("NGAY_NGHIEM_THU")]
        public DateTime? DateAccepted { get; set; }

        [StringLength(9)]
        [Column("NGUOI_NGHIEM_THU")]
        public string AcceptedBy { get; set; }

        [Column("NGAY_BTPN")]
        public DateTime? DueDate { get; set; }

        [StringLength(50)]
        [Column("USERNAME_NGUOI_NT")]
        public string UserAccepted { get; set; }

        [StringLength(128)]
        [Column("SO_PHIEU_BAO_TRI")]
        public string WorkOrderNo { get; set; }

        [Column("MS_NGUYEN_NHAN")]
        public int? CauseID { get; set; }

        [Column("DEN_GIO_HONG")]
        public DateTime? BreakdownEndTime { get; set; }

        [Column("DEN_NGAY_HONG")]
        public DateTime? BreakdownEndDate { get; set; }

        [StringLength(4000)]
        [Column("DHX")]
        public string IssusedID { get; set; }

        [Column("SO_GIO_LUY_KE")]
        public double? MetterValues { get; set; }

        [Column("MS_TT_CT")]
        public int? TheStatusDetailsOfWorkOrderID { get; set; }

        [Column("UPDATE_NGAY_CUOI")]
        public DateTime? LastDateModified { get; set; }

        [Column("STT_CA")]
        public int? ShiftID { get; set; }

        [StringLength(250)]
        [Column("TT_SAU_BT_ANH")]
        public string StateAfterMaintenanceOfEng { get; set; }

        [StringLength(250)]
        [Column("TT_SAU_BT_HOA")]
        public string StateAfterMaintenanceOfChina { get; set; }

        [Column("NGAY_BTDK_GOC")]
        public DateTime? OriginalStartDate { get; set; }

        public Staff Staff { get; set; }

        public TypeOfMaintenance TypeOfMaintenance { get; set; }

        public Device Device { get; set; }

        public Priority Priority { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrderOfSupportingWork> WorkOrderOfSupportingWork { get; set; }
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrderDetailsByTheState> WorkOrderDetailsByTheState { get; set; }

        public TheStatusOfWorkOrder TheStatusOfWorkOrder1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrderByTheStatus> WorkOrderByTheStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrderOfWork> WorkOrderOfWork { get; set; }
        //public PHIEU_BAO_TRI_CHI_PHI PHIEU_BAO_TRI_CHI_PHI { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<PHIEU_BAO_TRI_CLASS> PHIEU_BAO_TRI_CLASS { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<PHIEU_BAO_TRI_DI_CHUYEN_BP> PHIEU_BAO_TRI_DI_CHUYEN_BP { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<PHIEU_BAO_TRI_GIAM_SAT_NGHIEM_THU> PHIEU_BAO_TRI_GIAM_SAT_NGHIEM_THU { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<PHIEU_BAO_TRI_HINH> PHIEU_BAO_TRI_HINH { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<PHIEU_BAO_TRI_PHAN_HOI_THONG_TIN> PHIEU_BAO_TRI_PHAN_HOI_THONG_TIN { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<PHIEU_BAO_TRI_SERVICE> PHIEU_BAO_TRI_SERVICE { get; set; }

    }
}
