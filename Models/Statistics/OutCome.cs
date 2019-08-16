using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using Tools.Statistics;

namespace Models
{
    public partial class OutCome
    {
        private static IDbInterface dbInterface = new DbHelper(new SteelRepositoryDbEntities());
        public static List<OutCome> GetOutComes(DateTime begin, DateTime end)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var outcomes = helper.Select<OutCome>(p => p.recipientsTime > begin && p.recipientsTime < end);
                return outcomes;
            }
        }
        public static List<OutCome> GetOutComes(DateTime begin, DateTime end,int? alwaysid=null)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if (alwaysid == null)
                {
                    return GetOutComes(begin,end);
                }
                else
                {
                    var muloutcomes = helper.Select<OutCome>(p => (p.recipientsTime > begin && p.recipientsTime < end)
                    & (p.inventoryId == alwaysid || p.borrowerId == alwaysid || p.projectId == alwaysid));
                    return muloutcomes;
                }
            }
        }

        public static Dictionary<string, Dictionary<string, double>> StatisticsAmountByMonth(DateTime begin, DateTime end)
        {
            Func<OutCome, string> monthMapper = p =>
            {
                 return p.recipientsTime.ToString("yyyy年MM月");
            };
            Func<OutCome, double> amountMapper = p =>
            {
                return p.price ?? 0;
            };
            return Statistics(begin, end, monthMapper, "金额", amountMapper);
        }

        public static Dictionary<string, Dictionary<string, double>> StatisticsAmountByQuarter(DateTime begin, DateTime end)
        {
            Func<OutCome, string> quarterMapper = p =>
            {
                string year = p.recipientsTime.ToString("yyyy年");
                int month = p.recipientsTime.Month;

                if (month == 1 || month == 2 || month == 3)
                    return year + "1Q";
                if (month == 4 || month == 5 || month == 6)
                    return year + "2Q";
                if (month == 7 || month == 8 || month == 9)
                    return year + "3Q";
                if (month == 10 || month == 11 || month == 12)
                    return year + "4Q";

                throw new Exception("错误的月份");
            };

            Func<OutCome, double> amountMapper = p =>
            {
                return p.price ?? 0;
            };
            return Statistics(begin, end, quarterMapper, "金额", amountMapper);
        }

        public static Dictionary<string, Dictionary<string, double>> StatisticsAmountByYear(DateTime begin, DateTime end)
        {
            Func<OutCome, string> yearMapper = p =>
            {
                return p.recipientsTime.ToString("yyyy年");
            };

            Func<OutCome, double> amountMapper = p =>
            {
                return p.price ?? 0;
            };
            return Statistics(begin, end, yearMapper, "金额", amountMapper);
        }

        public static Dictionary<string, Dictionary<string, double>> StatisticsAmountByCode(DateTime begin, DateTime end)
        {
            Func<OutCome, string> codeMapper = p =>
            {
                var inventory = dbInterface.FindFirst<Inventory, int>("id", p.inventoryId);
                var income = dbInterface.FindFirst<InCome, int>("id", inventory.incomeId);
                var mCode = dbInterface.FindFirst<MaterialCode, int>("id", income.codeId);
                return mCode.code;
            };

            Func<OutCome, double> amountMapper = p =>
            {
                return p.price ?? 0;
            };
            return Statistics(begin, end, codeMapper, "金额", amountMapper);
        }

        private static Dictionary<string, Dictionary<string, double>> Statistics(DateTime begin, DateTime end, Func<OutCome, string> xAxisMapper, string seriesName, Func<OutCome, double> yAxisMapper)
        {
            var outcomeList = GetOutComes(begin, end);
            MultipleSeriesStatistics2D<OutCome> statistics = new MultipleSeriesStatistics2D<OutCome>(xAxisMapper);
            statistics.AddSeries(seriesName, yAxisMapper);
            return statistics.GetValues(outcomeList);
        }
    }
}
