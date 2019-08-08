using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Inventory
    {
        public static Inventory Insert(int incomeId, double amount, string unit, IDbInterface dbInterface)
        {
            var inven = new Inventory() { amount = amount, incomeId = incomeId, unit = unit };
            dbInterface.Insert(inven, false);
            return inven;
        }
        public static List<string> GetMaterialCodeCode(int id)
        {
            List<string> listCode = new List<string>();
            foreach (var inCome in Position.GetInventories(id))
            {
                listCode.Add(InCome.GetMaterialCode(inCome.incomeId).code);
            }
            return listCode;
        }
        public  MaterialCode GetMaterialCode(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<MaterialCode>(helper.FindId<InCome>(id).codeId);
            }
        }
    }
}
