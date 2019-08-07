using DbInterface;
using DbService;
using System;
using System.Collections.Generic;

namespace Models
{
    public partial class InCome
    {
        public Name GetName()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Name>(helper.FindId<MaterialCode>(codeId).materialNameId);
            }
        }

        public Model GetModel()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Model>(helper.FindId<MaterialCode>(codeId).materialModelId);
            }
        }
        public Category GetCategory()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Category>(categoryId);
            }
        }

        public MaterialCode GetMaterialCode()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<MaterialCode>(codeId);
            }
        }

        public Employee GetOperator()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Employee>(operatorId);
            }
        }

        public Manufacturer GetManufacture()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if (menufactureId == null)
                    return null;
                return helper.FindId<Manufacturer>(menufactureId.Value);
            }
        }

        public IEnumerable<RecheckReport> GetRecheckReports()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<RecheckReport>(p => p.incomeId == id);
            }
        }

        public Position GetPosition()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Position>(positionId);
            }
        }
        public static InCome NewInCome(InCome inCome, string materialCode, string materialName, string materialModel, string priceMeasure = "千克", List<byte[]> qualityCertification = null)
        {
            //using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            //{
            //    MaterialCode mCode;
            //    try
            //    {
            //        mCode = MaterialCode.Insert(materialCode, materialName, materialModel, helper);
            //        helper.Commit();
            //    }
            //    catch(Exception e)
            //    {
            //        throw e;
            //    }

            //    if (BatchIdExist(inCome.batch, helper)) throw new Exception("批号已存在");

            //    //写入入库
            //    var income = new InCome() { categoryId = inCome.categoryId, batch = inCome.batch, codeId = mCode.id, positionId = inCome.positionId, unit = inCome.unit, amount = inCome.amount, operatorId = inCome.operatorId, unitPrice =inCome.unitPrice, menufactureId = inCome.menufactureId, storageTime = inCome.storageTime, priceMeasure = priceMeasure };
            //    helper.Insert(income);
            //    //写入库存
            //    //var inventory = new Inventory() { amount = amount, incomeId = income.id , unit = measure};
            //    //helper.Insert(inventory, false);
            //    var inventory = Inventory.Insert(income.id, inCome.amount, inCome.unit, helper);

            //    helper.Commit();
            //    return income;
            //}
            //    //写入质量报告图片
            //    if(qualityCertification != null)
            //    {
            //        foreach (var item in qualityCertification)
            //        {
            //            QualityCertificationReportImg.Insert(income.id, item, helper);
            //        }
            //    }
            return NewInCome(inCome.storageTime, inCome.categoryId, materialCode, materialName, materialModel, inCome.batch, inCome.positionId, inCome.unit, inCome.amount, inCome.operatorId, inCome.unitPrice, inCome.priceMeasure, inCome.menufactureId, qualityCertification);
        }
            

        
        public static InCome NewInCome(DateTime dateTime, int categoryId, string materialCode, string materialName, string materialModel, string batch, int positionId, string measure, double amount, int operatorId, double? price = null, string priceMeasure = "千克", int? menufactureId = null, List<byte[]> qualityCertification = null)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                MaterialCode mCode;
                try
                {
                    mCode = MaterialCode.Insert(materialCode, materialName, materialModel, helper);
                    helper.Commit();
                }
                catch (Exception e)
                {
                    throw e;
                }

                if (BatchIdExist(batch, helper)) throw new Exception("批号已存在");

                //写入入库
                var income = new InCome() { categoryId = categoryId, batch = batch, codeId = mCode.id, positionId = positionId, unit = measure, amount = amount, operatorId = operatorId, unitPrice = price, menufactureId = menufactureId, storageTime = dateTime, priceMeasure = priceMeasure };
                var income = new InCome() { categoryId = inCome.categoryId, batch = inCome.batch, codeId = mCode.id, positionId = inCome.positionId, unit = inCome.unit, amount = inCome.amount, operatorId = inCome.operatorId, unitPrice =inCome.unitPrice, menufactureId = inCome.menufactureId, storageTime = inCome.storageTime, priceMeasure = inCome.priceMeasure };
                helper.Insert(income);

                //写入质量报告图片
                if (qualityCertification != null)
                {
                    foreach (var item in qualityCertification)
                    {
                        QualityCertificationReportImg.Insert(income.id, item, helper);
                    }
                }

                //写入复检报告图片
                if (recheckReport != null)
                {
                    foreach (var item in recheckReport)
                    {
                        RecheckReportImg.Insert(income.id,item,helper);
                    }
                }

                //写入库存
                //var inventory = new Inventory() { amount = amount, incomeId = income.id , unit = measure};
                //helper.Insert(inventory, false);
                var inventory = Inventory.Insert(income.id, amount, measure, helper);

                helper.Commit();
                return income;
            }
        }

        public static bool BatchIdExist(string batch, IDbInterface helper)
        {
            var income = helper.FindFirst<InCome, string>("batch", batch);

            return income != null;
        }

        public static List<InCome> GetInComes()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.SelectAll<InCome>();
            }
        }

        public static InCome Update(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<InCome>(id);
            }
        }
    }
}
