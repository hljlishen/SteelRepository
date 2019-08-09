using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Tools.Statistics.Tests
{
    [TestClass()]
    public class MultipleSeriesStatistics2DTests
    {
        [TestMethod()]
        public void AddSeriesTest()
        {
            MultipleSeriesStatistics2D<User> statistics2D = new MultipleSeriesStatistics2D<User>(p => p.Gender);
            statistics2D.AddSeries("Age", p => p.Age ?? 0) ;

            Assert.AreEqual(statistics2D.SeriesCount, 1);

            try
            {
                statistics2D.AddSeries("Age", p => p.Age ?? 0);
                Assert.Fail();
            }
            catch { }
            Assert.AreEqual(statistics2D.SeriesCount, 1);

            statistics2D.AddSeries("Weight", p => p.Weight);
            Assert.AreEqual(statistics2D.SeriesCount, 2);
        }

        [TestMethod()]
        public void GetValuesTest()
        {
            #region 统计User中自带的字段
            //同时统计不同性别的年龄之和 和 体重之和
            {
                MultipleSeriesStatistics2D<User> statistics2D = new MultipleSeriesStatistics2D<User>(p => p.Gender);
                statistics2D.AddSeries("Age", p => p.Age ?? 0);     //calculator不传值时默认为取和运算
                statistics2D.AddSeries("Weight", p => p.Weight);
                var result = statistics2D.GetValues(SetupData());

                Assert.AreEqual(result.Count, 2);
                Assert.AreEqual(result["Age"]["男"], 66);
                Assert.AreEqual(result["Age"]["女"], 101);
                Assert.AreEqual(result["Weight"]["男"], 250);
                Assert.AreEqual(result["Weight"]["女"], 250);
            }
            //同时统计不同性别的年龄均值 和 体重均值
            {
                var statistics2D = new MultipleSeriesStatistics2D<User>(p => p.Gender);
                statistics2D.AddSeries("Age", p => p.Age ?? 0, StatisticsType.Average);
                statistics2D.AddSeries("Weight", p => p.Weight, StatisticsType.Average);
                var result = statistics2D.GetValues(SetupData());

                Assert.AreEqual(result.Count, 2);
                Assert.AreEqual(result["Age"]["男"], 13.2);
                Assert.AreEqual(result["Age"]["女"], 20.2);
                Assert.AreEqual(result["Weight"]["男"], 50);
                Assert.AreEqual(result["Weight"]["女"], 50);
            }
            //统计男女个数
            {
                var statistics2D = new MultipleSeriesStatistics2D<User>(p => p.Gender);
                statistics2D.AddSeries("个数", p => 0, StatisticsType.Count);
                var result = statistics2D.GetValues(SetupData());
                Assert.AreEqual(result.Count, 1);
                Assert.AreEqual(result["个数"]["男"], 5);
                Assert.AreEqual(result["个数"]["女"], 5);
            }
            #endregion

            #region 统计User中不存在的字段
            //统计不同月份注册人数
            {
                Func<User, string> xAxisMapper = p =>
                {
                    return p.RegisterDate.ToString("yyyy年MM月");
                };

                var statistics2D = new MultipleSeriesStatistics2D<User>(xAxisMapper);
                statistics2D.AddSeries("个数", p => 0, StatisticsType.Count);
                var result = statistics2D.GetValues(SetupData());
                Assert.AreEqual(result.Count, 1);
                Assert.AreEqual(result["个数"]["2007年01月"], 3);
                Assert.AreEqual(result["个数"]["2019年03月"], 2);
                Assert.AreEqual(result["个数"]["2017年04月"], 2);
                Assert.AreEqual(result["个数"]["2019年01月"], 2);
                Assert.AreEqual(result["个数"]["2017年01月"], 1);
            }
            #endregion
        }

        private IEnumerable<User> SetupData()
        {
            List<User> lst = new List<User>();
            lst.Add(new User() { Gender = "男", Age = 15, Weight = 50, RegisterDate = DateTime.Parse("2007-1-15") });
            lst.Add(new User() { Gender = "男", Age = 10, Weight = 50, RegisterDate = DateTime.Parse("2019-3-22") });
            lst.Add(new User() { Gender = "男", Age = null, Weight = 50, RegisterDate = DateTime.Parse("2017-4-25") }); ;
            lst.Add(new User() { Gender = "男", Age = 20, Weight = 50, RegisterDate = DateTime.Parse("2017-4-15") });
            lst.Add(new User() { Gender = "男", Age = 21, Weight = 50, RegisterDate = DateTime.Parse("2019-1-15") });
            lst.Add(new User() { Gender = "女", Age = 15, Weight = 50, RegisterDate = DateTime.Parse("2017-1-15") });
            lst.Add(new User() { Gender = "女", Age = 16, Weight = 50, RegisterDate = DateTime.Parse("2007-1-15") });
            lst.Add(new User() { Gender = "女", Age = 10, Weight = 50, RegisterDate = DateTime.Parse("2019-3-15") });
            lst.Add(new User() { Gender = "女", Age = 25, Weight = 50, RegisterDate = DateTime.Parse("2019-1-22") });
            lst.Add(new User() { Gender = "女", Age = 35, Weight = 50, RegisterDate = DateTime.Parse("2007-1-15") });

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
        public DateTime RegisterDate { get; set; }
    }
}