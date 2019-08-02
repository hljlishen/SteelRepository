using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class LoginController : Controller
    {
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
                Session["permissions"] = e.permissions;
                Session["IsLogin"] = true;
                Session["id"] = e.id;
            }
            return Json(IsLogin);
        }

        public ActionResult AdminIndex()
        {
            return View();
        }

        public ActionResult UserRegistered()
        {
            return View();
        }
    }
}