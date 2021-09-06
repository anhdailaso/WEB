namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LeadershipDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDLeadership { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime DateCreate { get; set; }

        public bool? Yes { get; set; }

        public bool? No { get; set; }

        public bool? NA { get; set; }

        public virtual Leadership Leadership { get; set; }
    }
}
