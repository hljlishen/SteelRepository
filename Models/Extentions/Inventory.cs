using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using Tools.Statistics;

namespace Models
{
    public partial class Inventory
    {
        public static Inventory Insert(int incomeId, double amount, string unit, int positionId, IDbInterface dbInterface)
        {
            var inven = new Inventory() { amount = amount, incomeId = incomeId, unit = unit, positionId = positionId , consumptionAmount = 0};
            dbInterface.Insert(inven, false);
            return inven;
        }

        public string GetMaterialCode(IDbInterface db)
        {
            var income = db.FindId<InCome>(incomeId);
            var materialCode = db.FindId<MaterialCode>(income.codeId);
            return materialCode.code;
        }

        public static List<Inventory> InComeIdGetInventory(int incomeId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<Inventory>(p => p.incomeId == incomeId);
            }
        }
        public static List<Inventory> SelectAll()
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.SelectAll<Inventory>();
            }
        }
        public static List<InventoryView> InventoryViewSelectAll()
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.SelectAll<InventoryView>();
            }
        }
        public static List<InventoryView> SelectRemaining()
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<InventoryView> inventories = new List<InventoryView>();
                foreach (var inv in Dbhelper.Select<InventoryView>(p => p.unit == "g" && p.amount <= 20000))
                {
                    inventories.Add(inv);
                }
                foreach (var inv in Dbhelper.Select<InventoryView>(p => p.unit == "kg" && p.amount <= 20))
                {
                    inventories.Add(inv);
                }
                return inventories;
            }
        }

        public static string GetMaterCodeName(int incomeId)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                int income = Dbhelper.FindId<InCome>(incomeId).codeId;
                return Dbhelper.FindId<MaterialCode>(income).code;
            }
        }
        public static string GetNameName(int incomeId)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var nameId = Dbhelper.FindId<MaterialCode>(Dbhelper.FindId<InCome>(incomeId).codeId).materialNameId;
                return Dbhelper.FindId<Name>(nameId).materialName;
            }
        }
        public static string GetModelName(int incomeId)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var modelId = Dbhelper.FindId<MaterialCode>(Dbhelper.FindId<InCome>(incomeId).codeId).materialModelId;
                return Dbhelper.FindId<Model>(modelId).modelName;
            }
        }
        public static double GetInComeAmount( int incomeId)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var income = Dbhelper.FindId<InCome>(incomeId);
                return WeightConverter.Convert(income.unit, income.amount, "kg");
            }
        }
        public static double GetOutComeNumber(int InventoryId)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                double sum = 0;
                foreach (var number in Dbhelper.Select<OutCome>(p => p.inventoryId == InventoryId))
                {

                    sum += WeightConverter.Convert(number.unit, number.number, "kg");
                }
                return sum;
            }
        }
        public static InCome IncomeIdGetInCome(int incomeId)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.FindId<InCome>(incomeId);
            }
        }
        public static string GetPositionName( int Positionid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.FindId<Position>(Positionid).positionName;
            }
        }
        public static DateTime GetInComeTime(int incomeId)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.FindId<InCome>(incomeId).storageTime;
            }
        }
        public static DateTime NewDateTime()
        {
            return DateTime.Now;
        }

        public static string NewDateMonth()
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            return year + "-" + month;
        }

        public static Inventory GetInventory(int Inventoryid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.FindId<Inventory>(Inventoryid);
            }
        }
        public static InventoryView GetInventoryView(int Inventoryid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.Select<InventoryView>(p => p.InvenId == Inventoryid)[0];
            }
        }
        public static string GetEmployeeName(int Incomeid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.FindId<Employee>(IncomeIdGetInCome(Incomeid).operatorId).name;
            }
        }
        public static List<Inventory> GetInventoryIds(string codeinput, string nameinput)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<Inventory> inventorys = new List<Inventory>();
                List<int> incomeId = new List<int>();
                foreach (var codeid in MaterialCode.GetMaterialId(codeinput, nameinput))
                {
                    foreach (var income in Dbhelper.Select<InCome>(p => p.codeId == codeid))
                    {
                        incomeId.Add(income.id);
                    }
                }
                foreach (var inco in incomeId)
                {
                    foreach (var inventory in Dbhelper.Select<Inventory>(p => p.incomeId == inco))
                    {
                        inventorys.Add(inventory);
                    }
                }
                return inventorys;
            }
        }
        public static List<Inventory> GetTimeInventorys(DateTime begin,DateTime end)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<Inventory> invenList = new List<Inventory>();
                foreach (var time in Dbhelper.Select<InCome>(p => p.storageTime > begin && p.storageTime < end))
                {
                    foreach (var inven in Dbhelper.Select<Inventory>(p => p.incomeId == time.id))
                    {
                        invenList.Add(inven);
                    }
                }
                return invenList;
            }
        }
        public static List<Inventory> NameGetInventoryId(string nameinput)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<Inventory> invenlists = new List<Inventory>();
                foreach (var name in Name.GetNames(nameinput))
                {
                    foreach (var material in Dbhelper.Select<MaterialCode>(p => p.materialNameId == name.id))
                    {
                        foreach (var income in Dbhelper.Select<InCome>(p => p.codeId == material.id))
                        {
                            foreach (var Inven in Dbhelper.Select<Inventory>(p => p.incomeId == income.id))
                            {
                                invenlists.Add(Inven);
                            }
                        }
                    }
                }
                return invenlists;
            }
        }
        public static Inventory GetInventory(int inventoryId ,IDbInterface helper)
        {
            return helper.FindId<Inventory>(inventoryId);
        }
        public static Category GetCategoryName(int incomeid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var incom = Dbhelper.FindId<InCome>(incomeid);
                return Dbhelper.FindId<Category>(incom.categoryId);
            }
        }
        public static Manufacturer GetMenufactureName(int InComeid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var income = Dbhelper.FindId<InCome>(InComeid);
                return Dbhelper.FindId<Manufacturer>(income.menufactureId.Value);
            }
        }
        public static List<InventoryView> MulSelectCheckInventory(string begin, string end, string codeinput, string nameinput,int positionid,int manufacturerid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                ExpressionBuilder<InventoryView> builder = new ExpressionBuilder<InventoryView>();
                ExpressionBuilder<InventoryView> beginbuilder = new ExpressionBuilder<InventoryView>();
                ExpressionBuilder<InventoryView> endbuilder = new ExpressionBuilder<InventoryView>();
                ExpressionBuilder<InventoryView> codebuilder = new ExpressionBuilder<InventoryView>();
                ExpressionBuilder<InventoryView> namebuilder = new ExpressionBuilder<InventoryView>();
                ExpressionBuilder<InventoryView> posibuilder = new ExpressionBuilder<InventoryView>();
                ExpressionBuilder<InventoryView> manubuilder = new ExpressionBuilder<InventoryView>();
                if (begin != "")
                {
                    DateTime begintime = Convert.ToDateTime(begin);
                    beginbuilder.Or(p => p.storageTime >= begintime);
                }
                if (end != "")
                {
                    DateTime endtime = Convert.ToDateTime(end);
                        endbuilder.Or(p => p.storageTime <= endtime);
                }
                if (codeinput != "")
                {
                        codebuilder.Or(p => p.code == codeinput);
                }
                if (nameinput != "")
                {
                        namebuilder.Or(p => p.materialName == nameinput);
                }
                if (positionid != 0)
                {
                    posibuilder.Or(p => p.posiId == positionid);
                }
                if (manufacturerid != 0)
                {
                        manubuilder.Or(p => p.manuId == manufacturerid);
                }
                var exptimebegin = beginbuilder.GetExpression();
                var exptimeend = endbuilder.GetExpression();
                var expcode = codebuilder.GetExpression();
                var expname = namebuilder.GetExpression();
                var expposi = posibuilder.GetExpression();
                var expman = manubuilder.GetExpression();
                if (exptimebegin != null) builder.And(exptimebegin);
                if (exptimeend != null) builder.And(exptimeend);
                if (expcode != null) builder.And(expcode);
                if (expname != null) builder.And(expname);
                if (expposi != null) builder.And(expposi);
                if (expman != null) builder.And(expman);
                var exp = builder.GetExpression();
                if (exp == null)
                {
                    return Dbhelper.SelectAll<InventoryView>();
                }
                return Dbhelper.Select(exp);
            }
        }
        public static Dictionary<string, double> StatisticAmount()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<Inventory> inventories = new List<Inventory>();
                MultipleSeriesStatistics2D<Inventory> statistics2D = new MultipleSeriesStatistics2D<Inventory>(p => p.GetMaterialCode(helper));
                statistics2D.AddSeries("amount",p => WeightConverter.Convert(p.unit, p.amount, "kg"));
                var Sum = statistics2D.GetValues(helper.Select<Inventory>(p => p.amount > 0));
                return Sum["amount"];
            }
        }

        public static Dictionary<string, double> StatisticConsumptionAmount()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                MultipleSeriesStatistics2D<Inventory> statistics2D = new MultipleSeriesStatistics2D<Inventory>(p => p.GetMaterialCode(helper));
                statistics2D.AddSeries("consumptionAmount", p => WeightConverter.Convert(p.unit, p.consumptionAmount, "kg"));
                var Sum = statistics2D.GetValues(helper.Select<Inventory>(p => p.consumptionAmount > 0));
                return Sum["consumptionAmount"];
            }
        }

        public static int Update(int incomeId, double amount, string unit, int positionId, IDbInterface helper)
        {
            Inventory inventory = helper.FindFirst<Inventory, int>("incomeId", incomeId);
            //bool isExit = true;
            double Sum = 0;
            foreach (var outcome in helper.SelectAll<OutCome>())
            {
                if (outcome.inventoryId == inventory.id && outcome.state == 1)
                {
                    //isExit = false;
                    //continue;
                    Sum += WeightConverter.Convert(outcome.unit, outcome.number, unit);
                }
            }
            inventory.amount = amount - Sum;
            if (inventory.amount >= 0)
            {
                inventory.unit = unit;
                inventory.positionId = positionId;
                return helper.Update(inventory);
            }
            else {
                throw new Exception("更改入库数量小于出库量，请重新键入！(出库量为："+Sum+unit+")");
            }
          
            //if (isExit)
            //{
            //    inventory.amount = amount;
            //    inventory.unit = unit;
            //    inventory.positionId = positionId;
            //    return helper.Update(inventory);
            //}
            //try
            //{
            //    inventory.positionId = positionId;
            //    return helper.Update(inventory);
            //}
            //catch
            //{
            //    throw new Exception("已出库，无法更改数量和单位");
            //}
        }
        public static Dictionary<string, object> numberExist(string number,string unit,int invenId) {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            var num = double.Parse(number);
            double Num= WeightConverter.Convert(unit, num, "kg");
            double Num2 = WeightConverter.Convert(GetInventory(invenId).unit, GetInventory(invenId).amount, "kg");
            if (Num > Num2)
            {
                pairs.Add("userExsit", false);
                pairs.Add("msg", "出库量大于库存量，请重新键入！");
            }
            else
            {
                pairs.Add("userExsit", true);
                pairs.Add("msg", "");
            }
            return pairs;
        }
        public static Dictionary<string, object> NumberExist(string number,string unit,int invenId) {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            var num = double.Parse(number);
            double Num= WeightConverter.Convert(unit, num, "kg");
            double Num2 = WeightConverter.Convert(GetInventory(invenId).unit, GetInventory(invenId).amount, "kg");
            if (Num > Num2)
            {
                pairs.Add("userExsit", false);
                pairs.Add("msg", "出库量大于库存量，请重新键入！");
            }
            else
            {
                pairs.Add("userExsit", true);
                pairs.Add("msg", "");
            }
            return pairs;
        }
    }
}
