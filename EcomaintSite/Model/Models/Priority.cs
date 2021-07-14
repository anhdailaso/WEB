namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MUC_UU_TIEN")]
    public partial class Priority
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Priority()
        {
        }

        [Key]
        [Column("MS_UU_TIEN")]
        public int ID { get; set; }
        
        [Required]
        [StringLength(100)]
        [Column("TEN_UU_TIEN")]
        public string Name { get; set; }
        
        [StringLength(100)]
        [Column("TEN_TA")]
        public string NameOfEng { get; set; }

        [Column("SO_NGAY_PHAI_BD")]
        public int? NumberOfStartDate { get; set; }

        [Column("SO_NGAY_PHAI_KT")]
        public int? NumberOfEndDate { get; set; }
    }
}
