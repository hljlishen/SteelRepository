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
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["Inventorylist"] = Inventory.SelectAll();
            return View();
        }
        [HttpPost]
        public ActionResult Inventory_list(FormCollection collection)
        {
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            
            bool b = DateTime.TryParse(collection["date"], out DateTime begin);
            bool e = DateTime.TryParse(collection["date1"], out DateTime end);
            int materialCodeid = Convert.ToInt32(collection["materialCodeid"]);
            int employeeid = Convert.ToInt32(collection["Surpiusid"]);
            int unitid = Convert.ToInt32(collection["Unitid"]);

            ViewData["Inventorylist"] = null;
            ViewData["Inventorylist"] = OutCome.MulSelectCheckOutCome(b, begin, e, end, materialCodeid, employeeid);
            return View();
        }
    }
}