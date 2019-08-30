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
        public static int Delete(int departmentId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if (GetEmployee(departmentId).Count() != 0)
                    return 0;
                else
                    return helper.Delete<Department>(departmentId);
            }
        }

        public static int DeleteChildNodes(int departmentId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if (helper.Select<Department>(p => p.parentNodeId == departmentId).Count <= 0)
                    return helper.Delete<Department>(departmentId);
                else
                    return 0;
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
