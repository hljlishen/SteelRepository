using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class RecheckReport
    {
        [Key]
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("复检时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd}", HtmlEncode = false, NullDisplayText = "数据无效")]
        public DateTime? recheckTime { get; set; }

        [DisplayName("批号Id")]
        public string batchId { get; set; }

        [DisplayName("复检报告图片")]
        public byte[] inspectionReport { get; set; }

    }
}