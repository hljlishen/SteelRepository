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
        private static  IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities());
        public static Name GetName(string name)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindFirst<Name, string>("materialName", name);
            }
        }
        public static List<Name> GetNames(string name)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if (name == "")
                {
                    return null;
                }
                return helper.Select<Name>(p => p.materialName == name);
            }
        }

        public static Name Insert(string name)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var record = helper.FindFirst<Name, string>("materialName", name);
                if (record != null) return record;
                var ret = new Name() { materialName = name };
                helper.Insert(ret);
                return ret;
            }
        }
        public static List<Name> SelectAll()
        {
            return Dbhelper.SelectAll<Name>();
        }
    }
}
