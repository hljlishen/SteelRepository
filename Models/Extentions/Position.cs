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
        public static List<InCome> GetInComes(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<InCome>(p => p.positionId == id);
            }
        }

        public static List<Inventory> GetInventories(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                //List<Inventory> ins = new List<Inventory>();
                //foreach (var income in GetInComes(id))
                //{
                //    var inventorys = helper.Select<Inventory>(p => p.incomeId == income.id);
                //    foreach (var inventory in inventorys)
                //    {
                //        inventory.amount = WeightConverter.Convert(inventory.unit, inventory.amount, "kg");
                //        ins.Add(inventory);
                //    }
                //}
                //return ins;   

                List<InCome> inComes = GetInComes(id);
                List<Inventory> inventories = new List<Inventory>();
                foreach (var income in inComes)
                {
                    inventories.Add(Inventory.GetInventories(income.id));
                }
                return inventories;
            }
        }
        public static List<InCome> GetInCome(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<InCome> ins = new List<InCome>();
                foreach (var income in GetInComes(id))
                {
                    income.amount = WeightConverter.Convert(income.unit, income.amount, "kg");
                    ins.Add(income);
                }
                return ins;
            }
        }

        public static Dictionary<string, double> StatisticAmount(int id)
        {
            //Dictionary<string, Dictionary<string, double>> Sum = new Dictionary<string, Dictionary<string, double>>();
            MultipleSeriesStatistics2D<Inventory> statistics2D = new MultipleSeriesStatistics2D<Inventory>(p => Inventory.GetMaterialCodeCode(id).code);
            statistics2D.AddSeries("amount", p => p.amount);
            var Sum = statistics2D.GetValues(GetInventories(id));
            return Sum["amount"];
            //MultipleSeriesStatistics2D<InCome> statistics2D = new MultipleSeriesStatistics2D<InCome>(p => p.GetMaterialCode().code);
            //statistics2D.AddSeries("amount", p => p.amount);
            //var Sum = statistics2D.GetValues(GetInCome(id));
            //return Sum["amount"];

            //MultipleSeriesStatistics2D<Inventory> statistics2D = new MultipleSeriesStatistics2D<Inventory>(p => p.GetMaterialCode(id).code);
            //statistics2D.AddSeries("amount", p => p.amount);
            //var Sum = statistics2D.GetValues(GetInventories(id));
            //return Sum["amount"];
        }
    }
}
