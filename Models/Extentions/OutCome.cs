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
        public static IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities());
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
                var outcome = new OutCome() { borrowerId = borrowerId, number = amount, unit = measure, inventoryId = inventoryId, projectId = projectId, recipientsTime = dateTime, instructions = instructions, price = outcomePrice };
                helper.Insert(outcome, false);
                helper.Commit();

                return outcome;
            }
        }

        public static List<OutCome> InComeIdSelect(int inComeId, IDbInterface helper)
        {
            var inventory = helper.FindFirst<Inventory, int>("incomId", inComeId);
            return helper.Select<OutCome>(p => p.inventoryId == inventory.id);
        }
        public static List<OutCome> SelectAll()
        {
            return Dbhelper.SelectAll<OutCome>();
        }
        public static OutCome GetOutCome(int OutComeId)
        {
            return Dbhelper.FindId<OutCome>(OutComeId);
        }
        public static string GetPositionName(int inventoryid)
        {
            int Pid = Dbhelper.FindId<Inventory>(inventoryid).positionId;
            return Dbhelper.FindId<Position>(Pid).positionName;
        }
        public static string GetEmployeeName(int borrowerid)
        {
            return Dbhelper.FindId<Employee>(borrowerid).name;
        }
        public static List<string> GetEmployeeNames()
        {
            List<string> listr = new List<string>();
            foreach (var employee in Dbhelper.SelectAll<Employee>())
            {
                listr.Add(employee.name);
            }
            return listr;
        }
        public static string GetProjectName(int? projectid)
        {
            if (projectid == null)
            {
                return null;
            }
            return Dbhelper.FindId<Project>(projectid.Value).projectName;
        }
        public static string GetNameName(int inventoryid)
        {
            var Income = Dbhelper.FindId<InCome>(Dbhelper.FindId<Inventory>(inventoryid).incomeId);
            var Material = Dbhelper.FindId<MaterialCode>(Income.codeId);
            return Dbhelper.FindId<Name>(Material.materialNameId).materialName;
        }
        public static string GetModelName(int inventoryid)
        {
            var Income = Dbhelper.FindId<InCome>(Dbhelper.FindId<Inventory>(inventoryid).incomeId);
            var Material = Dbhelper.FindId<MaterialCode>(Income.codeId);
            return Dbhelper.FindId<Model>(Material.materialModelId).modelName;
        }
        public static double? GetInComeunitPrice(int inventoryid)
        {
            var Income = Dbhelper.FindId<InCome>(Dbhelper.FindId<Inventory>(inventoryid).incomeId);
            return Income.unitPrice;
        }
        public static string GetMaterCodeName(int inventoryid)
        {
            var Income = Dbhelper.FindId<InCome>(Dbhelper.FindId<Inventory>(inventoryid).incomeId);
            return Dbhelper.FindId<MaterialCode>(Income.codeId).code;
        }
        public static List<OutCome> ReverseGetEmployeeName(string employeeName)
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
        public static List<OutCome> OutComeNumber(double number)
        {
            return Dbhelper.Select<OutCome>(p => p.number == number);
        }
        public static List<OutCome> CodeidGetOutCome(int MaterialCodeid)
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
        public static List<int> CodeidGetOutComeId(int MaterialCodeid)
        {
            List<int> listId = new List<int>();
            foreach ( var come in CodeidGetOutCome(MaterialCodeid))
            {
                listId.Add(come.id);
            }
            return listId;
        }
        public static List<OutCome> MulSelectCheckOutCome(bool b, DateTime begin, bool e, DateTime end, int MaterCodeid, int employeeid)
        {
            ExpressionBuilder<OutCome> builder = new ExpressionBuilder<OutCome>();
            List<OutCome> comes = new List<OutCome>();
            if (b)
            {
                if (e)
                {
                    if (MaterCodeid != 0)
                    {
                        if (employeeid != 0)
                        {
                            builder.And(p => p.borrowerId == employeeid);
                            var exp = builder.GetExpression();
                            var Icomes = GetOutComes(begin, end).Where(exp.Compile());
                            foreach (var come in Icomes)
                            {
                                foreach (var comeid in CodeidGetOutComeId(MaterCodeid))
                                {
                                    if (come.id == comeid)
                                    {
                                        comes.Add(come);
                                    }
                                }
                            }
                            return comes;
                        }
                        else
                        {
                            foreach (var come in GetOutComes(begin, end))
                            {
                                foreach (var comeid in CodeidGetOutComeId(MaterCodeid))
                                {
                                    if (come.id == comeid)
                                    {
                                        comes.Add(come);
                                    }
                                }
                            }
                            return comes;
                        }
                    }
                    else
                    {
                        if (employeeid != 0)
                        {
                            builder.And(p => p.borrowerId == employeeid);
                            var exp = builder.GetExpression();
                            var Icomes = GetOutComes(begin, end).Where(exp.Compile());
                            foreach (var come in Icomes)
                            {
                                comes.Add(come);
                            }
                            return comes;
                        }
                        else
                        {
                            return GetOutComes(begin, end);
                        }
                    }
                }
                else
                {
                    if (MaterCodeid != 0)
                    {
                        if (employeeid != 0)
                        {
                            builder.And(p => p.borrowerId == employeeid);
                            var exp = builder.GetExpression();
                            var Icomes = GetOutComes(begin, DateTime.MaxValue).Where(exp.Compile());
                            foreach (var come in Icomes)
                            {
                                foreach (var comeid in CodeidGetOutComeId(MaterCodeid))
                                {
                                    if (come.id == comeid)
                                    {
                                        comes.Add(come);
                                    }
                                }
                            }
                            return comes;
                        }
                        else
                        {
                            foreach (var come in GetOutComes(begin, DateTime.MaxValue))
                            {
                                foreach (var comeid in CodeidGetOutComeId(MaterCodeid))
                                {
                                    if (come.id == comeid)
                                    {
                                        comes.Add(come);
                                    }
                                }
                            }
                            return comes;
                        }
                    }
                    else
                    {
                        if (employeeid != 0)
                        {
                            builder.And(p => p.borrowerId == employeeid);
                            var exp = builder.GetExpression();
                            var Icomes = GetOutComes(begin, DateTime.MaxValue).Where(exp.Compile());
                            foreach (var come in Icomes)
                            {
                                comes.Add(come);
                            }
                            return comes;
                        }
                        else
                        {
                            return GetOutComes(begin, DateTime.MaxValue);
                        }
                    }
                }
            }
            else
            {
                if (e)
                {
                    if (MaterCodeid != 0)
                    {
                        if (employeeid != 0)
                        {
                            builder.And(p => p.borrowerId == employeeid);
                            var exp = builder.GetExpression();
                            var Icomes = GetOutComes(DateTime.MinValue, end).Where(exp.Compile());
                            foreach (var come in Icomes)
                            {
                                foreach (var comeid in CodeidGetOutComeId(MaterCodeid))
                                {
                                    if (come.id == comeid)
                                    {
                                        comes.Add(come);
                                    }
                                }
                            }
                            return comes;
                        }
                        else
                        {
                            foreach (var come in GetOutComes(DateTime.MinValue, end))
                            {
                                foreach (var comeid in CodeidGetOutComeId(MaterCodeid))
                                {
                                    if (come.id == comeid)
                                    {
                                        comes.Add(come);
                                    }
                                }
                            }
                            return comes;
                        }
                    }
                    else
                    {
                        if (employeeid != 0)
                        {
                            builder.And(p => p.borrowerId == employeeid);
                            var exp = builder.GetExpression();
                            var Icomes = GetOutComes(DateTime.MinValue, end).Where(exp.Compile());
                            foreach (var come in Icomes)
                            {
                                comes.Add(come);
                            }
                            return comes;
                        }
                        else
                        {
                            return GetOutComes(DateTime.MinValue, end);
                        }
                    }
                }
                else
                {
                    if (MaterCodeid != 0)
                    {
                        if (employeeid != 0)
                        {
                            builder.And(p => p.borrowerId == employeeid);
                            var exp = builder.GetExpression();
                            var Icomes = SelectAll().Where(exp.Compile());
                            foreach (var come in Icomes)
                            {
                                foreach (var comeid in CodeidGetOutComeId(MaterCodeid))
                                {
                                    if (come.id == comeid)
                                    {
                                        comes.Add(come);
                                    }
                                }
                            }
                            return comes;
                        }
                        else
                        {
                            foreach (var come in SelectAll())
                            {
                                foreach (var comeid in CodeidGetOutComeId(MaterCodeid))
                                {
                                    if (come.id == comeid)
                                    {
                                        comes.Add(come);
                                    }
                                }
                            }
                            return comes;
                        }
                    }
                    else
                    {
                        if (employeeid != 0)
                        {
                            builder.And(p => p.borrowerId == employeeid);
                            var exp = builder.GetExpression();
                            var Icomes = SelectAll().Where(exp.Compile());
                            foreach (var come in Icomes)
                            {
                                comes.Add(come);
                            }
                            return comes;
                        }
                        else
                        {
                            return new List<OutCome>();
                        }
                    }
                }
            }
        }
    }
}
