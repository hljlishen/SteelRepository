using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using Tools.Statistics;

namespace Models
{
    public partial class OutCome
    {
        public MonthChinese MonthCh { get; set; }
        public string FullMonth { get; set; }
        public MonthEnglish MonthEn { get; set; }
        public Quarter Quarter { get; set; }
        public string FullQuarter { get; set; }
        public string Year { get; set; }
        public string Code { get; set; }

        private void PrepareStatistic()
        {
            Year = recipientsTime.ToString("yyyy");
            MonthCh = (MonthChinese)(int.Parse(this.recipientsTime.ToString("MM")));
            FullMonth = $"{Year}年{MonthCh.ToString()}";
            MonthEn = (MonthEnglish)(int.Parse(this.recipientsTime.ToString("MM")));
            Quarter = CalQuarter(MonthCh);
            FullQuarter = $"{Year}年{Quarter.ToString()}";
            Code = GetMaterialCode().code;
        }

        private static void PrepareStatistic(IEnumerable<OutCome> outcomes)
        {
            foreach (var item in outcomes)
            {
                item.PrepareStatistic();
            }
        }
        public static List<OutCome> GetOutComes(DateTime begin, DateTime end)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var outcomes = helper.Select<OutCome>(p => p.recipientsTime > begin && p.recipientsTime < end);
                PrepareStatistic(outcomes);
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

        public static Dictionary<string, Dictionary<string, double>> StatisticsAmountByMonth(DateTime begin, DateTime end)
        {
            return Statistics(begin, end, "FullMonth", "price");
        }

        public static Dictionary<string, Dictionary<string, double>> StatisticsAmountByQuarter(DateTime begin, DateTime end)
        {
            return Statistics(begin, end, "FullQuarter", "price");
        }

        public static Dictionary<string, Dictionary<string, double>> StatisticsAmountByYear(DateTime begin, DateTime end)
        {
            return Statistics(begin, end, "Year", "price");
        }

        public static Dictionary<string, Dictionary<string, double>> StatisticsAmountByCode(DateTime begin, DateTime end)
        {
            return Statistics(begin, end, "Code", "price");
        }

        private static Dictionary<string, Dictionary<string, double>> Statistics(DateTime begin, DateTime end, string xAxisName, string yAxisName)
        {
            var outcomeList = GetOutComes(begin, end);
            MultipleSeriesStatistics2D<OutCome> statistics = new MultipleSeriesStatistics2D<OutCome>(xAxisName);
            statistics.AddSeries(yAxisName);
            return statistics.GetValues(outcomeList);
        }
    }
}
