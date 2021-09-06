namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Leadership")]
    public partial class Leadership
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Leadership()
        {
            LeadershipDetails = new HashSet<LeadershipDetail>();
        }

        public int ID { get; set; }

        public string Content { get; set; }

        [StringLength(30)]
        public string UserCreate { get; set; }

        public DateTime? DateCreate { get; set; }

        [StringLength(30)]
        public string UserEdits { get; set; }

        public DateTime? DateEdits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeadershipDetail> LeadershipDetails { get; set; }
    }
}
