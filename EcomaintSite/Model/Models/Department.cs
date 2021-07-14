namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("TO_PHONG_BAN")]
    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
            //DS_TL = new HashSet<DS_TL>();
            //Team = new HashSet<Team>();
            User = new HashSet<User>();
            //NHOMs = new HashSet<NHOM>();
        }
        [Key]
        [Column("MS_TO")]
        public int ID { get; set; }

        [StringLength(50)]
        [Column("TEN_TO")]
        public string Name { get; set; }

        [Required]
        [StringLength(6)]
        [Column("MS_DON_VI")]
        public string OrgUnitID { get; set; }

        [Column("STT_TO")]
        public int? Index { get; set; }

        [StringLength(70)]
        [Column("TO_TRUONG")]
        public string Leader { get; set; }

        [StringLength(50)]
        [Column("MS_TO_INT")]
        public string ID2 { get; set; }

        public OrgUnit OrgUnit { get; set; }

        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //    public ICollection<DS_TL> DS_TL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Team> Team { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> User { get; set; }

        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //    public ICollection<NHOM> NHOMs { get; set; }
        }

    }