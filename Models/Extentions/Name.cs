using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Name
    {
        public static Name GetName(string name)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindFirst<Name, string>("materialName", name);
            }
        }

        public static Name Insert(string name, IDbInterface helper)
        {
            var record = helper.FindFirst<Name, string>("materialName", name);
            if (record != null) return record;
            var ret = new Name() { materialName = name };
            helper.Insert(ret, false);
            return ret;
        }
    }
}
