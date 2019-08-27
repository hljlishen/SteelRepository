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
        private static IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities());
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
            return Dbhelper.SelectAll<Inventory>();
        }

        public static List<Inventory> SelectRemaining()
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
            var modelId = Dbhelper.FindId<MaterialCode>(Dbhelper.FindId<InCome>(incomeId).codeId).materialModelId;
            return Dbhelper.FindId<Model>(modelId).modelName;
        }
        public static double GetInComeAmount( int incomeId)
        {
            var income = Dbhelper.FindId<InCome>(incomeId);
            return WeightConverter.Convert( income.unit,income.amount, "kg");
        }
        public static double GetOutComeNumber(int InventoryId)
        {
            double sum = 0;
            foreach ( var number in Dbhelper.Select<OutCome>(p =>p.inventoryId == InventoryId))
            {

                sum += WeightConverter.Convert(number.unit,number.number,"kg");
            }
            return sum;
        }
        public static InCome IncomeIdGetInCome(int incomeId)
        {
            return Dbhelper.FindId<InCome>(incomeId);
        }
        public static string GetPositionName( int Positionid)
        {
            return Dbhelper.FindId<Position>(Positionid).positionName;
        }
        public static DateTime GetInComeTime(int incomeId)
        {
            return Dbhelper.FindId<InCome>(incomeId).storageTime;
        }
        public static string NewDateTime()
        {
            return DateTime.Now.ToLongDateString().ToString();
        }
        public static Inventory GetInventory(int Inventoryid)
        {
            return Dbhelper.FindId<Inventory>(Inventoryid);
        }
        public static string GetEmployeeName(int Incomeid)
        {
            return Dbhelper.FindId<Employee>(IncomeIdGetInCome(Incomeid).operatorId).name;
        }
        public static List<Inventory> GetInventoryIds(string codeinput, string nameinput)
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
        public static List<Inventory> GetTimeInventorys(DateTime begin,DateTime end)
        {
            List<Inventory> invenList = new List<Inventory>();
            foreach (var time in Dbhelper.Select<InCome>(p => p.storageTime>begin && p.storageTime<end))
            {
                foreach (var inven in Dbhelper.Select<Inventory>(p => p.incomeId == time.id)) {
                    invenList.Add(inven);
                }
            }
            return invenList;
        }
        public static List<Inventory> NameGetInventoryId(string nameinput)
        {
            List<Inventory> invenlists = new List<Inventory>();
            foreach (var name in Name.GetNames(nameinput))
            {
                foreach (var material in Dbhelper.Select<MaterialCode>(p => p.materialNameId == name.id)) {
                    foreach (var income in Dbhelper.Select<InCome>(p => p.codeId == material.id)) {
                        foreach (var Inven in Dbhelper.Select<Inventory>(p => p.incomeId == income.id)) {
                            invenlists.Add(Inven);
                        }
                    }
                }
            }
            return invenlists;
        }
        public static Inventory GetInventory(int inventoryId ,IDbInterface helper)
        {
            return helper.FindId<Inventory>(inventoryId);
        }
        public static int NameGetEmployeeid(string employeeName)
        {
            str = employeeName + " ";
            string[] str1 = new string[3];
            for (int i = 0; i < 3; i++)
            {
                str1[i] = MidStrEx(str, "：", " ");
            }
            var employee = Dbhelper.FindFirst<Employee,string>("number", str1[1]);
            if (employee == null)
            {
                throw new Exception("请输入正确的领用人信息！！！");
            }
            return employee.id;
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
            var incom = Dbhelper.FindId<InCome>(incomeid);
            return Dbhelper.FindId<Category>(incom.categoryId);
        }
        public static Manufacturer GetMenufactureName(int InComeid)
        {
            var income = Dbhelper.FindId<InCome>(InComeid);
            return Dbhelper.FindId<Manufacturer>(income.menufactureId.Value);
        }
        public static List<Inventory> MulSelectCheckInventory(string begin, string end, string codeinput, string nameinput,int positionid,int manufacturerid)
        {
            ExpressionBuilder<Inventory> builder = new ExpressionBuilder<Inventory>();
            ExpressionBuilder<Inventory> builder0 = new ExpressionBuilder<Inventory>();
            ExpressionBuilder<Inventory> builder1 = new ExpressionBuilder<Inventory>();
            ExpressionBuilder<Inventory> builder2 = new ExpressionBuilder<Inventory>();
            ExpressionBuilder<Inventory> builder3 = new ExpressionBuilder<Inventory>();
            ExpressionBuilder<Inventory> builder4 = new ExpressionBuilder<Inventory>();
            ExpressionBuilder<Inventory> builder5 = new ExpressionBuilder<Inventory>();
            List<Inventory> inventorys = new List<Inventory>();
            if (begin == "" && end == "" && codeinput == "" && nameinput == "" && positionid == 0 && manufacturerid == 0 )
            {
                return Dbhelper.SelectAll<Inventory>();
            }
            if (begin != "")
            {
                DateTime begintime = Convert.ToDateTime(begin);
                var tar = Dbhelper.SqlQuery<Inventory>("select Inventory.* from Inventory, InCome where InCome.storageTime >= '"+begintime+"' and InCome.id = Inventory.incomeId");
                foreach (var incom in tar)
                {
                    builder0.Or(p => p.id == incom.id);
                }
            }
            if (end != "")
            {
                DateTime endtime = Convert.ToDateTime(end);
                var tar = Dbhelper.SqlQuery<Inventory>("select Inventory.* from Inventory, InCome where InCome.storageTime <= '" +endtime+ "' and InCome.id = Inventory.incomeId");
                foreach (var incom in tar) {
                    builder1.Or(p => p.id == incom.id);
                }
            }
            if (codeinput != "")
            {
                var tar = Dbhelper.SqlQuery<Inventory>("select Inventory.* from Inventory, MaterialCode, InCome where MaterialCode.code = '"+
                    codeinput+"' and InCome.codeId = MaterialCode.id and Inventory.incomeId = InCome.id");
                foreach (var inven in tar)
                {
                    builder2.Or(p => p.id == inven.id);
                }
            }
            if (nameinput != "")
            {
                var tar = Dbhelper.SqlQuery<Inventory>("select Inventory.* from Inventory, MaterialCode, InCome, Name where MaterialCode.materialNameId = Name.id and name.materialName = '"+
                    nameinput+"' and InCome.codeId = MaterialCode.id and Inventory.incomeId = InCome.id");
                foreach (var inven in tar)
                {
                    builder3.Or(p => p.id == inven.id);
                }
            }
            if (positionid != 0)
            { 
                builder4.Or(p => p.positionId == positionid);
            }
            if (manufacturerid != 0)
            {
                var tar = Dbhelper.SqlQuery<Inventory>("select Inventory.* from Inventory, InCome where InCome.menufactureId = " +
                    manufacturerid+"and Inventory.incomeId = InCome.id");
                foreach (var inven in tar)
                {
                    builder5.Or(p => p.id == inven.id);
                }
            }
            var exptimebegin = builder0.GetExpression();
            var exptimeend = builder1.GetExpression();
            var expcode = builder2.GetExpression();
            var expname = builder3.GetExpression();
            var expposi = builder4.GetExpression();
            var expman = builder5.GetExpression();
            if (exptimebegin != null) builder.And(exptimebegin);
            if (exptimeend != null) builder.And(exptimeend);
            if (expcode != null) builder.And(expcode);
            if (expname != null) builder.And(expname);
            if (expposi != null) builder.And(expposi);
            if (expman != null) builder.And(expman);
            var exp = builder.GetExpression();
            if (exp == null)
            {
                return new List<Inventory>();
            }
            return Dbhelper.Select(exp);
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
                List<Inventory> inventories = new List<Inventory>();
                MultipleSeriesStatistics2D<Inventory> statistics2D = new MultipleSeriesStatistics2D<Inventory>(p => p.GetMaterialCode(helper));
                statistics2D.AddSeries("consumptionAmount", p => p.consumptionAmount);
                foreach (var inv in helper.Select<Inventory>(p => p.consumptionAmount > 0))
                {
                    inv.amount = WeightConverter.Convert(inv.unit, inv.consumptionAmount, "kg");
                    inventories.Add(inv);
                }
                var Sum = statistics2D.GetValues(inventories);
                return Sum["consumptionAmount"];
            }
        }
    }
}
