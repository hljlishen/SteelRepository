using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Inventory_list()
        {
            ViewData["Inventorylist"] = Inventory.SelectAll();
            return View();
        }
    }
}