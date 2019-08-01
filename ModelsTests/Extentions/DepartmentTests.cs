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
    public class DepartmentTests
    {
        [TestMethod()]
        public void GetDepartmentTest()
        {
            
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                Department department = new Department() { id = 1,departmentName = "hffg" };
                Department department1 = new Department() { id = 2,departmentName = "dfgrfd" };
                Department department2 = new Department() { id = 3,departmentName = "jihugy" };
                helper.Insert(department);
                helper.Insert(department1);
                helper.Insert(department2);
                Assert.AreEqual(Department.GetDepartment(1).departmentName, department.departmentName);

            }
        }

        [TestMethod()]
        public void GetDepartmentTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SelectAllTest()
        {
            int count = Department.SelectAll().Count();
            Assert.AreEqual(count,6);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }
    }
}