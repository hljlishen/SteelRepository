using DbInterface;
using DbService;
using System;
using System.Collections.Generic;

namespace Models
{
    public partial class OutCome
    {
        public static IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities());
        public static  List<OutCome> comes;
        public static OutCome NewOutCome(DateTime dateTime, int inventoryId, double amount, string measure, int borrowerId, int? projectId = null, string instructions = "")
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                //查找income
                var incomeList = helper.Select<InCome>(p => p.id == inventoryId);
                if (incomeList.Count == 0) throw new Exception($"入库记录中不存在id：{inventoryId}");
                var income = incomeList[0];

                //查找inventory
                var inventoryList = helper.Select<Inventory>(p => p.incomeId == inventoryId);
                if (inventoryList.Count == 0) throw new Exception($"库存中不存在incomeId：{inventoryId}");
                var inventory = inventoryList[0];

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
        public static string GetPositionName(int inventoryid)
        {
            int Pid = Dbhelper.FindId<Inventory>(inventoryid).positionId;
            return Dbhelper.FindId<Position>(Pid).positionName;
        }
        public static string GetEmployeeName(int borrowerid)
        {
            return Dbhelper.FindId<Employee>(borrowerid).name;
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
        public static List<OutCome> ReverseGetPositionName(string positionName)
        {
            var position = Dbhelper.FindFirst<Position, string>("positionName", positionName);
            foreach (var inventory in Dbhelper.Select<Inventory>(p => p.positionId == position.id))
            {
               var coms = Dbhelper.Select<OutCome>(p => p.inventoryId == inventory.id);
                foreach (var com in coms)
                {
                    comes.Add(com);
                }
            }
            return comes;
        }
        public static List<OutCome> ReverseGetEmployeeName(string employeeName)
        {
            var employee = Dbhelper.FindFirst<Employee, string>("employeeName", employeeName);
            foreach (var come in Dbhelper.Select<OutCome>(p => p.borrowerId == employee.id))
            {
                comes.Add(come);
            }
            return comes;
        }
        public static List<OutCome> ReverseGetProjectName(string projectName)
        {
            var project = Dbhelper.FindFirst<Project, string>("projectName", projectName);
            foreach (var come in Dbhelper.Select<OutCome>(p => p.projectId == project.id))
            {
                comes.Add(come);
            }
            return comes;
        }
        public static List<OutCome> OutComeNumber(int number)
        {
            return Dbhelper.Select<OutCome>(p => p.number == number);
        }

    }
}
