using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace Models
{
    public partial class OutCome
    {
        //public static IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities());
        public static List<OutCome> comes = new List<OutCome>();
        public static OutCome NewOutCome(DateTime dateTime, int inventoryId, double amount, string measure, int borrowerId, int? projectId = null, string instructions = "")
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                ////查找income
                //var incomeList = helper.Select<InCome>(p => p.id == inventoryId);
                //if (incomeList.Count == 0) throw new Exception($"入库记录中不存在id：{inventoryId}");
                //var income = incomeList[0];

                //查找inventory
                var inventoryList = helper.Select<Inventory>(p => p.id == inventoryId);
                if (inventoryList.Count == 0) throw new Exception($"库存中不存在inventoryId：{inventoryId}");
                var inventory = inventoryList[0];
                var income = helper.FindId<InCome>(inventory.incomeId);

                //判断出库数量是否合法
                double outcomeAmout = WeightConverter.Convert(measure, amount, inventory.unit);
                if (outcomeAmout > inventory.amount) throw new Exception($"出库数量:{outcomeAmout},大于库存:{inventory.amount}");

                //计算价格
                double? outcomePrice;
                if (income.unitPrice == null)
                    outcomePrice = null;
                else
                {
                    double outAmout = WeightConverter.Convert(measure, amount, income.priceMeasure);
                    outcomePrice = outAmout * income.unitPrice;
                }

                inventory.amount -= outcomeAmout;
                inventory.consumptionAmount += outcomeAmout;
                helper.Update(inventory, false);
                var state1 = 1;
                var outcome = new OutCome() { borrowerId = borrowerId, number = amount, unit = measure, inventoryId = inventoryId, projectId = projectId, recipientsTime = dateTime, instructions = instructions, price = outcomePrice,state = state1 };
                helper.Insert(outcome, false);
                helper.Commit();

                return outcome;
            }
        }

        public static List<OutCome> InComeIdSelect(int inComeId, IDbInterface helper)
        {
            var inventory = helper.Select<Inventory>(p => p.incomeId == inComeId);
            int inventoryId = 0;
            foreach (var inv in inventory)
            {
                inventoryId = inv.id;
            }
            return helper.Select<OutCome>(p => p.inventoryId == inventoryId);
        }
        public static List<OutCome> SelectAll()
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities())) {
                return Dbhelper.SelectAll<OutCome>();
            }
              
        }
        public static List<OutCome> GetOutComesDesc()
        {
            List<OutCome> liDesc = new List<OutCome>();
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var ret = helper.SqlQuery<OutCome>("select OutCome.* from OutCome order by OutCome.recipientsTime desc");
                foreach (var outcome in ret)
                {
                    liDesc.Add(outcome);
                }
                return liDesc;
            }
        }
        public static OutcomeQueryView GetOutCome(int OutComeId)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.Select<OutcomeQueryView>(p => p.OutId == OutComeId)[0];
            }
        }
        public static string GetPositionName(int inventoryid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                int Pid = Dbhelper.FindId<Inventory>(inventoryid).positionId;
                return Dbhelper.FindId<Position>(Pid).positionName;
            }
        }
        public static string GetEmployeeName(int borrowerid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.FindId<Employee>(borrowerid).name;
            }
        }
        public static List<string> GetEmployeeNames()
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<string> listr = new List<string>();
                foreach (var employee in Dbhelper.SelectAll<Employee>())
                {
                    listr.Add(employee.name);
                }
                return listr;
            }
        }
        public static string GetProjectName(int? projectid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if (projectid == null)
                {
                    return null;
                }
                var pro = Dbhelper.FindId<Project>(projectid.Value).projectName;
                return pro;
            }
        }
        public static string GetNameName(int inventoryid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var Income = Dbhelper.FindId<InCome>(Dbhelper.FindId<Inventory>(inventoryid).incomeId);
                var Material = Dbhelper.FindId<MaterialCode>(Income.codeId);
                return Dbhelper.FindId<Name>(Material.materialNameId).materialName;
            }
        }
        public static string GetModelName(int inventoryid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var Income = Dbhelper.FindId<InCome>(Dbhelper.FindId<Inventory>(inventoryid).incomeId);
                var Material = Dbhelper.FindId<MaterialCode>(Income.codeId);
                return Dbhelper.FindId<Model>(Material.materialModelId).modelName;
            }
        }
        public static double? GetInComeunitPrice(int inventoryid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var Income = Dbhelper.FindId<InCome>(Dbhelper.FindId<Inventory>(inventoryid).incomeId);
                return Income.unitPrice;
            }
        }
        public static string GetMaterCodeName(int inventoryid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var Income = Dbhelper.FindId<InCome>(Dbhelper.FindId<Inventory>(inventoryid).incomeId);
                return Dbhelper.FindId<MaterialCode>(Income.codeId).code;
            }
        }
        public static List<OutCome> ReverseGetEmployeeName(string employeeName)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var employee = Dbhelper.FindFirst<Employee, string>("employeeName", employeeName);
                if (employee == null)
                    return null;
                foreach (var come in Dbhelper.Select<OutCome>(p => p.borrowerId == employee.id))
                {
                    comes.Add(come);
                }
                return comes;
            }
        }
        public static List<OutCome> OutComeNumber(double number)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.Select<OutCome>(p => p.number == number);
            }
        }
        public static List<OutCome> CodeidGetOutCome(int MaterialCodeid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<OutCome> comes = new List<OutCome>();
                var inComes = Dbhelper.Select<InCome>(p => p.codeId == MaterialCodeid);
                foreach (var income in inComes)
                {
                    var inventorys = Dbhelper.Select<Inventory>(p => p.incomeId == income.id);
                    foreach (var inventory in inventorys)
                    {
                        var outcomes = Dbhelper.Select<OutCome>(p => p.inventoryId == inventory.id);
                        foreach (var outcom in outcomes)
                        {
                            comes.Add(outcom);
                        }
                    }
                }
                return comes;
            }
        }
        public static List<int> CodeidGetOutComeId(int MaterialCodeid)
        {
            List<int> listId = new List<int>();
            foreach ( var come in CodeidGetOutCome(MaterialCodeid))
            {
                listId.Add(come.id);
            }
            return listId;
        }
        public static Category GetCategoryName(int inventoryid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var income = Dbhelper.FindId<InCome>(Dbhelper.FindId<Inventory>(inventoryid).incomeId);
                return Dbhelper.FindId<Category>(income.categoryId);
            }
        }
        public static int OutComeRevocation(int incomeid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<int> outcomeid = new List<int>();
                if (incomeid == 0)
                {
                    foreach (var id in SelectAll())
                    {
                        outcomeid.Add(id.id);
                    }
                }
                else
                {
                    var income = Dbhelper.FindId<InCome>(incomeid);
                    var invenView = Dbhelper.Select<InventoryView>(p => p.batch == income.batch)[0];
                    foreach (var id2 in Dbhelper.Select<OutCome>(p => p.inventoryId == invenView.InvenId))
                    {
                        outcomeid.Add(id2.id);
                    }
                }
                if (outcomeid.Count == 0) throw new Exception("该货品批号无出库记录！！");
                var Maxoutcome = Dbhelper.FindId<OutCome>(outcomeid.Max());
                var Inventory = Dbhelper.FindId<Inventory>(Maxoutcome.inventoryId);
                if (Maxoutcome.state < 2)
                {
                    var Number = WeightConverter.Convert(Maxoutcome.unit, Maxoutcome.number, Inventory.unit);
                    Inventory.amount += Number;
                    Inventory.consumptionAmount -= Number;
                    Maxoutcome.state = 2;
                    Maxoutcome.instructions += "(已撤销)";
                    Dbhelper.Update(Maxoutcome, false);
                    Dbhelper.Update(Inventory, false);
                    return Dbhelper.Commit();
                }
                throw new Exception("该货品的最后一条出库记录处于撤销状态，不能再执行本次操作！！");
            }
        }
        public static List<OutcomeQueryView> GetRevocationOutComes()
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.Select<OutcomeQueryView>(p => p.state == 2);
            }
        }
        public static List<OutcomeQueryView> MulSelectCheckOutCome(string begin,string end, int MaterCodeid, int employeeid,int manufacturerid,int departmentid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                ExpressionBuilder<OutcomeQueryView> builder = new ExpressionBuilder<OutcomeQueryView>();
                ExpressionBuilder<OutcomeQueryView> beginbuilder = new ExpressionBuilder<OutcomeQueryView>();
                ExpressionBuilder<OutcomeQueryView> endbuilder = new ExpressionBuilder<OutcomeQueryView>();
                ExpressionBuilder<OutcomeQueryView> Codebuilder = new ExpressionBuilder<OutcomeQueryView>();
                ExpressionBuilder<OutcomeQueryView> Manbuilder = new ExpressionBuilder<OutcomeQueryView>();
                ExpressionBuilder<OutcomeQueryView> deparbuilder = new ExpressionBuilder<OutcomeQueryView>();
                ExpressionBuilder<OutcomeQueryView> emplobuilder = new ExpressionBuilder<OutcomeQueryView>();
                if (begin != "")
                {
                    DateTime begintime = Convert.ToDateTime(begin);
                    beginbuilder.And(p => p.recipientsTime >= begintime);
                }
                if (end != "")
                {
                    DateTime endtime = Convert.ToDateTime(end);
                    endbuilder.And(p => p.recipientsTime <= endtime);
                }
                if (MaterCodeid != 0)
                {
                        Codebuilder.Or(p => p.codeId == MaterCodeid);
                }
                if (manufacturerid != 0)
                {
                        Manbuilder.Or(p => p.mfId == manufacturerid);
                }
                if (departmentid != 0)
                {
                        deparbuilder.Or(p => p.deparId==departmentid);
                }
                if (employeeid != 0)
                {
                    emplobuilder.Or(p => p.emploId == employeeid);
                }
                var exptimebegin = beginbuilder.GetExpression();
                var exptimeend = endbuilder.GetExpression();
                var expcode = Codebuilder.GetExpression();
                var expman = Manbuilder.GetExpression();
                var dapar = deparbuilder.GetExpression();
                var emplo = emplobuilder.GetExpression();
                if (exptimebegin != null) builder.And(exptimebegin);
                if (exptimeend != null) builder.And(exptimeend);
                if (expcode != null) builder.And(expcode);
                if (emplo != null) builder.And(emplo);
                if (dapar != null) builder.And(dapar);
                if (expman != null) builder.And(expman);
                var exp = builder.GetExpression();
                if (exp == null)
                {
                    List<OutcomeQueryView> list2 = Dbhelper.SelectAll<OutcomeQueryView>();
                    return list2;
                }
                else
                    return Dbhelper.Select(exp);
            }
        }
        public string GetMaterialCode(IDbInterface db)
        {
            var incomeId = db.FindId<Inventory>(inventoryId).incomeId;
            var income = db.FindId<InCome>(incomeId);
            var materialCode = db.FindId<MaterialCode>(income.codeId);
            return materialCode.code;
        }

        public string GetMaterialProject(IDbInterface db)
        {
            var projectName = db.FindId<Project>(projectId.Value).projectName;
            return projectName;
        }
        public static List<OutcomeQueryView> GetOutComeView()
        {
            using (IDbInterface db = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<OutcomeQueryView> list1 = db.SelectAll<OutcomeQueryView>();
                return list1;
            }
        }
    }
}
