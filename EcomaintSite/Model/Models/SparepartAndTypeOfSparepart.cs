namespace Model.Data
{ 
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IC_PHU_TUNG_LOAI_PHU_TUNG")]
    public partial class SparepartAndTypeOfSparepart
    {
        [Key]
        [Column("MS_PT", Order = 0)]
        [StringLength(25)]
        public string SparePartID { get; set; }

        [Key]
        [Column("MS_LOAI_PT", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeOfSparepart { get; set; }
    }
}
