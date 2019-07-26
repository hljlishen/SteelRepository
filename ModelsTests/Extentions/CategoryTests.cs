using Microsoft.VisualStudio.TestTools.UnitTesting;

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