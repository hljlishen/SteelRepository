using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class DataStatisticsController : Controller
    {
        // GET: DataStatistics
        public ActionResult DataStatistics()
        {
            ViewData["inventory"] = Inventory.StatisticAmount();
            return View();
        }
    }
}