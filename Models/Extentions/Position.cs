using DbInterface;
using DbService;
using System.Collections.Generic;
using Tools.Statistics;

namespace Models
{
    public partial class Position
    {
        
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
        //public static List<InCome> GetInComes(int id)
        //{
        //    List<InCome> ret = new List<InCome>();
        //    using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
        //    {
        //        var inventories = helper.Select<Inventory>(p => p.positionId == id);
        //        foreach (var item in inventories)
        //        {
        //            ret.Add(helper.FindId<InCome>(item.incomeId));
        //        }
        //    }

        //    return ret;
        //}

        //public static List<InCome> GetInCome(int id)
        //{
        //    using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
        //    {
        //        List<InCome> ins = new List<InCome>();
        //        foreach (var income in GetInComes(id))
        //        {
        //            income.amount = WeightConverter.Convert(income.unit, income.amount, "kg");
        //            ins.Add(income);
        //        }
        //        return ins;
        //    }
        //}

        public static Dictionary<string, double> StatisticAmount(int positionId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                MultipleSeriesStatistics2D<Inventory> statistics2D = new MultipleSeriesStatistics2D<Inventory>(p => p.GetMaterialCode(helper));
                statistics2D.AddSeries("amount", p => p.amount);
                var Sum = statistics2D.GetValues(helper.Select<Inventory>(p => p.positionId == positionId && p.amount > 0));
                return Sum["amount"];
            }
        }
    }
}
