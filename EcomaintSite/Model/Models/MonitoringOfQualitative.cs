
namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GIAM_SAT_TINH_TRANG_TS_DT")]
    public partial class MonitoringOfQualitative
    {
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

        [Key]
        [Column("STT_GT", Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ValueParamID { get; set; }

        [StringLength(9)]
        [Column("MS_CONG_NHAN")]
        public string StaffID { get; set; }

        [StringLength(50)]
        [Column("USERNAME")]
        public string Username { get; set; }

        [Column("TG_XU_LY")]
        public DateTime? ExecuteTime { get; set; }

        [StringLength(50)]
        [Column("MS_PBT")]
        public string WorkOrderID { get; set; }

        [Column("HANG_MUC_ID_KE_HOACH")]
        public int? PlanID { get; set; }

        [StringLength(15)]
        [Column("MS_CACH_TH")]
        public string ExecuteID { get; set; }

        [Column("STT_VAN_DE")] 
        public int? UserRequestDetailID { get; set; }

        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("TG_TT")]
        public double? RealityTime { get; set; }

        public MonitoringOfQuantitative MonitoringOfQuantitative { get; set; }
    }
}
