namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("CAU_TRUC_THIET_BI")]
    public partial class Component
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Component()
        {
            ComponentOfSparePart = new HashSet<ComponentOfSparePart>();
            ComponentOfWork = new HashSet<ComponentOfWork>();
            //THONG_SO_BO_PHAN = new HashSet<THONG_SO_BO_PHAN>();
        }

        [Key]
        [Column("MS_MAY", Order = 0)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("MS_BO_PHAN", Order = 1)]
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(250)]
        [Column("TEN_BO_PHAN")]
        public string Name { get; set; }

        [StringLength(25)]
        [Column("MS_PT")]
        public string SparePartID { get; set; }

        [Column("SO_LUONG")]
        public int Amount { get; set; }

        [StringLength(50)]
        [Column("MS_BO_PHAN_CHA")]
        public string RootID { get; set; }

        [StringLength(250)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("RUN_TIME")]
        public int? Runtime { get; set; }

        [Column("MS_DVT_RT")]
        public int? UnitOfRuntimeID { get; set; }

        [StringLength(250)]
        [Column("HINH")]
        public string Image { get; set; }

        [StringLength(50)]
        [Column("CLASS_ID")]
        public string ClassID { get; set; }

        [Column("STT")]
        public int? Index { get; set; }

        [StringLength(250)]
        [Column("TEN_BO_PHAN_ANH")]
        public string NameOfEng { get; set; }

        [StringLength(250)]
        [Column("TEN_BO_PHAN_HOA")]
        public string NameOfChina { get; set; }

        //public  CLASS_LIST CLASS_LIST { get; set; }
       
        public UnitOfRuntime UnitOfRuntime { get; set; }

        public  Device Device { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ComponentOfSparePart> ComponentOfSparePart { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ComponentOfWork> ComponentOfWork { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<THONG_SO_BO_PHAN> THONG_SO_BO_PHAN { get; set; }
    }
}
