namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LANGUAGES")]
    public partial class Languages
    {
        [Key]
        [Column("STT")]
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        [Column("MS_MODULE")]
        public string ModuleName { get; set; }

        [Required]
        [StringLength(255)]
        [Column("FORM")]
        public string Form { get; set; }

        [Required]
        [StringLength(255)]
        [Column("Keyword")]
        public string Keyword { get; set; }

        [StringLength(255)]
        [Column("VietNam")]
        public string VietNam { get; set; }

        [StringLength(255)]
        [Column("ENGLISH")]
        public string English { get; set; }

        [StringLength(255)]
        [Column("CHINESE")]
        public string Chinese { get; set; }

        [StringLength(10)]
        [Column("HANG_MUC")]
        public string Article { get; set; }

        [Column("FORM_HAY_REPORT")]
        public bool Type { get; set; }

        [StringLength(255)]
        [Column("VIETNAM_OR")]
        public string VietNamOld { get; set; }

        [StringLength(255)]
        [Column("ENGLISH_OR")]
        public string EnglishOld { get; set; }

        [StringLength(255)]
        [Column("CHINESE_OR")]
        public string ChineseOld { get; set; }
    }
}
