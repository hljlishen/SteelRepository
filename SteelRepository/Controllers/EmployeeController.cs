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
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Employee_list()
        {
            return View();
        }

        public ActionResult Employee_add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Employee_add(Employee employee)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Json(helper.Insert(employee));
            }
        }
    }
}