namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CAU_TRUC_THIET_BI_PHU_TUNG")]
    public partial class ComponentOfSparePart
    {
        [Key]
        [Column("MS_MAY", Order = 0)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("MS_BO_PHAN", Order = 1)]
        [StringLength(50)]
        public string ComponentID { get; set; }

        [Key]
        [Column("MS_PT", Order = 2)]
        [StringLength(25)]
        public string SparePartID { get; set; }

        [Key]
        [Column("MS_VI_TRI_PT", Order = 3)]
        [StringLength(30)]
        public string LocationID { get; set; }

        [Column("SO_LUONG")]
        public double? Amount { get; set; }

        [Column("ACTIVE")]
        public bool? Active { get; set; }

        [Column("CHUC_NANG")]
        public string Action { get; set; }

        public Component Component { get; set; }
    }
}
