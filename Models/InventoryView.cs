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
    
    public partial class InventoryView
    {
        public System.DateTime storageTime { get; set; }
        public string batch { get; set; }
        public string unit { get; set; }
        public double amount { get; set; }
        public int InvenId { get; set; }
        public double consumptionAmount { get; set; }
        public string categoryName { get; set; }
        public string code { get; set; }
        public string manufacturersName { get; set; }
        public int manuId { get; set; }
        public string materialName { get; set; }
        public string modelName { get; set; }
        public string positionName { get; set; }
        public int posiId { get; set; }
        public string name { get; set; }
    }
}
