//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InCome
    {
        public int id { get; set; }
        public Nullable<int> brandCodeId { get; set; }
        public int categoryId { get; set; }
        public int codeId { get; set; }
        public string batch { get; set; }
        public Nullable<int> menufactureId { get; set; }
        public string unit { get; set; }
        public double amount { get; set; }
        public Nullable<double> unitPrice { get; set; }
        public string priceMeasure { get; set; }
        public System.DateTime storageTime { get; set; }
        public int operatorId { get; set; }
        public double reviewCycle { get; set; }
    }
}
