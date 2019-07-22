using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class OutCome
    {
        [Key]
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("名称")]
        public string name { get; set; }

        [DisplayName("规格")]
        public string specifications { get; set; }

        [DisplayName("批号Id")]
        public string batchId { get; set; }

        [DisplayName("数量")]
        public double? number { get; set; }

        [DisplayName("单位")]
        public string unit { get; set; }

        [DisplayName("金额")]
        public double? money { get; set; }

        [DisplayName("领用时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd}", HtmlEncode = false, NullDisplayText = "数据无效")]
        public DateTime? recipientsTime { get; set; }

        [DisplayName("领用人Id")]
        public int? borrowerId { get; set; }

        [DisplayName("价格")]
        public double? price { get; set; }

        [DisplayName("说明")]
        public string instructions { get; set; }

        [DisplayName("题目令号Id")]
        public int? projectId { get; set; }
    }
}