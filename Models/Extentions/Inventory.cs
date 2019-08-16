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
        public static string GetNameName(int incomeId)
        {
            var nameId = Dbhelper.FindId<MaterialCode>(Dbhelper.FindId<InCome>(incomeId).codeId).materialNameId;
            return Dbhelper.FindId<Name>(nameId).materialName;
        }
        public static string GetModelName(int incomeId)
        {
            var nameId = Dbhelper.FindId<MaterialCode>(Dbhelper.FindId<InCome>(incomeId).codeId).materialNameId;
            return Dbhelper.FindId<Model>(nameId).modelName;
        }
        public static double GetInComeAmount( int incomeId)
        {
            return Dbhelper.FindId<InCome>(incomeId).amount;
        }
        public static double GetOutComeNumber(int InventoryId)
        {
            double sum = 0;
            foreach ( var number in Dbhelper.Select<OutCome>(p =>p.inventoryId == InventoryId))
            {
                sum += number.number;
            }
            return sum;
        }
        public static string GetInComeUnit(int incomeId)
        {
            return Dbhelper.FindId<InCome>(incomeId).unit;
        }
        public static string GetPositionName( int Positionid)
        {
            return Dbhelper.FindId<Position>(Positionid).positionName;
        }
        public static DateTime GetInComeTime(int incomeId)
        {
            return Dbhelper.FindId<InCome>(incomeId).storageTime;
        }
        public static double GetSurpius( int Incomeid,int Inventoryid)
        {
            double dif= GetInComeAmount(Incomeid) - GetOutComeNumber(Inventoryid);
            if (dif > 0)
            {
                return dif;
            }
            return 0;
        }
        public static List<double> GetInComeAmounts()
        {
            List<double> doulist = new List<double>();
            foreach (var incom in InCome.GetInComes())
            {
                doulist.Add(incom.amount);
            }
            return doulist;
        }

    }
}
