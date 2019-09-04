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
                if(helper.Select<Project>(p => p.projectCode == project.projectCode).Count <= 0)
                    return helper.Insert(project);
                return 0;
            }
        }
        public static List<Project> SelectAll()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.SelectAll<Project>();
            }
        }

        public static int Update(Project project)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Update(project);
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
    }
}
