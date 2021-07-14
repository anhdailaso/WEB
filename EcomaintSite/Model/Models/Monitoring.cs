
namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("GIAM_SAT_TINH_TRANG")]
    public partial class Monitoring
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Monitoring()
        {
            MonitoringOfQuantitative = new HashSet<MonitoringOfQuantitative>();
        }

        [Key]
        [Column("STT")]
        public int ID { get; set; }

        [Column("NGAY_KT")]
        public DateTime? DateChecked { get; set; }

        [Column("GIO_KT")]
        public DateTime? FromHour { get; set; }

        [Column("DEN_GIO")]
        public DateTime? ToHour { get; set; }

        [StringLength(9)]
        [Column("MS_CONG_NHAN")]
        public string StaffID { get; set; }

        [Column("GIO_CHAY_MAY")]
        public double? HourOfRuntime { get; set; }

        [StringLength(255)]
        [Column("NHAN_XET")]
        public string Comment { get; set; }

        [StringLength(255)]
        [Column("USERNAME")]
        public string Username { get; set; }


        [StringLength(20)]
        [Column("SO_PHIEU")]
        public string votes { get; set; }

        public Staff Staff { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<MonitoringOfQuantitative> MonitoringOfQuantitative { get; set; }
    }
}
