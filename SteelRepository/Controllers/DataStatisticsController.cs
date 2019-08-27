﻿using Models;
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
            Employee.NoJudge();
            ViewData["amount"] = Inventory.StatisticAmount();
            ViewData["project"] = Project.StatisticPrice();
            ViewData["consumptionAmount"] = Inventory.StatisticConsumptionAmount();
            return View();
        }
    }
}