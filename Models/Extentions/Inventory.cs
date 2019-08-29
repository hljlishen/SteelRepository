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
        private static string str;
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
        public static List<Inventory> SelectRemaining()
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<Inventory> inventories = new List<Inventory>();
                foreach (var inv in Dbhelper.Select<Inventory>(p => p.unit == "g" && p.amount <= 20000))
                {
                    inventories.Add(inv);
                }
                foreach (var inv in Dbhelper.Select<Inventory>(p => p.unit == "kg" && p.amount <= 20))
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
        public static string NewDateTime()
        {
            return DateTime.Now.ToLongDateString().ToString();
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
        public static int NameGetEmployeeid(string employeeName)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                str = employeeName + " ";
                string[] str1 = new string[3];
                for (int i = 0; i < 3; i++)
                {
                    str1[i] = MidStrEx(str, "：", " ");
                }
                var employee = Dbhelper.FindFirst<Employee, string>("number", str1[1]);
                if (employee == null)
                {
                    throw new Exception("请输入正确的领用人信息！！！");
                }
                return employee.id;
            }
        }
        public static string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                str = tmpstr;
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
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
                statistics2D.AddSeries("amount", p => p.amount);
                foreach (var inv in helper.Select<Inventory>(p => p.amount > 0))
                {
                    inv.amount = WeightConverter.Convert(inv.unit,inv.amount,"kg");
                    inventories.Add(inv);
                }
                var Sum = statistics2D.GetValues(inventories);
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
    }
}
