using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Tests
{
    [TestClass()]
    public class ExpressionBuilderTests
    {
        [TestMethod()]
        public void ExpressionBuilderTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AndTest()
        {
            ExpressionBuilder<int?> builder = new ExpressionBuilder<int?>();
            builder.And(p => p.Value > 5);
            builder.And(p => p.Value < 15);

            var exp = builder.GetExpression();
        }

        [TestMethod()]
        public void OrTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetExpressionTest()
        {
            Assert.Fail();
        }
    }
}