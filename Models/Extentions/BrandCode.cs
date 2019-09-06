using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class BrandCode
    {
        public static BrandCode Insert(string brandCode)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var record = helper.FindFirst<BrandCode, string>("brandCodeName", brandCode);
                if (record != null) return record;
                var ret = new BrandCode() { brandCodeName = brandCode };
                helper.Insert(ret);
                return ret;
            }
        }
    }
}
