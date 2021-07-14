namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("PHIEU_BAO_TRI_CONG_VIEC_NHAN_SU_CHI_TIET")]
    public partial class WorkOrderOfWorkIncludeDetailStaff
    {
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

        [Key]
        [Column("MS_CONG_NHAN", Order = 3)]
        [StringLength(9)]
        public string StaffID { get; set; }

        [Column("NGAY")]
        public DateTime? StartDate { get; set; }

        [Column("TU_GIO")]
        public DateTime? StartHour { get; set; }

        [Column("DEN_NGAY")]
        public DateTime? EndDate { get; set; }

        [Column("DEN_GIO")]
        public DateTime? EndHour { get; set; }

        [Column("HOAN_THANH")]
        public bool IsComplete { get; set; }

        [Key]
        [Column("STT", Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Column("SO_GIO")]
        public double? TotalHour { get; set; }

        [Column("NGAY_KH")]
        public DateTime? StartDatePlan { get; set; }

        [Column("TU_GIO_KH")]
        public DateTime? StartHourPlan { get; set; }

        [Column("DEN_NGAY_KH")]
        public DateTime? EndDatePlan { get; set; }

        [Column("DEN_GIO_KH")]
        public DateTime? EndHourPlan { get; set; }

        [Column("TONG_GIO_KH")]
        public double? TotalHourPlan { get; set; }

        public WorkOrderOfWorkIncludeStaff WorkOrderOfWorkIncludeStaff { get; set; }
    }
}
