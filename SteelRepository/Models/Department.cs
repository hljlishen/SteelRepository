using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class Department
    {
        [Key]
        [DisplayName("部门Id")]
        public int Id { get; set; }

        [DisplayName("部门名称")]
        public string departmentName { get; set; }
    }
}