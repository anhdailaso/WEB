namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ST_APPROVAL_USER
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string FORM_ID { get; set; }

        [StringLength(200)]
        public string APPROVAL_LEVEL1 { get; set; }

        [StringLength(200)]
        public string APPROVAL_LEVEL2 { get; set; }
    }
}
