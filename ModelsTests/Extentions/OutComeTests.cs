using DbInterface;
using DbService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
                        var outcome = OutCome.NewOutCome(DateTime.Now, g.incomes[0].id, 500, "G", 1, 1, "");
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
                            var outcome = OutCome.NewOutCome(DateTime.Now, g.incomes[0].id, 50000, "G", 1, 1, "");
                            Assert.Fail();
                        }
                        catch (Exception e)
                        {
                            Assert.IsTrue(e.Message.Contains("大于库存"));
                        }
                    }
                }
            }
            catch
            {

            }
            finally
            {
                g.DestroyData();
            }
        }

        [TestMethod()]
        public void GetOutComesTest()
        {
            var outcomes = OutCome.GetOutComes(DateTime.Now.AddDays(-10), DateTime.Now);
        }

        [TestMethod()]
        public void StatisticsAmountByMonthTest()
        {
            DataGenerator dataGenerator = new DataGenerator();

            try
            {
                dataGenerator.SetupData();

                var result = OutCome.StatisticsAmountByMonth(new DateTime(2016, 1, 1), new DateTime(2018, 12, 12));
            }
            finally
            {
                dataGenerator.DestroyData();
            }
        }

        [TestMethod()]
        public void StatisticsAmountByQuarterTest()
        {
            DataGenerator dataGenerator = new DataGenerator();

            try
            {
                dataGenerator.SetupData();

                var result = OutCome.StatisticsAmountByQuarter(new DateTime(2016, 1, 1), new DateTime(2018, 12, 12));
            }
            finally
            {
                dataGenerator.DestroyData();
            }
        }

        [TestMethod()]
        public void StatisticsAmountByYearTest()
        {
            DataGenerator dataGenerator = new DataGenerator();

            try
            {
                dataGenerator.SetupData();

                var result = OutCome.StatisticsAmountByYear(new DateTime(2016, 1, 1), new DateTime(2018, 12, 12));
            }
            finally
            {
                dataGenerator.DestroyData();
            }
        }

        [TestMethod()]
        public void StatisticsAmountByCodeTest()
        {
            DataGenerator dataGenerator = new DataGenerator();

            try
            {
                dataGenerator.SetupData();

                var result = OutCome.StatisticsAmountByCode(new DateTime(2016, 1, 1), new DateTime(2018, 12, 12));
            }
            finally
            {
                dataGenerator.DestroyData();
            }
        }
    }
}