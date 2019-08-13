using DbInterface;
using DbService;
using System;
using System.Collections.Generic;

namespace Models
{
    public partial class OutCome
    {
        public static OutCome NewOutCome(DateTime dateTime, int inventoryId, double amount, string measure, int borrowerId, int? projectId = null, string instructions = "")
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                //查找income?????????????
                var incomeList = helper.Select<InCome>(p => p.id == inventoryId);
                if (incomeList.Count == 0) throw new Exception($"入库记录中不存在id：{inventoryId}");
                var income = incomeList[0];

                //查找inventory???????????
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
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.SelectAll<OutCome>();
            }
        }
    }
}
