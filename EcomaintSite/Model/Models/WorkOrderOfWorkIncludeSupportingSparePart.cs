namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEU_BAO_TRI_CV_PHU_TRO_PHU_TUNG")]
    public partial class WorkOrderOfWorkIncludeSupportingSparePart
    {
        [Key]
        [Column("MS_PHIEU_BAO_TRI", Order = 0)]
        [StringLength(20)]
        public string WorkOrderID { get; set; }

        [Key]
        [Column("STT", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupportingWorkID { get; set; }

        [Key]
        [Column("MS_PT", Order = 2)]
        [StringLength(25)]
        public string SparePartID { get; set; }

        [Column("SL_KH")]
        public double? PlanQuantity { get; set; }

        [Column("SL_TT")]
        public double? ActualQuantity { get; set; }

        [StringLength(255)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [StringLength(25)]
        [Column("MS_PT_TT")]
        public string AlternativeSparePartID { get; set; }

        public Sparepart Sparepart { get; set; }

        public WorkOrderOfSupportingWork WorkOrderOfSupportingWork { get; set; }
    }
}
