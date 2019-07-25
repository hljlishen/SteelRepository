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
    public class CategoryTests
    {
        [TestMethod()]
        public void GetCategoryTest()
        {
            var ret = Category.GetCategory("123");
            ret = Category.GetCategory(1);
        }
    }
}