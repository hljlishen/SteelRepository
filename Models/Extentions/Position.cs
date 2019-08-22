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
        public static string ValueGetName(string value)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<string> namestr = new List<string>();
                foreach (var Mater in MaterialCode.GetMaterialCodes(value))
                {
                    namestr.Add(helper.FindId<Name>(Mater.materialNameId).materialName);
                }
                return ToString(namestr);
            }
        }
        public static string ValueGetModel(string value)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<string> namestr = new List<string>();
                foreach (var Mater in MaterialCode.GetMaterialCodes(value))
                {
                    namestr.Add(helper.FindId<Model>(Mater.materialModelId).modelName);
                }
                return ToString(namestr);
            }
        }
        public static string ToString(List<string> list)
        {
            string Tostr = "";
            foreach (var str in list)
            {
                Tostr += str + " ";
            }
            return Tostr;
        }

        public static Dictionary<string, double> StatisticAmount(int positionId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<Inventory> ins = new List<Inventory>();
                MultipleSeriesStatistics2D<Inventory> statistics2D = new MultipleSeriesStatistics2D<Inventory>(p => p.GetMaterialCode(helper));
                statistics2D.AddSeries("amount", p => p.amount);
                foreach (var inven in helper.Select<Inventory>(p => p.positionId == positionId && p.amount > 0))
                {
                    inven.amount = WeightConverter.Convert(inven.unit, inven.amount, "kg");
                    ins.Add(inven);
                }
                var Sum = statistics2D.GetValues(ins);
                return Sum["amount"];
            }
        }
    }
}
