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

        public static List<Model> SelectModelAll()
        {
            return Dbhelper.SelectAll<Model>();
        }

        public static List<Category> SelectCategoryAll()
        {
            return Dbhelper.SelectAll<Category>();
        }

        public static List<BrandCode> SelectBrandCodeAll()
        {
            return Dbhelper.SelectAll<BrandCode>();
        }

        public static List<MaterialCode> SelectMaterialCodeAll()
        {
            return Dbhelper.SelectAll<MaterialCode>();
        }

        public static List<RecheckReport> SelectRecheckReportAll()
        {
            List<RecheckReport> recheckReports = Dbhelper.SelectAll<RecheckReport>();
            List<RecheckReport> recheckReports1 = new List<RecheckReport>();
            foreach (var rr in recheckReports)
            {
                if (recheckReports1 == null)
                {
                    recheckReports1.Add(rr);
                }
                foreach (var rr1 in recheckReports1)
                {
                    if (rr.recheckBasis == rr1.recheckBasis)
                        continue;
                    else
                        recheckReports1.Add(rr);
                }
            }
            return recheckReports1;
        }
    }
}
