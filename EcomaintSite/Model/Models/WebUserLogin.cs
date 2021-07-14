using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model.Data
{
    [Table("WebUserLogin")]
    public partial class WebUserLogin
    {
        [Key]
        public string Username { get; set; }
        public DateTime TimeLogin { get; set; }
    }
}
