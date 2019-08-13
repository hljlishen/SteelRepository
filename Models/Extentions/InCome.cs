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
        public Name GetName(int codeId)
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

        public Model GetModel(int codeId)
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

        public Category GetCategory(int categoryId)
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

        public static MaterialCode GetMaterialCode(int codeId)
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

        public Employee GetOperator(int operatorId)
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

        public Manufacturer GetManufacture(int? menufactureId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Manufacturer>((int)menufactureId);
            }
        }

        public IEnumerable<RecheckReport> GetRecheckReports()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<RecheckReport>(p => p.incomeId == id);
            }
        }

        //public Position GetPosition()
        //{
        //    using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
        //    {
        //        return helper.FindId<Position>(positionId);
        //    }
        //}

        public Position GetPosition(int incomeId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                Position position = new Position();
                foreach (var inv in Inventory.InComeIdGetInventory(incomeId))
                {
                    position = Position.GetPosition(inv.positionId);
                }
                return position;
            }
        }

        public static InCome NewInCome(InCome inCome, int positionId, string materialCode, string materialName, string materialModel, DateTime RecheckTime, List<byte[]> qualityCertification = null, List<byte[]> recheckReport = null)
        {
            return NewInCome(inCome.storageTime, inCome.reviewCycle, inCome.categoryId, materialCode, materialName, materialModel, inCome.batch, positionId, inCome.unit, inCome.amount, inCome.operatorId, RecheckTime, inCome.unitPrice, inCome.priceMeasure, inCome.menufactureId, qualityCertification, recheckReport);
        }

        public static InCome NewInCome(DateTime dateTime, double recheckCycle, int categoryId, string materialCode, string materialName, string materialModel, string batch, int positionId, string measure, double amount, int operatorId, DateTime RecheckTime, double? price = null, string priceMeasure = "kg", int? menufactureId = null, List<byte[]> qualityCertification = null, List<byte[]> recheckReport = null)
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
                var income = new InCome() { categoryId = categoryId, batch = batch, codeId = mCode.id, unit = measure, amount = amount, operatorId = operatorId, unitPrice = price, menufactureId = menufactureId, storageTime = dateTime, priceMeasure = priceMeasure ,reviewCycle=recheckCycle};
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
                    RecheckReport.Insert(income.id, RecheckTime, recheckReport);
                }
                //写入库存;
                var inventory = Inventory.Insert(income.id, amount, measure, positionId, helper);
                helper.Commit();
                return income;
            }
        }


        public static InCome NewInCome(DateTime dateTime, int categoryId, string materialCode, string materialName, string materialModel, string batch, int positionId, string measure, double amount , int operatorId, double? price = null, string priceMeasure = "kg", int? menufactureId = null, List<byte[]> qualityCertification = null,List<byte[]> recheckReport = null)
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
                var income = new InCome() { categoryId = categoryId, batch = batch, codeId = mCode.id, unit = measure, amount = amount, operatorId = operatorId, unitPrice = price, menufactureId = menufactureId, storageTime = dateTime, priceMeasure = priceMeasure};
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
                //写入库存;
                var inventory = Inventory.Insert(income.id, amount, measure, positionId, helper);
                helper.Commit();
                return income;
            }
        }

        public static int Update(InCome inCome)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                double? kgPrice = 0;
                double weight = 0;
                if (BatchIdExist(inCome.batch,inCome.id ,helper)) throw new Exception("批号已存在");
                //修改单价
                foreach (var outcome in OutCome.InComeIdSelect(inCome.id, helper))
                {
                    if (inCome.priceMeasure == "g         ")
                        kgPrice = inCome.unitPrice * 1000;
                    else
                        kgPrice = inCome.unitPrice;
                    if (outcome.unit == "g")
                        weight = outcome.number * 1000;
                    else
                        weight = outcome.number;
                    outcome.price = kgPrice * weight;
                    helper.Update(outcome); 
                }
                return helper.Update(inCome);
            }
        }

        public static bool BatchIdExist(string batch, IDbInterface helper)
        {
            var income = helper.FindFirst<InCome, string>("batch", batch);

            return income != null;
        }

        public static bool BatchIdExist(string batch,int incomeId ,IDbInterface helper)
        {
            var income = helper.FindFirst<InCome, string>("batch", batch);
            if (income != null)
            {
                if (income.id != incomeId)
                    return true;
            }
            
            return false;
        }

        public static List<InCome> GetInComes()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.SelectAll<InCome>();
            }
        }
        public static InCome GetInCome(int incomeid)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<InCome>(incomeid);
            }
        }
        public static InCome Selete(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<InCome>(id);
            }
        }
    }
}
