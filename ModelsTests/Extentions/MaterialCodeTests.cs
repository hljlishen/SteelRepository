using DbInterface;
using DbService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Models.Tests
{
    [TestClass()]
    public class MaterialCodeTests
    {
        private List<Name> names = new List<Name>();
        private List<Model> models = new List<Model>();
        private List<MaterialCode> codes = new List<MaterialCode>();

        private void SetupData()
        {
            names.Add(new Name() { materialName = "N1" });
            names.Add(new Name() { materialName = "N2" });
            names.Add(new Name() { materialName = "N3" });

            models.Add(new Model() { modelName = "M1" });
            models.Add(new Model() { modelName = "M2" });
            models.Add(new Model() { modelName = "M3" });

            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                helper.InsertRange(names);

                helper.InsertRange(models);

                for (int i = 0; i< 3; i++)
                {
                    var code = new MaterialCode() { materialModelId = models[i].id, materialNameId = names[i].id, code = $"C{i+1}"};
                    codes.Add(code);
                    helper.Insert(code);
                }
            }
        }

        private void DestroyData()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                helper.DeleteRange(models);
                helper.DeleteRange(names);
                helper.DeleteRange(codes);
            }
        }
        [TestMethod()]
        public void GetNameTest()
        {
            SetupData();

            try
            {
                var name = codes[0].GetName();
                Assert.IsFalse(ReferenceEquals(name, names[0]));
                Assert.AreEqual(name.id, names[0].id);

                name = codes[1].GetName();
                Assert.IsFalse(ReferenceEquals(name, names[1]));
                Assert.AreEqual(name.id, names[1].id);

                name = codes[2].GetName();
                Assert.IsFalse(ReferenceEquals(name, names[2]));
                Assert.AreEqual(name.id, names[2].id);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                DestroyData();
            }
        }

        [TestMethod()]
        public void GetModelTest()
        {
            SetupData();

            try
            {
                var model = codes[0].GetModel();
                Assert.IsFalse(ReferenceEquals(model, models[0]));
                Assert.AreEqual(model.id, models[0].id);

                model = codes[1].GetModel();
                Assert.IsFalse(ReferenceEquals(model, models[1]));
                Assert.AreEqual(model.id, models[1].id);

                model = codes[2].GetModel();
                Assert.IsFalse(ReferenceEquals(model, models[2]));
                Assert.AreEqual(model.id, models[2].id);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                DestroyData();
            }
        }

        [TestMethod()]
        public void InsertTest()
        {
            SetupData();
            try
            {
                var code = MaterialCode.GetMaterialCode("C1");
                //var code1 = MaterialCode.GetMaterialCode("C1");
                //Assert.IsTrue(ReferenceEquals(code, code1));    //两个引用不相等
                using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
                {
                    #region 组合正常，不插入数据 excel第2行
                    {
                        int codeCount = helper.SelectAll<MaterialCode>().Count;
                        var insertCode = MaterialCode.Insert("C1", "N1", "M1", helper);
                        helper.Commit();
                        int codeCount1 = helper.SelectAll<MaterialCode>().Count;
                        Assert.AreEqual(codeCount, codeCount1);
                    }
                    #endregion

                    #region code找不到，抛出异常 excel第8行
                    {
                        try
                        {
                            var insertCode = MaterialCode.Insert("C4", "N1", "M1", helper);
                            helper.Commit();
                        }
                        catch (Exception e)
                        {
                            Assert.IsTrue(e.Message == "code输入错误");
                        }
                    }
                    #endregion

                    #region 需要插入新数据 excel第9-12行
                    {
                        #region 插入code 不插入name 不插入model excel第9行
                        {
                            int codeCount = helper.SelectAll<MaterialCode>().Count;
                            int nameCount = helper.SelectAll<Name>().Count;
                            int modelCount = helper.SelectAll<Model>().Count;
                            var insertCode = MaterialCode.Insert("C4", "N2", "M1", helper);
                            helper.Commit();
                            int codeCount1 = helper.SelectAll<MaterialCode>().Count;
                            int nameCount1 = helper.SelectAll<Name>().Count;
                            int modelCount1 = helper.SelectAll<Model>().Count;
                            Assert.AreEqual(codeCount, codeCount1 - 1);
                            Assert.AreEqual(nameCount, nameCount1);
                            Assert.AreEqual(modelCount, modelCount1);
                            helper.Delete(insertCode);
                        }
                        #endregion
                        #region 插入code 插入name 不插入model excel第10行
                        {
                            int codeCount = helper.SelectAll<MaterialCode>().Count;
                            int nameCount = helper.SelectAll<Name>().Count;
                            int modelCount = helper.SelectAll<Model>().Count;
                            var insertCode = MaterialCode.Insert("C4", "N4", "M1", helper);
                            helper.Commit();
                            int codeCount1 = helper.SelectAll<MaterialCode>().Count;
                            int nameCount1 = helper.SelectAll<Name>().Count;
                            int modelCount1 = helper.SelectAll<Model>().Count;
                            Assert.AreEqual(codeCount, codeCount1 - 1);
                            Assert.AreEqual(nameCount, nameCount1 - 1);
                            Assert.AreEqual(modelCount, modelCount1);
                            helper.Delete(insertCode);
                            helper.DeleteWhere<Name>(p => p.materialName == "N4");
                        }
                        #endregion
                        #region 插入code 不插入name 插入model excel第11行
                        {
                            int codeCount = helper.SelectAll<MaterialCode>().Count;
                            int nameCount = helper.SelectAll<Name>().Count;
                            int modelCount = helper.SelectAll<Model>().Count;
                            var insertCode = MaterialCode.Insert("C4", "N3", "M4", helper);
                            helper.Commit();
                            int codeCount1 = helper.SelectAll<MaterialCode>().Count;
                            int nameCount1 = helper.SelectAll<Name>().Count;
                            int modelCount1 = helper.SelectAll<Model>().Count;
                            Assert.AreEqual(codeCount, codeCount1 - 1);
                            Assert.AreEqual(nameCount, nameCount1);
                            Assert.AreEqual(modelCount, modelCount1 - 1);
                            helper.Delete(insertCode);
                            helper.DeleteWhere<Model>(p => p.modelName == "M4");
                        }
                        #endregion
                        #region 插入code 插入name 插入model excel第12行
                        {
                            int codeCount = helper.SelectAll<MaterialCode>().Count;
                            int nameCount = helper.SelectAll<Name>().Count;
                            int modelCount = helper.SelectAll<Model>().Count;
                            var insertCode = MaterialCode.Insert("C4", "N4", "M4", helper);
                            helper.Commit();
                            int codeCount1 = helper.SelectAll<MaterialCode>().Count;
                            int nameCount1 = helper.SelectAll<Name>().Count;
                            int modelCount1 = helper.SelectAll<Model>().Count;
                            Assert.AreEqual(codeCount, codeCount1 - 1);
                            Assert.AreEqual(nameCount, nameCount1 - 1);
                            Assert.AreEqual(modelCount, modelCount1 - 1);
                            helper.Delete(insertCode);
                            helper.DeleteWhere<Model>(p => p.id == insertCode.materialModelId);
                            helper.DeleteWhere<Name>(p => p.id == insertCode.materialNameId);
                        }
                        #endregion
                    }
                    #endregion

                    #region code找不到，抛出异常 excel第3-7行
                    {
                        #region code有，name有， model有， 组合有， 组合对不上 excel第3行
                        try
                        {
                            var insertCode = MaterialCode.Insert("C3", "N1", "M1", helper);
                            helper.Commit();
                        }
                        catch (Exception e)
                        {
                            Assert.IsTrue(e.Message == "输入错误");
                        }
                        #endregion

                        #region code有，name有， model有， 没有组合 excel第4行
                        try
                        {
                            var insertCode = MaterialCode.Insert("C3", "N2", "M1", helper);
                            helper.Commit();
                        }
                        catch (Exception e)
                        {
                            Assert.IsTrue(e.Message == "输入错误");
                        }
                        #endregion

                        #region code有，name有， model无 excel第5行
                        try
                        {
                            var insertCode = MaterialCode.Insert("C3", "N2", "M10", helper);
                            helper.Commit();
                        }
                        catch (Exception e)
                        {
                            Assert.IsTrue(e.Message == "输入错误");
                        }
                        #endregion

                        #region code有，name无， model有 excel第6行
                        try
                        {
                            var insertCode = MaterialCode.Insert("C3", "N20", "M1", helper);
                            helper.Commit();
                        }
                        catch (Exception e)
                        {
                            Assert.IsTrue(e.Message == "输入错误");
                        }
                        #endregion

                        #region code有，name无， model无 excel第7行
                        try
                        {
                            var insertCode = MaterialCode.Insert("C3", "N23", "M11", helper);
                            helper.Commit();
                        }
                        catch (Exception e)
                        {
                            Assert.IsTrue(e.Message == "输入错误");
                        }
                        #endregion
                    }
                    #endregion
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                DestroyData();
            }
        }

        [TestMethod()]
        public void GetMaterialCodeTest()
        {
            SetupData();
            try
            {
                var code = MaterialCode.GetMaterialCode("C1");
                Assert.IsTrue(code != null);

                code = MaterialCode.GetMaterialCode("*&JBY^%");
                Assert.IsTrue(code == null);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                DestroyData();
            }
        }

        [TestMethod()]
        public void GetMaterialCodeTest1()
        {
            SetupData();
            try
            {
                var code = MaterialCode.GetMaterialCode("N1", "M1");
                Assert.IsTrue(code.code == "C1");

                code = MaterialCode.GetMaterialCode("N2", "M2");
                Assert.IsTrue(code.code == "C2");

                code = MaterialCode.GetMaterialCode("N3", "M3");
                Assert.IsTrue(code.code == "C3");

                code = MaterialCode.GetMaterialCode("N1", "M2");
                Assert.IsTrue(code == null);

                code = MaterialCode.GetMaterialCode("N1", "M4");
                Assert.IsTrue(code == null);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                DestroyData();
            }
        }

        //[TestMethod()]
        //public void InsertTest1()
        //{
        //    Assert.Fail();
        //}
    }
}