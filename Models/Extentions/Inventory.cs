using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Models
{
    public partial class Inventory
    {
        private static IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities());
        public static Inventory Insert(int incomeId, double amount, string unit, int positionId, IDbInterface dbInterface)
        {
            var inven = new Inventory() { amount = amount, incomeId = incomeId, unit = unit, positionId = positionId , consumptionAmount = amount};
            dbInterface.Insert(inven, false);
            return inven;
        }

        public string GetMaterialCode(IDbInterface db)
        {
            var income = db.FindId<InCome>(incomeId);
            var materialCode = db.FindId<MaterialCode>(income.codeId);
            return materialCode.code;
        }

        public string GetMaterialCode(int incomeId,IDbInterface db)
        {
            var income = db.FindId<InCome>(incomeId);
            var materialCode = db.FindId<MaterialCode>(income.codeId);
            return materialCode.code;
        }

        public static List<Inventory> InComeIdGetInventory(int incomeId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<Inventory>(p => p.incomeId == incomeId);
            }
        }
        public static List<Inventory> SelectAll()
        {
            return Dbhelper.SelectAll<Inventory>();
        }
        public static string GetMaterCodeName(int incomeId)
        {
            int  income = Dbhelper.FindId<InCome>(incomeId).codeId;
            return Dbhelper.FindId<MaterialCode>(income).code;
        }
        public static string GetNameName(int incomeId)
        {
            var nameId = Dbhelper.FindId<MaterialCode>(Dbhelper.FindId<InCome>(incomeId).codeId).materialNameId;
            return Dbhelper.FindId<Name>(nameId).materialName;
        }
        public static string GetModelName(int incomeId)
        {
            var modelId = Dbhelper.FindId<MaterialCode>(Dbhelper.FindId<InCome>(incomeId).codeId).materialModelId;
            return Dbhelper.FindId<Model>(modelId).modelName;
        }
        public static double GetInComeAmount( int incomeId)
        {
            var income = Dbhelper.FindId<InCome>(incomeId);
            return WeightConverter.Convert( income.unit,income.amount, "kg");
        }
        public static double GetOutComeNumber(int InventoryId)
        {
            double sum = 0;
            foreach ( var number in Dbhelper.Select<OutCome>(p =>p.inventoryId == InventoryId))
            {

                sum += WeightConverter.Convert(number.unit,number.number,"kg");
            }
            return sum;
        }
        public static string GetInComeUnit(int incomeId)
        {
            return Dbhelper.FindId<InCome>(incomeId).unit;
        }
        public static string GetPositionName( int Positionid)
        {
            return Dbhelper.FindId<Position>(Positionid).positionName;
        }
        public static DateTime GetInComeTime(int incomeId)
        {
            return Dbhelper.FindId<InCome>(incomeId).storageTime;
        }
        public static double GetSurpius(int Incomeid, int Inventoryid)
        {
            double dif = GetInComeAmount(Incomeid) - GetOutComeNumber(Inventoryid);
            return dif;
        }
        public static List<Inventory> GetInventoryIds(string codeinput, string nameinput)
        {
            List<Inventory> inventorys = new List<Inventory>();
            List<int> incomeId = new List<int>();
            foreach (var codeid in MaterialCode.GetMaterialId(codeinput, nameinput))
            {
                foreach (var income in Dbhelper.Select<InCome>(p => p.codeId == codeid))
                {
                    incomeId.Add(income.id);
                }
            }
            foreach (var inco in incomeId)
            {
                foreach (var inventory in Dbhelper.Select<Inventory>(p => p.incomeId == inco))
                {
                    inventorys.Add(inventory);
                }
            }
            return inventorys;
        }
        public static List<Inventory> GetTimeInventorys(DateTime begin,DateTime end)
        {
            List<Inventory> invenList = new List<Inventory>();
            foreach (var time in Dbhelper.Select<InCome>(p => p.storageTime>begin && p.storageTime<end))
            {
                foreach (var inven in Dbhelper.Select<Inventory>(p => p.incomeId == time.id)) {
                    invenList.Add(inven);
                }
            }
            return invenList;
        }
        public static List<Inventory> NameGetInventoryId(string nameinput)
        {
            List<Inventory> invenlists = new List<Inventory>();
            foreach (var name in Name.GetNames(nameinput))
            {
                foreach (var material in Dbhelper.Select<MaterialCode>(p => p.materialNameId == name.id)) {
                    foreach (var income in Dbhelper.Select<InCome>(p => p.codeId == material.id)) {
                        foreach (var Inven in Dbhelper.Select<Inventory>(p => p.incomeId == income.id)) {
                            invenlists.Add(Inven);
                        }
                    }
                }
            }
            return invenlists;
        }
        public static Inventory GetInventory(int inventoryId ,IDbInterface helper)
        {
            return helper.FindId<Inventory>(inventoryId);
        }
        public static List<Inventory> GetCodeNameInventorys(string codeinput)
        {
            List<Inventory> invenlists = new List<Inventory>();
            foreach (var code in MaterialCode.GetMaterialCodes(codeinput))
            {
                foreach (var Income in Dbhelper.Select<InCome>(p => p.codeId == code.id))
                {
                    foreach (var Inven in Dbhelper.Select<Inventory>(p => p.incomeId == Income.id))
                    {
                        invenlists.Add(Inven);
                    }
                }
            }
            return invenlists;
        }
        public static List<Inventory> MulSelectCheckInventory(bool b, DateTime begin, bool e, DateTime end, string codeinput, string nameinput)
        {
            ExpressionBuilder<Inventory> builder = new ExpressionBuilder<Inventory>();
            List<Inventory> inventorys = new List<Inventory>();
            if (b)
            {
                if (e)
                {
                    if (Name.GetNames(nameinput) != null)
                    {
                        if (MaterialCode.GetMaterialCodes(codeinput) != null)
                        {
                            if (MaterialCode.GetMaterialId(codeinput, nameinput).Count > 0)
                            {
                                foreach (var invenid in GetInventoryIds(codeinput, nameinput))
                                {
                                    builder.And(p => p.id == invenid.id);
                                }
                                var exp = builder.GetExpression();
                                var Invens = GetTimeInventorys(begin, end).Where(exp.Compile());
                                foreach (var inven in Invens)
                                {
                                    inventorys.Add(inven);
                                }
                                return inventorys;
                            }
                            else {
                                foreach (var invenid in GetCodeNameInventorys(codeinput))
                                {
                                    builder.And(p => p.id == invenid.id);
                                }
                                var exp = builder.GetExpression();
                                var Invens = GetTimeInventorys(begin, end).Where(exp.Compile());
                                foreach (var inven in Invens)
                                {
                                    inventorys.Add(inven);
                                }
                                return inventorys;
                            }
                        }
                        else
                        {
                            foreach (var ven in NameGetInventoryId(nameinput))
                            {
                                builder.And(p => p.id == ven.id);
                            }
                            var exp = builder.GetExpression();
                            var Invens = GetTimeInventorys(begin, end).Where(exp.Compile());
                            foreach (var inven in Invens)
                            {
                                inventorys.Add(inven);
                            }
                            return inventorys;
                        }
                    }
                    else
                    {
                        if (MaterialCode.GetMaterialCodes(codeinput) != null)
                        {

                            foreach (var invenid in GetCodeNameInventorys(codeinput))
                            {
                                builder.And(p => p.id == invenid.id);
                            }
                            var exp = builder.GetExpression();
                            var Invens = GetTimeInventorys(begin, end).Where(exp.Compile());
                            foreach (var inven in Invens)
                            {
                                inventorys.Add(inven);
                            }
                            return inventorys;
                        }
                        else
                        {
                            foreach (var inven in GetTimeInventorys(begin, end))
                            {
                                inventorys.Add(inven);
                            }
                            return inventorys;
                        }
                    }
                }
                else
                {
                    if (Name.GetNames(nameinput) != null)
                    {
                        if (MaterialCode.GetMaterialCodes(codeinput) != null)
                        {
                            if (MaterialCode.GetMaterialId(codeinput, nameinput).Count > 0)
                            {
                                foreach (var invenid in GetInventoryIds(codeinput, nameinput))
                                {
                                    builder.And(p => p.id == invenid.id);
                                }
                                var exp = builder.GetExpression();
                                var Invens = GetTimeInventorys(begin, DateTime.MaxValue).Where(exp.Compile());
                                foreach (var inven in Invens)
                                {
                                    inventorys.Add(inven);
                                }
                                return inventorys;
                            }
                            else
                            {
                                foreach (var invenid in GetCodeNameInventorys(codeinput))
                                {
                                    builder.And(p => p.id == invenid.id);
                                }
                                var exp = builder.GetExpression();
                                var Invens = GetTimeInventorys(begin, DateTime.MaxValue).Where(exp.Compile());
                                foreach (var inven in Invens)
                                {
                                    inventorys.Add(inven);
                                }
                                return inventorys;
                            }
                        }
                        else
                        {
                            foreach (var ven in NameGetInventoryId(nameinput))
                            {
                                builder.And(p => p.id == ven.id);
                            }
                            var exp = builder.GetExpression();
                            var Invens = GetTimeInventorys(begin, DateTime.MaxValue).Where(exp.Compile());
                            foreach (var inven in Invens)
                            {
                                inventorys.Add(inven);
                            }
                            return inventorys;
                        }
                    }
                    else
                    {
                        if (MaterialCode.GetMaterialCodes(codeinput) != null)
                        {

                            foreach (var invenid in GetCodeNameInventorys(codeinput))
                            {
                                builder.And(p => p.id == invenid.id);
                            }
                            var exp = builder.GetExpression();
                            var Invens = GetTimeInventorys(begin, DateTime.MaxValue).Where(exp.Compile());
                            foreach (var inven in Invens)
                            {
                                inventorys.Add(inven);
                            }
                            return inventorys;
                        }
                        else
                        {
                            foreach (var inven in GetTimeInventorys(begin, DateTime.MaxValue))
                            {
                                inventorys.Add(inven);
                            }
                            return inventorys;
                        }
                    }
                }
            }
            else
            {
                if (e)
                {
                    if (Name.GetNames(nameinput) != null)
                    {
                        if (MaterialCode.GetMaterialCodes(codeinput) != null)
                        {
                            if (MaterialCode.GetMaterialId(codeinput, nameinput).Count > 0)
                            {
                                foreach (var invenid in GetInventoryIds(codeinput, nameinput))
                                {
                                    builder.And(p => p.id == invenid.id);
                                }
                                var exp = builder.GetExpression();
                                var Invens = GetTimeInventorys(DateTime.MinValue, end).Where(exp.Compile());
                                foreach (var inven in Invens)
                                {
                                    inventorys.Add(inven);
                                }
                                return inventorys;
                            }
                            else
                            {
                                foreach (var invenid in GetCodeNameInventorys(codeinput))
                                {
                                    builder.And(p => p.id == invenid.id);
                                }
                                var exp = builder.GetExpression();
                                var Invens = GetTimeInventorys(DateTime.MinValue, end).Where(exp.Compile());
                                foreach (var inven in Invens)
                                {
                                    inventorys.Add(inven);
                                }
                                return inventorys;
                            }
                        }
                        else
                        {
                            foreach (var ven in NameGetInventoryId(nameinput))
                            {
                                builder.And(p => p.id == ven.id);
                            }
                            var exp = builder.GetExpression();
                            var Invens = GetTimeInventorys(DateTime.MinValue, end).Where(exp.Compile());
                            foreach (var inven in Invens)
                            {
                                inventorys.Add(inven);
                            }
                            return inventorys;
                        }
                    }
                    else
                    {
                        if (MaterialCode.GetMaterialCodes(codeinput) != null)
                        {

                            foreach (var invenid in GetCodeNameInventorys(codeinput))
                            {
                                builder.And(p => p.id == invenid.id);
                            }
                            var exp = builder.GetExpression();
                            var Invens = GetTimeInventorys(DateTime.MinValue, end).Where(exp.Compile());
                            foreach (var inven in Invens)
                            {
                                inventorys.Add(inven);
                            }
                            return inventorys;
                        }
                        else
                        {
                            return GetTimeInventorys(DateTime.MinValue, end);
                        }
                    }
                }
                else
                {
                    if (Name.GetNames(nameinput) != null)
                    {
                        if (MaterialCode.GetMaterialCodes(codeinput) != null)
                        {
                            if (MaterialCode.GetMaterialId(codeinput, nameinput).Count > 0)
                            {
                                return GetInventoryIds(codeinput, nameinput);
                            }
                            else
                            {
                                return GetCodeNameInventorys(codeinput);
                            }
                        }
                        else
                        {
                            return NameGetInventoryId(nameinput);
                        }
                    }
                    else
                    {
                        if (MaterialCode.GetMaterialCodes(codeinput) != null)
                        {
                            return GetCodeNameInventorys(codeinput);
                        }
                        else
                        {
                            return inventorys;
                        }
                    }
                }
            }
        }
    }
}
