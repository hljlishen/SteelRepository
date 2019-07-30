using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbInterface;
using DbService;
using Models;

namespace Models
{
    public partial class OutCome
    {
        public MonthChinese MonthCh { get; set; }
        public MonthEnglish MonthEn { get; set; }
        public Quarter Quarter { get; set; }
        public string Year { get; set; }
        public string Code { get; set; }

        public void PrepareStatistic()
        {
            MonthCh = (MonthChinese)(int.Parse(this.recipientsTime.ToString("MM")));
            MonthEn = (MonthEnglish)(int.Parse(this.recipientsTime.ToString("MM")));
            Quarter = CalQuarter(MonthCh);
            Year = recipientsTime.ToString("yyyy");
            Code = GetMaterialCode().code;
        }

        public static List<OutCome> GetOutComes(DateTime begin, DateTime end)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var outcomes = helper.Select<OutCome>(p => p.recipientsTime > begin && p.recipientsTime < end);
                return outcomes;
            }
        }

        private MaterialCode GetMaterialCode()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var income = helper.FindFirst<InCome, int>("id", incomeId);
                var mCode = helper.FindFirst<MaterialCode, int>("id", income.codeId);
                return mCode;
            }
        }

        private Quarter CalQuarter(MonthChinese month)
        {
            switch (month)
            {
                case MonthChinese.一月:
                    return Quarter.第一季度;
                case MonthChinese.二月:
                    return Quarter.第一季度;
                case MonthChinese.三月:
                    return Quarter.第一季度;
                case MonthChinese.四月:
                    return Quarter.第二季度;
                case MonthChinese.五月:
                    return Quarter.第二季度;
                case MonthChinese.六月:
                    return Quarter.第二季度;
                case MonthChinese.七月:
                    return Quarter.第三季度;
                case MonthChinese.八月:
                    return Quarter.第三季度;
                case MonthChinese.九月:
                    return Quarter.第三季度;
                case MonthChinese.十月:
                    return Quarter.第四季度;
                case MonthChinese.十一月:
                    return Quarter.第四季度;
                case MonthChinese.十二月:
                    return Quarter.第四季度;
                default:
                    throw new Exception($"错误的月份{month}");
            }
        }
    }
}
