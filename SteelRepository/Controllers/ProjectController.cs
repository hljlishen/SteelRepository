using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Project_list()
        {
            return View();
        }

        public ActionResult Project_add()
        {
            return View();
        }
    }
}