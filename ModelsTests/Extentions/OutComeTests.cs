using DbInterface;
using DbService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tests
{
    [TestClass()]
    public class OutComeTests
    {
        [TestMethod()]
        public void NewOutComeTest()
        {
            DataGenerator g = new DataGenerator();
            g.SetupData();

            try
            {
                using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
                {
                    //测试正常
                    {
                        var outcome = OutCome.NewOutCome(g.incomes[0].id, 500, "G", 1, 1, "");
                        int incomeId = g.incomes[0].id;
                        var InventoryList = helper.Select<Inventory>(p => p.incomeId == incomeId);
                        var inventory = InventoryList[0];
                        Assert.IsTrue(InventoryList.Count > 0);
                        Assert.AreEqual(inventory.amount, 49.5);
                        Assert.AreEqual(inventory.unit, "千克");

                        var recordOutcome = helper.Select<OutCome>(p => p.incomeId == incomeId)[0];
                        Assert.AreEqual(recordOutcome.price, 5);
                        helper.Delete(recordOutcome);
                    }

                    //测试数量超出余额
                    {
                        try
                        {
                            var outcome = OutCome.NewOutCome(g.incomes[0].id, 50000, "G", 1, 1, "");
                            Assert.Fail();
                        }
                        catch (Exception e)
                        {
                            Assert.IsTrue(e.Message.Contains("大于库存"));
                        }
                    }
                }
            }
            catch(Exception e)
            {

            }
            finally
            {
                g.DestroyData();
            }
        }
    }
}