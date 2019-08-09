using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Department_list()
        {
            return View();
        }
    }
}