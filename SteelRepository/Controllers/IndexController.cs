﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class IndexController : Controller
    {
        private static string adminname;
        private static List<Inventory> inventories;
        private static List<InCome> inComes;
        // GET: Index
        public ActionResult Index()
        {
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["InventoryViewlist"] = Inventory.InventoryViewSelectAll();
            ViewData["manufacturer"] = Manufacturer.SelectAll();
            ViewData["position"] = Position.SelectAll();
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["manufacturer"] = Manufacturer.SelectAll();
            ViewData["position"] = Position.SelectAll();
            string begin = collection["date"];
            string end = collection["date1"];
            var codeinput = collection["codeinput"];
            var nameinput = collection["nameinput"];
            int positionid = Convert.ToInt32(collection["positionid"]);
            int manufacturerid = Convert.ToInt32(collection["manufacturerid"]);
            ViewData["InventoryViewlist"] = null;
            ViewData["InventoryViewlist"] = Inventory.MulSelectCheckInventory(begin, end, codeinput, nameinput, positionid, manufacturerid);
            return View();
        }

        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserLogin(Employee employee)
        {
            bool IsLogin = false;
            Employee e = Employee.Login(employee);
            if (e != null && e.permissions != 3)
            {
                IsLogin = true;
                Session["name"] = e.name;
                adminname = e.name;
                Session["permissions"] = e.permissions;
                Session["IsLogin"] = true;
                Session["id"] = e.id;
                inventories = Inventory.SelectRemaining();
                if (inventories != null)
                    Session["RemindCount"] = inventories.Count();
                else
                    Session["RemindCount"] = 0;
                inComes = InCome.SelectRemind();
                if (inComes != null)
                    Session["inComes"] = inComes.Count();
                else
                    Session["inComes"] = 0;
            }
            return Json(IsLogin);
        }

        public ActionResult UserRegistered()
        {
            ViewData["department"] = Department.SelectAll();
            return View();
        }

        [HttpPost]
        public JsonResult UserRegistered(Employee employee)
        {
            employee.permissions = 3;
            employee.state = 3;
            return Json(Employee.Inster(employee));
        }

        public static string adminName()
        {
            return adminname;
        }

        public static List<Inventory> Remind()
        {
            return inventories;
        }

        public static List<InCome> Recheck()
        {
            return inComes;
        }
    }
}