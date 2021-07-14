namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHU_KY_HIEU_CHUAN")]
    public partial class CalibrationCycle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CalibrationCycle()
        { 
            MeasureDeviceCalibration = new HashSet<MeasureDeviceCalibration>();
        }

        [Key]
        [Column("MS_MAY", Order = 0)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("MS_PT", Order = 1)]
        [StringLength(25)]
        public string SparePartID { get; set; }

        [Key]
        [Column("MS_VI_TRI", Order = 2)]
        [StringLength(50)]
        public string LocationID { get; set; }

        [Column("CHU_KY_HC_NOI")]
        public short? InternalCycle { get; set; }

        [Column("CHU_KY_HC_NGOAI")]
        public short? ExternalCycle  { get; set; }

        [Column("MS_DV_TG")]
        public int? TimeUnitID { get; set; }

        [StringLength(250)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [StringLength(50)]
        [Column("TEN_VI_TRI")]
        public string LocationName { get; set; }

        [Column("CHU_KY_KD")]
        public short? VerifiableCycle { get; set; }

        [Column("CHU_KY_KT")]
        public short? CheckingCycle { get; set; }

        public UnitTime UnitTime { get; set; }

        public Device Device { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<MeasureDeviceCalibration> MeasureDeviceCalibration { get; set; }
    }
}
