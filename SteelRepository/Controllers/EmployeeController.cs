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
    }
}