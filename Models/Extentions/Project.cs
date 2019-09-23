using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Statistics;

namespace Models
{
    public partial class Project
    {
        public static Project GetProject(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Project>(id);
            }
        }
        public static int Insert(Project project)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if(helper.Select<Project>(p => p.projectCode == project.projectCode && p.state == 1).Count <= 0)
                    return helper.Insert(project);
                return 0;
            }
        }
        public static List<Project> SelectAll()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<Project>(p => p.state != 2);
            }
        }
        public static List<Project> SelectDescAll() {
                using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
                {
                    List<Project> list = new List<Project>();
                    foreach (var project in helper.SqlQuery<Project>("select Project.* from Project where Project.state != 2 order by Project.id desc"))
                    {
                        list.Add(project);
                    }
                    return list;
                }
        }

        public static int Update(Project project)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if (helper.Select<Project>(p => p.projectCode == project.projectCode && p.state == 1 && p.id != project.id).Count <= 0)
                    return helper.Update(project);
                return 0;
            }
        }

        public static Dictionary<string, double> StatisticPrice()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                MultipleSeriesStatistics2D<OutCome> statistics2D = new MultipleSeriesStatistics2D<OutCome>(p => p.GetMaterialProject(helper));
                statistics2D.AddSeries("price", p => p.price == null ? 0 : p.price.Value/10000);
                var Sum = statistics2D.GetValues(helper.Select<OutCome>(p => p.state != 2));
                return Sum.Keys.Contains("price") ? Sum["price"] : null;
            }
        }
        public static bool ProjectCodeExist(string pnumber) {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var project = helper.FindFirst<Project, string>("projectCode", pnumber);

                return project == null;
            }
        }
    }
}
