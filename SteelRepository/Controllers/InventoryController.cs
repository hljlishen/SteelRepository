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
        private static Inventory inventory;
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
            var codeinput = collection["codeinput"];
            var nameinput = collection["nameinput"];
            ViewData["Inventorylist"] = null;
            ViewData["Inventorylist"] = Inventory.MulSelectCheckInventory(b,begin,e,end,codeinput,nameinput);
            return View();
        }
        public ActionResult OutCome_add(int id)
        {
            ViewData["Inventory"] = Inventory.GetInventory(id);

            inventory = Inventory.GetInventory(id);
            return View();
        }
        //[HttpPost]
        //public JsonResult OutCome_add(Inventory inven, FormCollection collection)
        //{
            
        //    return Json();
        //}
    }
}