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
    public class RecheckReportTests
    {
        private List<RecheckReport> reports = new List<RecheckReport>();

        private void SetupData()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                reports.Add(new RecheckReport() { incomeId = 1, recheckTime = DateTime.Now.AddDays(-3) });
                reports.Add(new RecheckReport() { incomeId = 1, recheckTime = DateTime.Now.AddDays(-30) });
                reports.Add(new RecheckReport() { incomeId = 1, recheckTime = DateTime.Now.AddDays(-300) });
                reports.Add(new RecheckReport() { incomeId = 1, recheckTime = DateTime.Now.AddDays(-3000)});
                reports.Add(new RecheckReport() { incomeId = 1, recheckTime = DateTime.Now.AddDays(-168) });
                reports.Add(new RecheckReport() { incomeId = 1, recheckTime = DateTime.Now.AddDays(-278) });
                helper.InsertRange(reports);
            }
        }

        private void DestroyData()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                helper.DeleteRange(reports);
            }
        }
        [TestMethod()]
        public void GetExpiredRportsTest()
        {
            SetupData();

            try
            {
                var list1 = RecheckReport.GetExpiredRports(5);
                Assert.IsTrue(list1.Count() >= 5);

                list1 = RecheckReport.GetExpiredRports(500);
                Assert.IsTrue(list1.Count() >= 1);

                list1 = RecheckReport.GetExpiredRports(200);
                Assert.IsTrue(list1.Count() >= 3);
            }
            catch
            {
                Assert.Fail();
            }
            finally
            {
                DestroyData();
            }
        }
    }
}