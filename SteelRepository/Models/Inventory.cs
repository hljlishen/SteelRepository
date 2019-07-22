using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class Inventory
    {
        [Key]
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("批号Id")]
        public string batchId { get; set; }

        [DisplayName("存量")]
        public double? stock { get; set; }

        [DisplayName("单位")]
        public string unit { get; set; }

        [DisplayName("编号Id")]
        public int? numberId { get; set; }
    }
}