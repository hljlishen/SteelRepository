using DbInterface;
using DbService;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class DepartmentController : Controller
    {
        private static Department depar;
        // GET: Department
        public ActionResult Department_list()
        {
            return View(Department.SelectAll());
        }
        public ActionResult Department_add()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Department_add(Department department)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Json(helper.Insert(department));
            }
        }
        public ActionResult Department_Update(int id)
        {
            ViewData["department"] = Department.GetDepartment(id);
            depar = Department.GetDepartment(id);
            return View();
        }
        [HttpPost]
        public JsonResult Department_Update(Department department, FormCollection collection)
        {
            depar.departmentName = department.departmentName;
            return Json(Department.Update(depar));
        }
        [HttpPost]
        public JsonResult Department_Delete(int id)
        {
            bool fa = Department.Delete(id) == 1;
            return Json(fa);
        }
    }
}