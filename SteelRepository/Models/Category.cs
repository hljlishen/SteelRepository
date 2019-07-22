using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class Category
    {
        [Key]
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("材料类别")]
        public string materialCategories { get; set; }
    }
}