﻿using Models;
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

        public ActionResult Index1()
        {
            return View();
        }

        public ActionResult AllowanceRemind()
        {
            return View(Inventory.SelectRemaining());
        }

        public ActionResult RecheckRemind()
        {
            return View(InCome.SelectRemind());
        }
    }
}