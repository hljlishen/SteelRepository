using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class MaterialCode
    {
        [Key]
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("编号")]
        public string Code { get; set; }

        [DisplayName("材料名称Id")]
        public int? materialNameId { get; set; }

        [DisplayName("材料规格Id")]
        public int? materialModelId { get; set; }
    }
}