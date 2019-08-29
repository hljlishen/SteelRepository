using DbService;
using DbServiceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DbService.Tests
{
    [TestClass()]
    public class DbHelperTests
    {
        [TestMethod()]
        public void DbHelperTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                user u = new user() { name = "wang", sex = "女", CompanyId = 1 };
                dbHelper.Insert(u);
                dbHelper.Delete<user>(u.id);
                Assert.IsTrue(dbHelper.Select<user>(p => p.id == u.id).Count() == 0);
            }
        }

        [TestMethod()]
        public void DeleteWhereTest()
        {
            List<user> ls = new List<user>();
            ls.Add(new user() { name = "zhang", sex = "男", CompanyId = 1 });
            ls.Add(new user() { name = "xiao", sex = "男", CompanyId = 1 });
            ls.Add(new user() { name = "liu", sex = "男", CompanyId = 1 });
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                int count = dbHelper.SelectAll<user>().Count();
                dbHelper.InsertRange(ls);
                int count1 = dbHelper.SelectAll<user>().Count();
                Assert.AreEqual(count, count1 - 3);

                int idMin = ls[0].id;
                dbHelper.DeleteWhere<user>(p => p.id >= idMin);
                count1 = dbHelper.SelectAll<user>().Count();
                Assert.AreEqual(count, count1);
            }
        }

        [TestMethod()]
        public void InsertTest()
        {
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                int count = dbHelper.SelectAll<user>().Count();
                user u = new user() { name = "wang", sex = "女", CompanyId = 1 };
                dbHelper.Insert(u);
                int count1 = dbHelper.SelectAll<user>().Count();

                Assert.AreEqual(count, count1 - 1);
            }
        }

        [TestMethod()]
        public void SelectTest()
        {
            IEnumerable<user> result;
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                result = dbHelper.Select<user>(p => p.id == 2);
            }

            user u = result.ElementAt(0);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                user u = new user() { name = "jjj", sex = "女", id = 2 };
                dbHelper.Update(u);
                u.name = "yyy";
                dbHelper.Update(u);
                user u1 = dbHelper.FindId<user>(2);
                Assert.AreEqual("yyy", u1.name);
            }
        }

        [TestMethod()]
        public void InsertRangeTest()
        {
            List<user> ls = new List<user>();
            ls.Add(new user() { name = "zhang", sex = "男", CompanyId = 1 });
            ls.Add(new user() { name = "xiao", sex = "男", CompanyId = 1 });
            ls.Add(new user() { name = "liu", sex = "男", CompanyId = 1 });
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                int count = dbHelper.SelectAll<user>().Count();
                dbHelper.InsertRange(ls);
                int count1 = dbHelper.SelectAll<user>().Count();
                Assert.AreEqual(count, count1 - 3);
            }
        }

        [TestMethod()]
        public void SelectPageTest()
        {
            IEnumerable<user> result;
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                result = dbHelper.SelectPage<user, int>(p => p.id > 0, 3, 2, p => p.id);
                int count1 = result.Count();
                Assert.AreEqual(count1, 3);
            }

            user u = result.ElementAt(0);
        }

        [TestMethod()]
        public void DisposeTest()
        {
            Assert.IsFalse(false);
        }

        [TestMethod()]
        public void FindIdTest()
        {
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                user u = null;
                u = dbHelper.FindId<user>(2);
                Assert.IsTrue(u != null);
            }
        }

        [TestMethod()]
        public void DeleteTest1()
        {
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                user u = new user() { name = "wang", sex = "女", CompanyId = 1 };
                dbHelper.Insert(u);
                dbHelper.Delete(u);
                Assert.IsTrue(dbHelper.Select<user>(p => p.id == u.id).Count() == 0);
            }
        }

        [TestMethod()]
        public void DeleteRangeTest()
        {
            List<user> ls = new List<user>();
            ls.Add(new user() { name = "zhang", sex = "男", CompanyId = 1 });
            ls.Add(new user() { name = "xiao", sex = "男", CompanyId = 1 });
            ls.Add(new user() { name = "liu", sex = "男", CompanyId = 1 });
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                int count = dbHelper.SelectAll<user>().Count();
                dbHelper.InsertRange(ls);
                int count1 = dbHelper.SelectAll<user>().Count();
                Assert.AreEqual(count, count1 - 3);

                dbHelper.DeleteRange(ls);
                count1 = dbHelper.SelectAll<user>().Count();
                Assert.AreEqual(count, count1);
            }
        }

        [TestMethod()]
        public void CommitTest()
        {
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                int count = dbHelper.SelectAll<user>().Count();
                user u = new user() { name = "wang", sex = "女", CompanyId = 1 };
                dbHelper.Insert(u, false);
                int count1 = dbHelper.SelectAll<user>().Count();
                Assert.AreEqual(count, count1);

                dbHelper.Commit();

                count1 = dbHelper.SelectAll<user>().Count();
                Assert.AreEqual(count, count1 - 1);
            }
        }

        [TestMethod()]
        public void SelectAllTest()
        {
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                var result = dbHelper.SelectAll<user>();
                Assert.IsTrue(result.Count() > 0);
            }
        }

        [TestMethod()]
        public void FindFirstTest()
        {
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                var result = dbHelper.FindFirst<user, int>("id", 2);
                Assert.IsTrue(result != null);
            }
        }

        [TestMethod()]
        public void SqlQueryTest()
        {
            using (DbHelper dbHelper = new DbHelper(new userEntities()))
            {
                List<Query3<string, string, string>> query3 = new List<Query3<string, string, string>>();
                var ret = dbHelper.SqlQuery<Query3<string, string, string>>("select InCome.batch as Q1, MaterialCode.code as Q2, Name.materialName as Q3 from InCome, MaterialCode, Name where InCome.codeId = MaterialCode.id and MaterialCode.materialNameId = Name.id");
                foreach (var item in ret)
                {
                     query3.Add(item);
                }
            }
        }
    }

    class Query3<T1,T2,T3>
    {
        T1 Q1 { get; set; }
        T2 Q2 { get; set; }
        T3 Q3 { get; set; }
    }

    class Query4<T1, T2, T3, T4>
    {
        T1 Q1 { get; set; }
        T2 Q2 { get; set; }
        T3 Q3 { get; set; }
        T4 Q4 { get; set; }
    }
}