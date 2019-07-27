using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Position GetPosition()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Position>(positionId);
            }
        }

        public static InCome NewInCome(int categoryId, string materialCode, string materialName, string materialModel, string batch, int positionId, string unit, double amount, int operatorId, double? price = null, int? menufactureId = null, byte[] qualityCertification = null)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                MaterialCode mCode;
                try
                {
                    mCode = MaterialCode.Insert(materialCode, materialName, materialModel, helper);
                    helper.Commit();
                }
                catch(Exception e)
                {
                    throw e;
                }

                if (BatchIdExist(batch, helper)) throw new Exception("批号已存在");

                var income = new InCome() { categoryId = categoryId, batchId = batch, codeId = mCode.id, positionId = positionId, unit = unit, number = amount, operatorId = operatorId, unitPrice = price, menufactureId = menufactureId, qualityCertificate = qualityCertification };
                helper.Insert(income);

                var inventory = new Inventory() { amount = amount, incomeId = income.id };
                helper.Insert(inventory);

                return income;
            }
        }

        public static bool BatchIdExist(string batchId, IDbInterface helper)
        {
            var income = helper.FindFirst<InCome, string>("batchId", batchId);

            return income != null;
        }
    }
}
