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
        private static Inventory inventorysta;
        private static int invenId;
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
            SelectList select = new SelectList(GetUnitList(), "Value", "Text");
            ViewBag.select = select;
            ViewData["employee"] = Employee.SelectAll();
            ViewData["name"] = IndexController.adminName();
            inventorysta = Inventory.GetInventory(id);
            ViewData["project"] = Project.SelectAll();
            invenId = id;
            return View();
        }
        private List<SelectListItem> GetUnitList()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem()
            {
                Value = "g",
                Text = "g"
            };
            SelectListItem item2 = new SelectListItem()
            {
                Value = "kg",
                Text = "kg"
            };
            itemList.Add(item1);
            itemList.Add(item2);
            return itemList;
        }
         [HttpPost]
        public JsonResult OutCome_add( Inventory inventory, FormCollection collection)
        {
            bool b = DateTime.TryParse(collection["date"], out DateTime begin);
            var number = Convert.ToInt32(collection["number"]);
            var employeeid =Convert.ToInt32(collection["employeeName"]);
            var projectid =Convert.ToInt32(collection["project"]);
            var instruction = collection["instruction"];
            var ins = collection["instructions"];
            var unit = inventory.unit;
            if (b) {
                //try {
                    var newOutcome = OutCome.NewOutCome(begin, invenId, number, unit, employeeid, projectid, instruction);
                //}
            
                return Json(1);
            }else 
            return Json(0);
        }
    }
}