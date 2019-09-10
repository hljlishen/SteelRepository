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
                return helper.Select<Employee>(p => p.state != 4);
            }
        }
        public static List<EmployeeDepartView> SelectAllDesc()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<EmployeeDepartView> list = new List<EmployeeDepartView>();
                foreach (var emplo in helper.SqlQuery<EmployeeDepartView>("select EmployeeDepartView.* from EmployeeDepartView, Employee where EmployeeDepartView.emploId = Employee.id and Employee.state != 4 order by EmployeeDepartView.emploId desc")) {
                    list.Add(emplo);
                }
                return list;
            }
        }

        public static int Inster(Employee employee)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if(helper.Select<Employee>(p => p.number == employee.number && p.state != 4).Count <= 0)
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
                foreach (var em in helper.SelectAll<Employee>())
                {
                    if (em.number == employee.number && em.password == employee.password && em.state !=4)
                    {
                        isJudge = true;
                        UseAmountStatisticals.AddAdministratorTraffic(DateTime.Now);
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
                MultipleSeriesStatistics2D<OutCome> statistics2D = new MultipleSeriesStatistics2D<OutCome>(p => p.GetMaterialCode(helper));
                statistics2D.AddSeries("number", p => WeightConverter.Convert(p.unit, p.number, "kg")/*, StatisticsType.UserDefine, p =>
                {
                    double ret = 1;
                    foreach (var item in p)
                    {
                        ret *= item;
                    }
                    return ret;
                }*/);
                var Sum = statistics2D.GetValues(helper.Select<OutCome>(p => p.borrowerId == employeeId && p.state !=2));
                return Sum["number"];
            }
        }
    }
}
