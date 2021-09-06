namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ST_Safety
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string Staffname { get; set; }

        public int? Department { get; set; }

        [StringLength(200)]
        public string Position { get; set; }

        [StringLength(200)]
        public string Userlogin { get; set; }

        public string Description { get; set; }
    }
}
