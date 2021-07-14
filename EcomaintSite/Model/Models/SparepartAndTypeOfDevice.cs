namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IC_PHU_TUNG_LOAI_MAY")]
    public partial class SparepartAndTypeOfDevice
    {
        [Key]
        [Column("MS_PT", Order = 0)]
        [StringLength(25)]
        public string SparePartID { get; set; }

        [Key]
        [Column("MS_LOAI_MAY", Order = 1)]
        [StringLength(20)]
        public string TypeOfDeviceID { get; set; }
    }
}
