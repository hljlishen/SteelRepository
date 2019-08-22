using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class IndexController : Controller
    {
        private static string adminname;
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserLogin(Employee employee)
        {
            bool IsLogin = false;
            Employee e = Employee.Login(employee);
            if (e != null)
            {
                IsLogin = true;
                Session["name"] = e.name;
                adminname = e.name;
                Session["permissions"] = e.permissions;
                Session["IsLogin"] = true;
                Session["id"] = e.id;
            }
            return Json(IsLogin);
        }

        public ActionResult UserRegistered()
        {
            ViewData["department"] = Department.SelectAll();
            return View();
        }

        [HttpPost]
        public JsonResult UserRegistered(Employee employee)
        {
            employee.permissions = 3;
            employee.state = 3;
            return Json(Employee.Inster(employee));
        }

        public static string adminName()
        {
            return adminname;
        }
    }
}