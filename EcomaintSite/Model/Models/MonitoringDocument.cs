
namespace Model.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("GIAM_SAT_TINH_TRANG_HINH")]
    public partial class MonitoringDocument
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
        [Column("STT_HINH", Order = 5)]
        public int ImageID { get; set; }

        [StringLength(255)]
        [Column("DUONG_DAN")]
        public string Path { get; set; }

        [StringLength(50)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        public  MonitoringOfQuantitative MonitoringOfQuantitative { get; set; }
    }
}
