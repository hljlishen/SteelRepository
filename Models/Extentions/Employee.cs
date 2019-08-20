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
        public static List<Employee> SelectAll()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.SelectAll<Employee>();
            }
        }

        public static int Inster(Employee employee)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Insert(employee);
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
                List<Employee> returnEm = new List<Employee>();
                employees = helper.SelectAll<Employee>();
                foreach (var em in employees)
                {
                    if (em.number == employee.number && em.password == employee.password)
                        return em;
                }
                return null;
            }
        }

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

        public static List<OutCome> GetOutComes(int employeeId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
                return helper.Select<OutCome>(p => p.borrowerId == employeeId);
        }

        public static Dictionary<string, double> StatisticAmount(int employeeId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var Sum = new Dictionary<string, Dictionary<string, double>>();
                foreach (var outcome in GetOutComes(employeeId))
                {
                    int incomeId = Inventory.GetInventory(outcome.inventoryId, helper).incomeId;
                    MultipleSeriesStatistics2D<Inventory> statistics2D = new MultipleSeriesStatistics2D<Inventory>(p => p.GetMaterialCode(incomeId,helper));
                    statistics2D.AddSeries("consumptionAmount", p => p.consumptionAmount);
                    Sum = statistics2D.GetValues(helper.Select<Inventory>(p => p.incomeId == incomeId && p.consumptionAmount > 0));
                }
                return Sum["consumptionAmount"];
            }
        }
    }
}
