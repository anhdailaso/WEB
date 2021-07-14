namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LOAI_VT")]
    public partial class TypeOfMaterial
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeOfMaterial()
        {
            //DE_XUAT_MUA_HANG = new HashSet<DE_XUAT_MUA_HANG>();
            //DON_DAT_HANG = new HashSet<DON_DAT_HANG>();
            Sparepart = new HashSet<Sparepart>();
        }

        [Key]
        [StringLength(10)]
        [Column("MS_LOAI_VT")]
        public string ID { get; set; }

        [StringLength(50)]
        [Column("TEN_LOAI_VT_TV")]
        public string Name { get; set; }

        [StringLength(50)]
        [Column("TEN_LOAI_VT_TA")]
        public string NameOfEng { get; set; }

        [StringLength(50)]
        [Column("TEN_LOAI_VT_TH")]
        public string NameOfChina { get; set; }

        [Column("VAT_TU")]
        public bool IsMaterial { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DE_XUAT_MUA_HANG> DE_XUAT_MUA_HANG { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DON_DAT_HANG> DON_DAT_HANG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sparepart> Sparepart { get; set; }
    }
}
