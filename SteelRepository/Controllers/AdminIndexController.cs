using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class AdminIndexController : Controller
    {
        public ActionResult AdminIndex()
        {
            return View(); 
        }

        public ActionResult AllowanceRemind()
        {
            return View(IndexController.Remind());
        }

        public ActionResult RecheckRemind()
        {
            return View(IndexController.Recheck());
        }
    }
}