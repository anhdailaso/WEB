namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEU_BAO_TRI_CONG_VIEC")]
    public partial class WorkOrderOfWork
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkOrderOfWork()
        {
           // PHIEU_BAO_TRI_CONG_VIEC_KE_HOACH = new HashSet<PHIEU_BAO_TRI_CONG_VIEC_KE_HOACH>();
            WorkOrderOfWorkIncludeStaff = new HashSet<WorkOrderOfWorkIncludeStaff>();
            WorkOrderOfWorkIncludeSparePart = new HashSet<WorkOrderOfWorkIncludeSparePart>();
        }

        [Key]
        [Column("MS_PHIEU_BAO_TRI", Order = 0)]
        [StringLength(20)]
        public string WorkOrderID { get; set; }

        [Key]
        [Column("MS_CV", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkID { get; set; }

        [Key]
        [Column("MS_BO_PHAN", Order = 2)]
        [StringLength(50)]
        public string ComponentID { get; set; }

        [Column("SO_GIO_KH")]
        public double? HourPlan { get; set; }

        [Column("HANG_MUC_ID")]
        public int? PlanID { get; set; }

        [StringLength(20)]
        [Column("EOR_ID")]
        public string EORID { get; set; }

        [StringLength(255)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("NGAY_HOAN_THANH")]
        public DateTime? DateCompleted { get; set; }

        [Column("STT_SERVICE")]
        public int? ServiceID { get; set; }

        [StringLength(30)]
        [Column("MS_MAY_TT")]
        public string AlternativeDeviceID { get; set; }

        [StringLength(50)]
        [Column("MS_BO_PHAN_TT")]
        public string AlternativeComponentID { get; set; }

        [StringLength(3500)]
        [Column("PHU_TUNG_TT")]
        public string AlternativeSparePartID { get; set; }

        [StringLength(50)]
        [Column("DINH_MUC_KH")]
        public string CostPerHour { get; set; }

        [Column("SO_GIO_PB")]
        public double? ActualTime  { get; set; }

        [Column("DINH_MUC_PB")]
        public double? ActualCostPerHour { get; set; }

        [StringLength(5)]
        [Column("NGOAI_TE")]
        public string Currency { get; set; }

        [Column("BAC_THO")]
        public int? SkillGradeID { get; set; }

        [StringLength(1000)]
        [Column("THUC_KIEM")]
        public string Actual { get; set; }

        [Column("H_THANH")]
        public bool? IsComplete { get; set; }

        [StringLength(1000)]
        [Column("DANH_GIA")]
        public string Evaluate { get; set; }

        [Column("PT_HOAN_THANH")]
        public double? PercentOfComplete { get; set; }

        [StringLength(2000)]
        [Column("THAO_TAC")]
        public string Action { get; set; }

        [StringLength(2000)]
        [Column("TIEU_CHUAN_KT")]
        public string QualityCheck { get; set; }

        [StringLength(250)]
        [Column("PATH_HD")]
        public string Path { get; set; }

        [Column("SO_NGUOI")]
        public int? AmountOfPeople { get; set; }

        [StringLength(1000)]
        [Column("YEU_CAU_NS")]
        public string InquiryHR { get; set; }

        [StringLength(1000)]
        [Column("YEU_CAU_DUNG_CU")]
        public string Tool { get; set; }

        [Column("NGAY_BDCV")]
        public DateTime? WorkStartDate { get; set; }

        [Column("NGAY_KTCV")]
        public DateTime? WorkEndDate { get; set; }

        public Work Work { get; set; }

        public WorkOrder WorkOrder { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<PHIEU_BAO_TRI_CONG_VIEC_KE_HOACH> PHIEU_BAO_TRI_CONG_VIEC_KE_HOACH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrderOfWorkIncludeStaff> WorkOrderOfWorkIncludeStaff { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrderOfWorkIncludeSparePart> WorkOrderOfWorkIncludeSparePart { get; set; }
    }
}
