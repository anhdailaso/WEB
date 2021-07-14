namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CAU_TRUC_THIET_BI_TS_GSTT")]
    public partial class ComponentInternalMonitoringParams
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ComponentInternalMonitoringParams()
        {
            MonitoringOfQuantitative = new HashSet<MonitoringOfQuantitative>();
        }

        [Key]
        [Column("MS_MAY", Order = 0)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("MS_TS_GSTT", Order = 1)]
        [StringLength(10)]
        public string MonitoringParamsID { get; set; }

        [Key]
        [Column("MS_BO_PHAN", Order = 2)]
        [StringLength(50)]
        public string ComponentID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(150)]
        [Column("TEN_GT")]
        public string ValueParams { get; set; }

        [Column("GIA_TRI_TREN")]
        public double? UpperValue { get; set; }

        [Column("GIA_TRI_DUOI")]    
        public double? LowerValue { get; set; }

        [Column("CHU_KY_DO")]
        public double? MeasureCycle { get; set; }

        [Column("MS_DV_TG")]
        public int? TimeUnitID { get; set; }

        [Column("DAT")]
        public bool? Pass { get; set; }

        [StringLength(250)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("ACTIVE")]
        public bool? Active { get; set; }

        [Column("THOI_GIAN")]
        public double? TimePlans { get; set; }

        [StringLength(2000)]
        [Column("CACH_THUC_HIEN")]
        public string Action { get; set; }

        [StringLength(2000)]
        [Column("TIEU_CHUAN_KT")]
        public string QualityCheck { get; set; }

        [StringLength(1000)]
        [Column("YEU_CAU_NS")]
        public string InquiryHR { get; set; }

        [StringLength(1000)]
        [Column("YEU_CAU_DUNG_CU")]
        public string Tool { get; set; }

        [StringLength(500)]
        [Column("PATH_HD")]
        public string Path { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<MonitoringOfQuantitative> MonitoringOfQuantitative { get; set; }
    }
}
