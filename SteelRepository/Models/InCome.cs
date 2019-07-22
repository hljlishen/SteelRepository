using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class InCome
    {
        [Key]
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("类别Id")]
        public int? categoryId { get; set; }

        [DisplayName("编号Id")]
        public int? codeId { get; set; }

        [DisplayName("批号")]
        public string batchId { get; set; }

        [DisplayName("厂家Id")]
        public int? menufactureId { get; set; }

        [DisplayName("位置Id")]
        public int? positionId { get; set; }

        [DisplayName("单位")]
        public string unit { get; set; }

        [DisplayName("单价")]
        public double? unitPrice { get; set; }

        [DisplayName("数量")]
        public double? number { get; set; }

        [DisplayName("入库时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd}", HtmlEncode = false, NullDisplayText = "数据无效")]
        public DateTime? storageTime { get; set; }

        [DisplayName("质量证明图片")]
        public byte[] qualityCertificate { get; set; }
    }
}