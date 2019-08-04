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
    public class ManufacturerController : Controller
    {
        // GET: Manufacturer
        public ActionResult Manufacturer_add()
        {
            return View();
        }
        public ActionResult Manufacturer_list()
        {
            return View();
        }
        
    }
}