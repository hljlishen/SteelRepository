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

        public static MaterialCode GetMaterialCodeCode(int Pid)
        {
            //foreach (var inCome in Position.GetInventories(Pid))
            //{
            //    listCode.Add(InCome.GetMaterialCode(InCome.GetInCome(inCome.incomeId).codeId).code);
            //}
            MaterialCode materialCode = new MaterialCode();
            foreach (var inc in Position.GetInComes(Pid))
            {
                materialCode = MaterialCode.GetMaterialCode(inc.codeId);
            }
            return materialCode;
        }

        public  MaterialCode GetMaterialCode(int incomeid)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<MaterialCode>(helper.FindId<InCome>(incomeid).codeId);
            }
        }

        public static Inventory GetInventories(int invenid)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Inventory>(invenid);
            }
        }
    }
}
