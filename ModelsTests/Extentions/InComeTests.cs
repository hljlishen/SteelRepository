using Models;
using DbInterface;
using DbService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Models.Tests
{
    [TestClass()]
    public class InComeTests
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

                for (int i = 0; i < 3; i++)
                {
                    var code = new MaterialCode() { materialModelId = models[i].id, materialNameId = names[i].id, code = $"C{i + 1}" };
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
        public void BatchIdExistTest()
        {
            var income = new InCome() { batch = "123", categoryId = 1, codeId = 1, positionId = 1, menufactureId = 1, amount = 5, operatorId = 1, unit = "千克", storageTime = DateTime.Now };
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                try
                {
                    _ = helper.Insert(income);
                    Assert.IsTrue(InCome.BatchIdExist("123", helper));
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    _ = helper.Delete(income);
                }
            }
        }

        [TestMethod()]
        public void NewInComeTest()
        {
            SetupData();
            try
            {
                using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
                {
                    //测试正常插入
                    {
                        var income = InCome.NewInCome(1, "C1", "N1", "M1", "123", 1, "千克", 5, 1, 10, 11);
                        var recordIncome = helper.Select<InCome>(p => p.id == income.id);
                        Assert.IsTrue(recordIncome.Count != 0);
                        Assert.AreEqual(recordIncome[0].id, income.id);
                        var inventory = helper.Select<Inventory>(p => p.incomeId == income.id);
                        Assert.IsTrue(inventory.Count != 0);
                        Assert.AreEqual(inventory[0].amount, 5);

                        helper.DeleteWhere<Inventory>(p => p.incomeId == income.id);
                        helper.DeleteWhere<InCome>(p => p.id == income.id);
                    }

                    //测试重复插入相同批号
                    {
                        InCome income = new InCome();
                        try
                        {
                            income = InCome.NewInCome(1, "C1", "N1", "M1", "123", 1, "千克", 5, 1, 10, 11);
                            var income1 = InCome.NewInCome(1, "C1", "N1", "M1", "123", 1, "千克", 5, 1, 10, 11);
                            Assert.Fail();
                        }
                        catch(Exception e)
                        {
                            Assert.AreEqual(e.Message , "批号已存在");
                        }
                        finally
                        {
                            helper.DeleteWhere<Inventory>(p => p.incomeId == income.id);
                            helper.DeleteWhere<InCome>(p => p.id == income.id);
                        }
                    }

                    //测试编号输入异常
                    {
                        InCome income = new InCome();
                        int incomeCount = helper.SelectAll<InCome>().Count;
                        int inventoryCount = helper.SelectAll<Inventory>().Count;
                        try
                        {
                            income = InCome.NewInCome(1, "C1", "N1", "M2", "123", 1, "千克", 5, 1, 10, 11);
                            Assert.Fail();
                        }
                        catch
                        {
                            //Assert.AreEqual(e.Message, "批号已存在");
                        }
                        finally
                        {
                            int incomeCount1 = helper.SelectAll<InCome>().Count;
                            int inventoryCount1 = helper.SelectAll<Inventory>().Count;
                            Assert.AreEqual(incomeCount, incomeCount1);
                            Assert.AreEqual(inventoryCount, inventoryCount1);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                DestroyData();
            }
        }
    }
}