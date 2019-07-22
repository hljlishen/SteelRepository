using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class Manufacturers
    {
        [Key]
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("厂家名称")]
        public string manufacturersName { get; set; }

        [DisplayName("是否存在")]
        public int? exists { get; set; }
    }
}