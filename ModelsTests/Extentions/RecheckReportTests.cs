using Models;
using DbInterface;
using DbService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.Tests
{
    [TestClass()]
    public class RecheckReportTests
    {
        [TestMethod()]
        public void GetExpiredIncomesTest()
        {
            DataGenerator dataGenerator = new DataGenerator();
            try
            {
                dataGenerator.SetupData();
                var expireIncomes = RecheckReport.GetExpiredIncomes(137);
                Assert.IsTrue(expireIncomes.Count >= 1);
                Assert.IsTrue(ContainBatch(expireIncomes, "B2"));

                expireIncomes = RecheckReport.GetExpiredIncomes(80);
                Assert.IsTrue(expireIncomes.Count >= 2);
                Assert.IsTrue(ContainBatch(expireIncomes, "B2"));
                Assert.IsTrue(ContainBatch(expireIncomes, "B3"));

                expireIncomes = RecheckReport.GetExpiredIncomes(50);
                Assert.IsTrue(expireIncomes.Count >= 3);
                Assert.IsTrue(ContainBatch(expireIncomes, "B1"));
                Assert.IsTrue(ContainBatch(expireIncomes, "B2"));
                Assert.IsTrue(ContainBatch(expireIncomes, "B3"));

                expireIncomes = RecheckReport.GetExpiredIncomes(10);
                Assert.IsTrue(expireIncomes.Count >= 4);
                Assert.IsTrue(ContainBatch(expireIncomes, "B4")); ;
                Assert.IsTrue(ContainBatch(expireIncomes, "B1"));
                Assert.IsTrue(ContainBatch(expireIncomes, "B2"));
                Assert.IsTrue(ContainBatch(expireIncomes, "B3"));
            }
            catch
            {
                Assert.Fail();
            }
            finally
            {
                dataGenerator.DestroyData();
            }
        }

        private bool ContainBatch(List<InCome> incomes, string batch)
        {
            foreach (var item in incomes)
            {
                if (item.batch == batch)
                    return true;
            }
            return false;
        }
    }
}