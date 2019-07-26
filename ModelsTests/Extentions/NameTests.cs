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
    public class NameTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                int count = helper.SelectAll<Name>().Count();
                Name.Insert("*&^%$", helper);
                helper.Commit();
                int count1 = helper.SelectAll<Name>().Count();
                Assert.AreEqual(count1 - 1, count);
                Name.Insert("*&^%$", helper);
                helper.Commit();
                Assert.AreEqual(count1 - 1, count);
                helper.DeleteWhere<Name>(p => p.materialName == "*&^%$");
            }
        }
    }
}