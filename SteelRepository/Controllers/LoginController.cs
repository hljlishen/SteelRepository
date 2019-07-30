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
        [ValidateInput(false)]
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(Employee employee)
        {
            return View();
        }

        public ActionResult AdminIndex()
        {
            return View();
        }
    }
}