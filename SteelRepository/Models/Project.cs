using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class Project
    {
        [Key]
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("题目令号")]
        public string projectNumber { get; set; }

        [DisplayName("题目名称")]
        public string projectName { get; set; }

        [DisplayName("是否存在")]
        public int? exists { get; set; }
    }
}