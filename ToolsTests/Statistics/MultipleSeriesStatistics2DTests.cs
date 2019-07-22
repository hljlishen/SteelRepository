using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tools.Statistics.Tests
{
    [TestClass()]
    public class MultipleSeriesStatistics2DTests
    {
        [TestMethod()]
        public void AddSeriesTest()
        {
            MultipleSeriesStatistics2D<User> statistics2D = new MultipleSeriesStatistics2D<User>("Gender");
            statistics2D.AddSeries("Age");

            Assert.AreEqual(statistics2D.SeriesCount, 1);

            try { statistics2D.AddSeries("Age"); }
            catch { }
            Assert.AreEqual(statistics2D.SeriesCount, 1);

            statistics2D.AddSeries("Weight");
            Assert.AreEqual(statistics2D.SeriesCount, 2);
        }

        [TestMethod()]
        public void GetValuesTest()
        {
            MultipleSeriesStatistics2D<User> statistics2D = new MultipleSeriesStatistics2D<User>("Gender");
            statistics2D.AddSeries("Age");
            statistics2D.AddSeries("Weight");
            var result = statistics2D.GetValues(SetupData());

            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result["Age"]["男"], 66);
            Assert.AreEqual(result["Age"]["女"], 101);
            Assert.AreEqual(result["Weight"]["男"], 250);
            Assert.AreEqual(result["Weight"]["女"], 250);
        }

        private IEnumerable<User> SetupData()
        {
            List<User> lst = new List<User>();
            lst.Add(new User() { Gender = "男", Age = 15, Weight = 50 });
            lst.Add(new User() { Gender = "男", Age = 10, Weight = 50 });
            lst.Add(new User() { Gender = "男"/*, Age = 17*/, Weight = 50 });
            lst.Add(new User() { Gender = "男", Age = 20, Weight = 50 });
            lst.Add(new User() { Gender = "男", Age = 21, Weight = 50 });
            lst.Add(new User() { Gender = "女", Age = 15, Weight = 50 });
            lst.Add(new User() { Gender = "女", Age = 16, Weight = 50 });
            lst.Add(new User() { Gender = "女", Age = 10, Weight = 50 });
            lst.Add(new User() { Gender = "女", Age = 25, Weight = 50 });
            lst.Add(new User() { Gender = "女", Age = 35, Weight = 50 });

            return lst;
        }
    }

    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Gender { get; set; }

        public int? Age { get; set; }
        public double Weight { get; set; }
    }
}