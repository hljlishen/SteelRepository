using DbInterface;
using DbService;
using System.Collections.Generic;
using Tools.Statistics;

namespace Models
{
    public partial class Position
    {
        private static List<InCome> ins = null;
        public static Position GetPosition(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Position>(id);
            }
        }
        public static int Insert(Position position)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Insert(position);
            }
        }
        public static int Update(Position position)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Update(position);
            }
        }
        public static List<Position> SelectAll()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.SelectAll<Position>();
            }
        }
        public static int Delete(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Delete<Position>(id);
            }
        }
        public static List<InCome> GetInComes(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var incomes = helper.Select<InCome>(p => p.positionId == id);
                foreach (var income in incomes)
                {
                    income.amount = WeightConverter.Convert(income.unit, income.amount, "kg");
                    ins.Add(income);
                }
                return ins;
            }
        }
        public static Dictionary<string, Dictionary<string, double>> StatisticAmount(int id)
        {
            MultipleSeriesStatistics2D<InCome> statistics2D = new MultipleSeriesStatistics2D<InCome>(p=> p.GetMaterialCode().code);
            statistics2D.AddSeries("amount",p=>p.amount);
            var Sum = statistics2D.GetValues(GetInComes(id));
            return Sum;
        }
    }
}
