using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model.Data
{
    public class ChooseListHazard
    {
        public int ID { get; set; }
        public string DocNum { get; set; }
        public string Description { get; set; }
        public string DocDate { get; set; }
        public string DocTime { get; set; }
        public string CreatedBy { get; set; }
        public string NGUOITH { get; set; }
        public string Status { get; set; }
        public string TTTH { get; set; }
        public string NLQVP { get; set; }
    }

    public class ChooseListStopCard
    {
        public int ID { get; set; }
        public string DocNum { get; set; }
        public string Description { get; set; }
        public string DocDate { get; set; }
        public string DocTime { get; set; }
        public string CreatedBy { get; set; }
        public string NGUOITH { get; set; }
        public string Status { get; set; }
        public string TTTH { get; set; }
        public string NLQVP { get; set; }
    }
}
