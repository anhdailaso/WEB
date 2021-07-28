
namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GIAM_SAT_TINH_TRANG_TS")]
    public partial class MonitoringOfQuantitative
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MonitoringOfQuantitative()
        {
            MonitoringDocument = new HashSet<MonitoringDocument>();
            MonitoringOfQualitative = new HashSet<MonitoringOfQualitative>();
        }

        [Key]
        [Column("STT", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MonitoringID { get; set; }

        [Key]
        [Column("MS_MAY", Order = 1)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("MS_TS_GSTT", Order = 2)]
        [StringLength(10)]
        public string MonitoringParamsID { get; set; }

        [Key]
        [Column("MS_BO_PHAN", Order = 3)]
        [StringLength(50)]
        public string ComponentID { get; set; }

        [Key]
        [Column("MS_TT", Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Column("GIA_TRI_DO")]
        public double? Measurement { get; set; }

        [StringLength(15)]
        [Column("MS_CACH_TH")]
        public string ExecuteID { get; set; }

        [StringLength(9)]
        [Column("MS_CONG_NHAN")]
        public string StaffID { get; set; }

        [Column("TG_XU_LY")]
        public DateTime? ExecuteTime { get; set; }

        [StringLength(50)]
        [Column("MS_PBT")]
        public string WorkOrderID { get; set; }

        [Column("HANG_MUC_ID_KE_HOACH")]
        public int? PlanID { get; set; }

        [StringLength(50)]
        [Column("USERNAME")]
        public string Username { get; set; }

        [Column("STT_VAN_DE")]
        public int? UserRequestDetailID { get; set; }

        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("TG_TT")]
        public double? RealityTime { get; set; }

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
        public string PATH_HD { get; set; }

        [Column("ImageGS")]
        public Byte[] ImageGS { get; set; }

        public ComponentInternalMonitoringParams ComponentInternalMonitoringParams { get; set; }
        public Monitoring Monitoring { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<MonitoringDocument> MonitoringDocument { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<MonitoringOfQualitative> MonitoringOfQualitative { get; set; }
    }
}
