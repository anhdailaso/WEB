using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model.Data
{
    [Table("HE_THONG")]
    public partial class Formation
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Formation()
        {
            Formation1 = new HashSet<Formation>();
        }

        [Key]
        [Column("MS_HE_THONG")]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Column("TEN_HE_THONG")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Formation> Formation1 { get; set; }
    }
}
