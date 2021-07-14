namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TINH_TRANG_PBT")]
    public partial class TheStatusOfWorkOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TheStatusOfWorkOrder()
        {
            WorkOrder = new HashSet<WorkOrder>();
            TheStatusDetailsOfWorkOrder = new HashSet<TheStatusDetailsOfWorkOrder>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("STT")]
        public int ID { get; set; }

        [StringLength(25)]
        [Column("TEN_TINH_TRANG")]
        public string Name { get; set; }

        [StringLength(25)]
        [Column("TEN_TINH_TRANG_ANH")]
        public string NameOfEng { get; set; }

        [StringLength(25)]
        [Column("TEN_TINH_TRANG_HOA")]
        public string NameOfChina { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrder> WorkOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<TheStatusDetailsOfWorkOrder> TheStatusDetailsOfWorkOrder { get; set; }
    }
}
