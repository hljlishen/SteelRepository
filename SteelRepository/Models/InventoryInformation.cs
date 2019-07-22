using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class InventoryInformation
    {
        [Key]
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("批号")]
        public string batchNumber { get; set; }

        [DisplayName("存置")]
        public double? stock { get; set; }

        [DisplayName("单位")]
        public string unit { get; set; }

        [DisplayName("编号Id")]
        public int? numberId { get; set; }
    }
}