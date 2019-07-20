using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tools.Statistics;

namespace Tools.StatisticsDataMaker2D.Tests
{
    [TestClass()]
    public class StatisticsDataMaker2DTests
    {
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

        private IEnumerable<User1> SetupData1()
        {
            List<User1> lst = new List<User1>();
            lst.Add(new User1() { Gender = Gender.male, Age = 15, Weight = 51 });
            lst.Add(new User1() { Gender = Gender.male, Age = 10, Weight = 52 });
            lst.Add(new User1() { Gender = Gender.male, Age = 12, Weight = 53 });
            lst.Add(new User1() { Gender = Gender.male, Age = 20, Weight = 54 });
            lst.Add(new User1() { Gender = Gender.male, Age = 21, Weight = 55 });
            lst.Add(new User1() { Gender = Gender.female, Age = 15, Weight = 51 });
            lst.Add(new User1() { Gender = Gender.female, Age = 16, Weight = 52 });
            lst.Add(new User1() { Gender = Gender.female, Age = 10, Weight = 53 });
            lst.Add(new User1() { Gender = Gender.female, Age = 25, Weight = 54 });
            lst.Add(new User1() { Gender = Gender.female, Age = 35, Weight = 56 });

            return lst;
        }

        [TestMethod()]
        public void GetValuesTest()
        {
            #region test string int
            SingleSeriesStatistics2D<User, int> statistics = new SingleSeriesStatistics2D<User, int>("Gender", "Age");

            Dictionary<string, int> result = statistics.GetValues(SetupData());
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result["男"], 66);
            Assert.AreEqual(result["女"], 101);

            statistics.Calculator = p =>
            {
                int sum = 0;
                int i = 0;
                foreach (var item in p)
                {
                    sum += item;
                    i++;
                }
                return sum / i;
            };
            result = statistics.GetValues(SetupData());
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result["男"], 13);
            Assert.AreEqual(result["女"], 20);
            #endregion

            #region test enum double
            SingleSeriesStatistics2D<User1, double> statistics2D = new SingleSeriesStatistics2D<User1, double>("Gender", "Weight");
            Dictionary<string, double> result1 = statistics2D.GetValues(SetupData1());
            Assert.AreEqual(result1.Count, 2);
            Assert.AreEqual(result1["male"], 265);
            Assert.AreEqual(result1["female"], 266);
            #endregion

            #region test int double
            SingleSeriesStatistics2D<User1, double> statistics2D1 = new SingleSeriesStatistics2D<User1, double>("Age", "Weight");
            Dictionary<string, double> result2 = statistics2D1.GetValues(SetupData1());
            #endregion
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

    class User1
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Gender Gender { get; set; }

        public int? Age { get; set; }
        public double Weight { get; set; }
    }

    enum Gender
    {
        male,
        female
    }
}