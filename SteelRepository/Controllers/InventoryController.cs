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
            ViewData["manufacturer"] = Manufacturer.SelectAll();
            ViewData["position"] = Position.SelectAll();
            return View();
        }
        [HttpPost]
        public ActionResult Inventory_list(FormCollection collection)
        {
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["manufacturer"] = Manufacturer.SelectAll();
            ViewData["position"] = Position.SelectAll();
            string begin = collection["date"];
            string end = collection["date1"];
            var codeinput = collection["codeinput"];
            var nameinput = collection["nameinput"];
            int  positionid = Convert.ToInt32(collection["positionid"]);
            int manufacturerid = Convert.ToInt32(collection["manufacturerid"]);
            ViewData["Inventorylist"] = null;
            ViewData["Inventorylist"] = Inventory.MulSelectCheckInventory(begin,end,codeinput,nameinput,positionid,manufacturerid);
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
        public JsonResult OutCome_add(Inventory inventory, FormCollection collection)
        {
            bool b = DateTime.TryParse(collection["date"], out DateTime begin);
            var number = double.Parse(collection["number"]);
            var employeeid = Inventory.NameGetEmployeeid(collection["employeeName"]);
            var projectid =Convert.ToInt32(collection["project"]);
            var ins = collection["instructions"];
            var unit = inventory.unit;
            if (b) {
                //try {
                var newOutcome = OutCome.NewOutCome(begin, invenId, number, unit, employeeid, projectid, ins);
                //}

                return Json(1);
            }else 
            return Json(0);
        }
        public ActionResult OutComeEmployee_new()
        {
            SelectList select = new SelectList(GetPermissionsList(), "Value", "Text");
            SelectList selectState = new SelectList(GetStateList(), "Value", "Text");
            ViewBag.select = select;
            ViewBag.selectState = selectState;
            ViewData["department"] = Department.SelectAll();
            ViewData["OutComeId"] = invenId;
            return View();
        }

        [HttpPost]
        public JsonResult OutComeEmployee_new(Employee employee)
        {
            ViewData["OutComeId"] = invenId;
            return Json(Employee.Inster(employee));
        }

        private List<SelectListItem> GetPermissionsList()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            SelectListItem item0 = new SelectListItem()
            {
                Value = "1",
                Text = "一级管理员"
            };
            SelectListItem item1 = new SelectListItem()
            {
                Value = "2",
                Text = "二级管理员"
            };
            SelectListItem item2 = new SelectListItem()
            {
                Value = "3",
                Text = "三级管理员"
            };
            itemList.Add(item0);
            itemList.Add(item1);
            itemList.Add(item2);
            return itemList;
        }

        private List<SelectListItem> GetStateList()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            SelectListItem item0 = new SelectListItem()
            {
                Value = "1",
                Text = "在职"
            };
            SelectListItem item1 = new SelectListItem()
            {
                Value = "2",
                Text = "离职"
            };
            itemList.Add(item0);
            itemList.Add(item1);
            return itemList;
        }
    }
}