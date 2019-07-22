using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class Employee
    {
        [Key]
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("姓名")]
        public string name { get; set; }

        [DisplayName("编号")]
        public string number { get; set; }

        [DisplayName("密码")]
        public string password { get; set; }

        [DisplayName("权限")]
        public int? permissions { get; set; }

        [DisplayName("部门Id")]
        public int? departmentId { get; set; }

        [DisplayName("是否存在")]
        public int? exists { get; set; }
    }
}