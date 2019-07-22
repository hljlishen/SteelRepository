using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class EnteringWarehouse
    {
        [Key]
        [DisplayName("Id")]
        public int Id { get; set; }

    }
}