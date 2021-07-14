namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TO")]
    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            Staff = new HashSet<Staff>();           
        }

        [Key]
        [Column("MS_TO1")]
        public int ID { get; set; }

        [StringLength(50)]
        [Column("TEN_TO")]
        public string Name { get; set; }

        [Column("MS_TO")]
        public int DepartmentID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Staff> Staff { get; set; }

        public virtual Department Department { get; set; }
    }
}
