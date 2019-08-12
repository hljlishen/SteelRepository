using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class OutComeController : Controller
    {
        // GET: OutCome
        public ActionResult OutCome_list()
        {
            return View(OutCome.SelectAll());
        }
        public ActionResult OutCome_Select()
        {
            return View();
        }
    }
}