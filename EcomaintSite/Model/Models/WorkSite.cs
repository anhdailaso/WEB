namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHA_XUONG")]
    public partial class WorkSite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkSite()
        {
            WorkSite1 = new HashSet<WorkSite>();
        }

        [Key]
        [StringLength(50)]
        [Column("MS_N_XUONG")]
        public string ID { get; set; }

        //[StringLength(50)]
        //public string MS_CHA { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Ten_N_XUONG")]
        public string Name { get; set; }

        //[StringLength(100)]
        //public string TEN_N_XUONG_A { get; set; }

        //[StringLength(100)]
        //public string TEN_N_XUONG_H { get; set; }

        //[StringLength(100)]
        //public string TRU_SO { get; set; }

        //[StringLength(100)]
        //public string NGUOI_DAI_DIEN { get; set; }

        //[StringLength(20)]
        //public string Phone { get; set; }

        //[StringLength(19)]
        //public string MS_QG { get; set; }

        //[StringLength(30)]
        //public string MS_DUONG { get; set; }

        //[StringLength(100)]
        //public string Address { get; set; }

        //public double? DIEN_TICH { get; set; }

        //public double? KHOANG_CACH { get; set; }

        //[StringLength(200)]
        //public string Note { get; set; }

        //[StringLength(6)]
        //public string ID { get; set; }

        //[Column(TypeName = "image")]
        //public byte[] HINH_ANH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<WorkSite> WorkSite1 { get; set; }

        public  WorkSite WorkSite2 { get; set; }
    }
}
