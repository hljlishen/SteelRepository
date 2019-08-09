using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Department
    {
        public static Department GetDepartment(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Department>(id);
            }
        }
        public static int Insert(Department department)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Insert(department);
            }
        }
        public static List<Department> SelectAll()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.SelectAll<Department>();
            }
        }
        public static int Update(Department department)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Update(department);
            }
        }
        public static int Delete(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                int empioyee = GetEmployee(id).Count();
                if (empioyee != 0)
                {
                    return 0;
                }
                else { 
                helper.Delete<Department>(id);
                    return 1;
                        }
            }
        }
        public static List<Employee> GetEmployee(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {

                return helper.Select<Employee>(p => p.departmentId == id);
            }
        }
    }
}
