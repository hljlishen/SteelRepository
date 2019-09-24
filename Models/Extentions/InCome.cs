using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using Tools;

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

        public static InCome NewInCome(InCome inCome, int positionId, string materialCode, string materialName, string materialModel, string recheckBasis, string recheckOrderNo, DateTime RecheckTime, List<byte[]> qualityCertification = null, List<byte[]> recheckReport = null)
        {
            return NewInCome(recheckBasis, recheckOrderNo, inCome.storageTime, inCome.reviewCycle, inCome.categoryId,inCome.brandCodeId.Value , materialCode, materialName, materialModel, inCome.batch, positionId, inCome.unit, inCome.amount, inCome.operatorId, RecheckTime, inCome.unitPrice, inCome.priceMeasure, inCome.menufactureId, qualityCertification, recheckReport);
        }

        public static InCome NewInCome(string recheckBasis,string recheckOrderNo,DateTime dateTime, double recheckCycle, int categoryId, int brandCodeId, string materialCode, string materialName, string materialModel, string batch, int positionId, string measure, double amount, int operatorId, DateTime RecheckTime, double? price = null, string priceMeasure = "kg", int? menufactureId = null, List<byte[]> qualityCertification = null, List<byte[]> recheckReport = null)
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
                var income = new InCome() { categoryId = categoryId, brandCodeId = brandCodeId, batch = batch, codeId = mCode.id, unit = measure, amount = amount, operatorId = operatorId, unitPrice = price, menufactureId = menufactureId, storageTime = dateTime, priceMeasure = priceMeasure ,reviewCycle=recheckCycle};
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
                    RecheckReport.Insert(income.id, RecheckTime, recheckReport, recheckBasis, recheckOrderNo);
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

        public static int Update(InCome inCome,string name,string model,string code,int positionId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                MaterialCode mCode;
                try
                {
                    mCode = MaterialCode.Insert(code, name, model, helper);
                    helper.Commit();
                }
                catch (Exception e)
                {
                    return -2;
                    //throw e;
                }
                inCome.codeId = mCode.id;
                double? kgPrice = 0;
                //修改库存
                try {
                    var inventory = Inventory.Update(inCome.id, inCome.amount, inCome.unit, positionId, helper);
                }
                catch (Exception ex) {
                    return -3;
                    //throw ex;
                }
                //修改出库总价
                foreach (var outcome in OutCome.InComeIdSelect(inCome.id, helper))
                {
                    if (inCome.priceMeasure == "g         " && outcome.unit == "g")
                        kgPrice = outcome.number*inCome.unitPrice;
                    else if (inCome.priceMeasure == "kg        " && outcome.unit == "g")
                        kgPrice = outcome.number * (inCome.unitPrice / 1000.0);
                    else if (inCome.priceMeasure == "kg        " && outcome.unit == "kg")
                        kgPrice = outcome.number * inCome.unitPrice;
                    else if(inCome.priceMeasure == "g         " && outcome.unit == "kg")
                        kgPrice = outcome.number * 1000 * inCome.unitPrice;
                    outcome.price = kgPrice;
                    helper.Update(outcome); 
                }
                
                return helper.Update(inCome);
            }
        }
        public static  string OutQty(int IncomeId,string unit)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                double Sum = 0;
                foreach (var outCome in helper.SelectAll<OutcomeQueryView>()) {
                    if (outCome.incoId == IncomeId && outCome.state == 1) { 
                    Sum += WeightConverter.Convert(outCome.unit,outCome.number,unit);
                    }
                }
                return "(出库总量为" + Sum +" "+ unit+")";
            }
        }

        public static bool BatchIdExist(string batch, IDbInterface helper)
        {
            var income = helper.FindFirst<InCome, string>("batch", batch);

            return income != null;
        }

        public static bool BatchIdExist(string batch)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var income = helper.FindFirst<InCome, string>("batch", batch);

                return income == null;
            }
        }

        public static List<InCome> GetInComes()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.SelectAll<InCome>();
            }
        }

        public static List<InComeView> GetInComesView()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.SelectAll<InComeView>();
            }
        }

        public static InComeView GetInComesView(int incomeId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<InComeView>(p => p.incoId == incomeId)[0];
            }
        }

        public static List<InComeView> GetInComeViewDesc()
        {
            List<InComeView> liDesc = new List<InComeView>();
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var ret = helper.SqlQuery<InComeView>("select InComeView.* from InComeView order by InComeView.storageTime desc");
                foreach (var income in ret)
                {
                    liDesc.Add(income);
                }
                return liDesc;
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
        public static InComeView SeleteInComeView(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<InComeView>(p => p.incoId == id)[0];
            }
        }

        public static List<InCome> SelectRemaining()
        {
            List<InCome> inComes = new List<InCome>();
            foreach (var inc in GetInComes())
            {
                string datetime = RecheckReport.GetMaxDate(RecheckReport.GetRecheckReports(inc.id), inc.reviewCycle);
                if (datetime == "未添加复检报告")
                    return null;
                inc.storageTime = DateTime.Parse(datetime);
                inComes.Add(inc);
            }
            return inComes;
        }

        public static List<InComeView> SelectRemind()
        {
            List<InComeView> inComes = new List<InComeView>();
            foreach (var inc in GetInComesView())
            {
                string datetime = RecheckReport.GetMaxDate(RecheckReport.GetRecheckReports(inc.incoId), inc.reviewCycle);
                if (datetime != "未添加复检报告")
                {
                    DateTime d1 = Convert.ToDateTime(datetime);
                    DateTime d2 = DateTime.Now;
                    DateTime d3 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d1.Year, d1.Month, d1.Day));
                    DateTime d4 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d2.Year, d2.Month, d2.Day));
                    int days = (d3 - d4).Days;
                    if (days <= 30)
                    {
                        inc.storageTime = Convert.ToDateTime(datetime);
                        inComes.Add(inc);
                    }

                }
            }
            return inComes;
        }
        public static List<InComeView> MulSelectCheckInCome(string begin, string end, int MaterCodeid, int manufacturerid)
        {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                ExpressionBuilder<InComeView> builder = new ExpressionBuilder<InComeView>();
                if (begin != "")
                {
                    DateTime begintime = Convert.ToDateTime(begin);
                    builder.And(p => p.storageTime >= begintime);
                }
                if (end != "")
                {
                    DateTime endtime = Convert.ToDateTime(end);
                    builder.And(p => p.storageTime <= endtime);
                }
                if (MaterCodeid != 0)
                    builder.And(p => p.materId == MaterCodeid);
                if (manufacturerid != 0)
                    builder.And(p => p.manuId == manufacturerid);
                var exp = builder.GetExpression();
                if (exp == null) return Dbhelper.SelectAll<InComeView>();
                return Dbhelper.Select(exp);
            }
        }
        public static double GetInventoryAmount(int InComeId) {
            using (IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Dbhelper.FindFirst<Inventory, int>("incomeId", InComeId).amount;
            }
        }
    }
}
