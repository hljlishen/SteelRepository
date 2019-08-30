using DbInterface;
using DbService;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace SteelRepository.Controllers
{
    public class DepartmentController : Controller
    {
        private static Department depar;
        private static int departmentId;
        // GET: Department
        public ActionResult Department_list()
        {
            ViewData["LoginEmployee"] = IndexController.LoginEmployee();
            Employee.NoJudge();
            ViewData["Employee"] = Employee.SelectAll();
            return View(Department.SelectAll());
        }

        [HttpPost]
        public ActionResult Department_list(int id)
        {
            ViewData["Employee"] = Employee.Select(id);
            return View(Department.SelectAll());
        }

        public ActionResult Company_add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Company_add(Department department)
        {
            department.nodeLevel = 1;
            department.parentNodeId = null;
            return Json(Department.Insert(department));
        }

        public ActionResult Department_add(int id)
        {
            departmentId = id;
            return View();
        }

        [HttpPost]
        public JsonResult Department_add(Department department)
        {
            department.nodeLevel = 2;
            department.parentNodeId = departmentId;
            return Json(Department.Insert(department));
        }

        public ActionResult Department_add2(int id)
        {
            departmentId = id;
            return View();
        }

        [HttpPost]
        public JsonResult Department_add2(Department department)
        {
            department.nodeLevel = 3;
            department.parentNodeId = departmentId;
            return Json(Department.Insert(department));
        }

        public ActionResult Department_Update(int id)
        {
            depar = Department.GetDepartment(id);
            return View(depar);
        }

        [HttpPost]
        public JsonResult Department_Update(Department department)
        {
            depar.departmentName = department.departmentName;
            return Json(Department.Update(depar));
        }

        [HttpPost]
        public JsonResult Department_Delete(int id)
        {
            return Json(Department.Delete(id));
        }

        [HttpPost]
        public JsonResult Department_Delete2(int id)
        {
            return Json(Department.DeleteChildNodes(id));
        }
    }
}