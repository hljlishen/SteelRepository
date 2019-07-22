using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class Name
    {
        [Key]
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("材料名称")]
        public string materialName { get; set; }
    }
}