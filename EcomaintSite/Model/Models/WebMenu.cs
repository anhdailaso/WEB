using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model.Data
{
    [Table("WebMenu")]
    public partial class WebMenu
    {
        [Key]
        public int ID { get; set; }
        public string MenuID { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public int? Root { get; set; }
        public int Index { get; set; }
        public bool Hide { get; set; }
        public string MetaTitle { get; set; }
        public string Icons { get; set; }

    }
}
