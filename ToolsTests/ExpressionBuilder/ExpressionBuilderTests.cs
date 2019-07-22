using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Tools.Tests
{
    [TestClass()]
    public class ExpressionBuilderTests
    {
        [TestMethod()]
        public void ExpressionBuilderTest()
        {
            
        }

        [TestMethod()]
        public void AndTest()
        {
            List<int?> ls = new List<int?>();
            ls.Add(11);
            ls.Add(3);
            ls.Add(1);
            ls.Add(4);
            ls.Add(5);
            ls.Add(6);
            ls.Add(12);
            ls.Add(16);
            ls.Add(17);
            ExpressionBuilder<int?> builder = new ExpressionBuilder<int?>();
            builder.And(p => p.Value > 5);
            builder.And(p => p.Value < 15);
            var exp = builder.GetExpression();
            var result = ls.Where(exp.Compile());
            Assert.AreEqual(result.Count(), 3);


        }
        [TestMethod()]
        public void OrTest()
        {
            List<int?> ls = new List<int?>();
            ls.Add(11);
            ls.Add(3);
            ls.Add(1);
            ls.Add(4);
            ls.Add(5);
            ls.Add(6);
            ls.Add(12);
            ls.Add(16);
            ls.Add(17);
            ExpressionBuilder<int?> builder = new ExpressionBuilder<int?>();
            builder.Or(p => p.Value > 7);
            builder.Or(p => p.Value == 3);
            var exp = builder.GetExpression();
            var result = ls.Where(exp.Compile());
            Assert.AreEqual(result.Count(), 5);
        }

        [TestMethod()]
        public void GetExpressionTest()
        {
            ExpressionBuilder<int?> builder = new ExpressionBuilder<int?>();
        }
    }
}