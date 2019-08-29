using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tools.Statistics;

namespace Models
{
    public partial class Employee
    {
        private static bool isJudge = false;
        public static List<Employee> SelectAll()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.SelectAll<Employee>();
            }
        }
        public static List<EmployeeDepartView> SelectAllDesc()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<EmployeeDepartView> list = new List<EmployeeDepartView>();
                foreach (var emplo in helper.SqlQuery<EmployeeDepartView>("select EmployeeDepartView.* from EmployeeDepartView order by EmployeeDepartView.emploId desc")) {
                    list.Add(emplo);
                }
                return list;
            }
        }

        public static int Inster(Employee employee)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if(helper.Select<Employee>(p => p.number == employee.number).Count <= 0)
                    return helper.Insert(employee);
                return 0;
            }
        }

        public static Employee FindId(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Employee>(id);
            }
        }

        public static int Update(Employee employee)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Update(employee);
            }
        }

        public static int Delete(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Delete<Employee>(id);
            }
        }

        public static Employee Login(Employee employee)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<Employee> employees = new List<Employee>();
                employees = helper.SelectAll<Employee>();
                foreach (var em in employees)
                {
                    if (em.number == employee.number && em.password == employee.password)
                    {
                        isJudge = true;
                        return em;
                    }
                }
                return null;
            }
        }

        public static void NoJudge() => isJudge = false;

        public static bool JudgeLogin() => isJudge;

        public static int DeleteState(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                Employee employee = FindId(id);
                employee.state = 1;
                return helper.Update(employee);
            }
        }

        public static List<Employee> Select(int depId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<Employee>(p => p.departmentId == depId);
            }
        }

        public static List<OutCome> GetOutComes(int employeeId, IDbInterface helper)
        {
            return helper.Select<OutCome>(p => p.borrowerId == employeeId);
        }

        public static Dictionary<string, double> StatisticPrice(int employeeId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                //var Sum = new Dictionary<string, Dictionary<string, double>>();
                //foreach (var outcome in GetOutComes(employeeId, helper))
                //{
                //    int incomeId = Inventory.GetInventory(outcome.inventoryId, helper).incomeId;
                //    MultipleSeriesStatistics2D<OutCome> statistics2D = new MultipleSeriesStatistics2D<OutCome>(p => p.GetMaterialCode(incomeId, helper));
                //    statistics2D.AddSeries("price", p => 
                //    {
                //        return p.price == null ? 0: p.price.Value;
                //    });
                //    Sum = statistics2D.GetValues(helper.Select<OutCome>(p => p.borrowerId == employeeId));
                //}
                MultipleSeriesStatistics2D<OutCome> statistics2D = new MultipleSeriesStatistics2D<OutCome>(p => p.GetMaterialCode(helper));
                statistics2D.AddSeries("price", p => p.price == null ? 0 : p.price.Value/*, StatisticsType.UserDefine, p =>
                {
                    double ret = 1;
                    foreach (var item in p)
                    {
                        ret *= item;
                    }
                    return ret;
                }*/);
                var Sum = statistics2D.GetValues(helper.Select<OutCome>(p => p.borrowerId == employeeId && p.state !=2));
                return Sum.Keys.Contains("price") ? Sum["price"] : null;
            }
        }
    }
}
