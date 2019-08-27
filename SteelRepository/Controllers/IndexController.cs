using Models;
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
            ViewData["Inventorylist"] = Inventory.SelectAll();
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            //bool b = DateTime.TryParse(collection["date"], out DateTime begin);
            //bool e = DateTime.TryParse(collection["date1"], out DateTime end);
            //var codeinput = collection["codeinput"];
            //var nameinput = collection["nameinput"];
            //ViewData["Inventorylist"] = null;
            //ViewData["Inventorylist"] = Inventory.MulSelectCheckInventory(b, begin, e, end, codeinput, nameinput);
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
                if(inventories != null)
                    Session["RemindCount"] = inventories.Count();
                inComes = InCome.SelectRemaining();
                if(inComes != null)
                    Session["inComes"] = inComes.Count();
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