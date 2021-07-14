namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEU_BAO_TRI_CV_PHU_TRO")]
    public partial class WorkOrderOfSupportingWork
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkOrderOfSupportingWork()
        {
            WorkOrderOfWorkIncludeSuppotingStaff = new HashSet<WorkOrderOfWorkIncludeSuppotingStaff>();
            //PHIEU_BAO_TRI_CV_KH_PHU_TRO = new HashSet<PHIEU_BAO_TRI_CV_KH_PHU_TRO>();
            //PHIEU_BAO_TRI_CV_PHU_TRO_NHAN_SU = new HashSet<PHIEU_BAO_TRI_CV_PHU_TRO_NHAN_SU>();
            WorkOrderOfWorkIncludeSupportingSparePart = new HashSet<WorkOrderOfWorkIncludeSupportingSparePart>();
        }

        [Key]
        [Column("MS_PHIEU_BAO_TRI", Order = 0)]
        [StringLength(20)]
        public string WorkOrderID { get; set; }

        [Key]
        [Column("STT", Order = 1)]
        public int ID { get; set; }

        [StringLength(255)]
        [Column("TEN_CONG_VIEC")]
        public string WorkName { get; set; }

        [Column("SO_GIO_KH")]
        public double? HourPlan { get; set; }

        [Column("NGAY_HOAN_THANH")]
        public DateTime? DateCompleted { get; set; }

        [Column("NGAY_BDCV")]
        public DateTime? WorkStartDate { get; set; }

        [Column("NGAY_KTCV")]
        public DateTime? WorkEndDate { get; set; }

        [Column("STT_CV")]
        public int? WorkID { get; set; }

        public WorkOrder WorkOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrderOfWorkIncludeSuppotingStaff> WorkOrderOfWorkIncludeSuppotingStaff { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<PHIEU_BAO_TRI_CV_KH_PHU_TRO> PHIEU_BAO_TRI_CV_KH_PHU_TRO { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<PHIEU_BAO_TRI_CV_PHU_TRO_NHAN_SU> PHIEU_BAO_TRI_CV_PHU_TRO_NHAN_SU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrderOfWorkIncludeSupportingSparePart> WorkOrderOfWorkIncludeSupportingSparePart { get; set; }
    }
}
