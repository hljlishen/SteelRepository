using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Inventory
    {
        private static IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities());
        public static Inventory Insert(int incomeId, double amount, string unit, int positionId, IDbInterface dbInterface)
        {
            var inven = new Inventory() { amount = amount, incomeId = incomeId, unit = unit, positionId = positionId , consumptionAmount = amount};
            dbInterface.Insert(inven, false);
            return inven;
        }

        public string GetMaterialCode(IDbInterface db)
        {
            var income = db.FindId<InCome>(incomeId);
            var materialCode = db.FindId<MaterialCode>(income.codeId);
            return materialCode.code;
        }

        //public static Inventory GetInventories(int invenid)
        //{
        //    using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
        //    {
        //        return helper.FindId<Inventory>(invenid);
        //    }
        //}

        public static List<Inventory> InComeIdGetInventory(int incomeId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<Inventory>(p => p.incomeId == incomeId);
            }
        }
        public static List<Inventory> SelectAll()
        {
            return Dbhelper.SelectAll<Inventory>();
        }
        public static string GetMaterCodeName(int incomeId)
        {
            int  income = Dbhelper.FindId<InCome>(incomeId).codeId;
            return Dbhelper.FindId<MaterialCode>(income).code;
        }
    }
}
