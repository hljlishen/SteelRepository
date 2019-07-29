using DbInterface;
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
    }
}
