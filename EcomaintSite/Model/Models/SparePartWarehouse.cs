namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IC_PHU_TUNG_KHO")]
    public partial class SparePartWarehouse
    {
        [Key]
        [Column("MS_PT", Order = 0)]
        [StringLength(25)]
        public string SparePartID { get; set; }

        [Key]
        [Column("MS_KHO", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WarehouseID { get; set; }

        [Column("MS_VI_TRI")]
        public int? LocationID { get; set; }

        [Column("TON_TOI_THIEU")]
        public double? MinInventory { get; set; }

        [Column("TON_KHO_MAX")]
        public double? MaxInventory { get; set; }

        [StringLength(100)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("SO_NGAY_DAT_MUA_HANG")]
        public int? Leadtime { get; set; }

        public virtual Sparepart Sparepart { get; set; }
    }
}
