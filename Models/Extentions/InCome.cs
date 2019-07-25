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

        public static void NewInCome(string category, string materialCode, string materialName, string materialModel, string batch, string position, string unit, double amount, int operatorId, double? price = null, string menufacture = null, byte[] qualityCertification = null)
        {

        }
    }
}
