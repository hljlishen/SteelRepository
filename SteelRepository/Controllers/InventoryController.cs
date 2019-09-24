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
        private static int invenId;
        public ActionResult Inventory_list()
        {
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["InventoryViewlist"] = Inventory.InventoryViewSelectAll();
            ViewData["manufacturer"] = Manufacturer.SelectAll();
            ViewData["position"] = Position.SelectAll();
            ViewData["permissions"] = Session["permissions"];
            return View();
        }
        [HttpPost]
        public ActionResult Inventory_list(FormCollection collection)
        {
            ViewData["permissions"] = Session["permissions"];
            Employee.NoJudge();
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["manufacturer"] = Manufacturer.SelectAll();
            ViewData["position"] = Position.SelectAll();
            string begin = collection["date"];
            string end = collection["date1"];
            var codeinput = collection["codeinput"];
            var nameinput = collection["nameinput"];
            int  positionid = Convert.ToInt32(collection["positionid"]);
            int manufacturerid = Convert.ToInt32(collection["manufacturerid"]);
            ViewData["InventoryViewlist"] = null;
            ViewData["InventoryViewlist"] = Inventory.MulSelectCheckInventory(begin, end, codeinput, nameinput, positionid, manufacturerid);
            return View();
        }
        public ActionResult OutCome_add(int id)
        {
            Employee.NoJudge();
            ViewData["Inventory"] = Inventory.GetInventoryView(id);
            SelectList select = new SelectList(GetUnitList(), "Value", "Text");
            ViewBag.select = select;
            ViewData["employee"] = Employee.SelectAllDesc();
            ViewData["name"] = IndexController.adminName();
            ViewData["project"] = Project.SelectDescAll();
            invenId = id;
            return View();
        }
        private List<SelectListItem> GetUnitList()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem()
            {
                Value = "kg",
                Text = "kg"
            };
            SelectListItem item2 = new SelectListItem()
            {
                Value = "g",
                Text = "g"
            };
            itemList.Add(item1);
            itemList.Add(item2);
            return itemList;
        }
        [HttpPost]
        public JsonResult OutCome_add(Inventory inventory, FormCollection collection)
        {

            DateTime begin = DateTime.Parse(collection["date"]);
            var number = double.Parse(collection["number"]);
            var Employeeid = int.Parse(collection["employeeId"]);
            var Projectid = int.Parse(collection["project"]);
            var ins = collection["instructions"].Trim();
            var unit = inventory.unit;
            List<InventoryView> inventories = Inventory.SelectRemaining();
            List<InComeView> inComes = InCome.SelectRemind();
            if (inventories != null)
                Session["RemindCount"] = inventories.Count();
            else
                Session["RemindCount"] = 0;
            if (inComes != null)
                Session["inComes"] = inComes.Count();
            else
                Session["inComes"] = 0;

            return Json(OutCome.NewOutCome(begin, invenId, number, unit, Employeeid, Projectid, ins));
        }
        public ActionResult OutComeEmployee_new()
        {
            Employee.NoJudge();
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
        public ActionResult OutComeProject_add()
        {
            ViewData["OutComeId"] = invenId;
            return View();
        }

        [HttpPost]
        public JsonResult OutComeProject_add(Project project)
        {
            ViewData["OutComeId"] = invenId;
            project.state = 1;
            return Json(Project.Insert(project));
        }
        public ActionResult NumberExist(string number,string unit)
        {
            return Json(Inventory.numberExist(number,unit,invenId));
        }
    }
}