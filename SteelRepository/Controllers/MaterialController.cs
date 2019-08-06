using DbInterface;
using DbService;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class MaterialController : Controller
    {
        // GET: Material
        public ActionResult InCome_list()
        {
            return View(InCome.GetInComes());
        }

        public ActionResult OutCome_list()
        {
            return View();
        }

        public ActionResult InCome_add()
        {
            SelectList select = new SelectList(GetUnitList(), "Value", "Text");
            ViewBag.select = select;
            SelectList select2 = new SelectList(GetMenufacturerList(), "Value", "Text");
            ViewBag.selectMenu = select2;
            SelectList select3 = new SelectList(GetPositionList(), "Value", "Text");
            ViewBag.selectPosition = select3;
            SelectList select4 = new SelectList(GetOperatorList(), "Value", "Text");
            ViewBag.selectOperator = select4;
            return View();
        }

        [HttpPost]
        public JsonResult InCome_add(InCome inCome,FormCollection collection)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                Category category = Category.Insert(collection["category"]);
                inCome.categoryId = category.id;
                inCome.batch = collection["batch"];
                inCome.unitPrice = double.Parse(collection["unitPrice"]);
                inCome.amount = double.Parse(collection["amount"]);
                inCome.storageTime = DateTime.Parse(collection["InComeTime"]);
                InCome.NewInCome(inCome, collection["materialCode"], collection["name1"], collection["model"]);
                return Json(true);
            } 
        }

        private List<SelectListItem> GetUnitList()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            SelectListItem item0 = new SelectListItem()
            {
                Value = "g",
                Text = "g"
            };
            SelectListItem item1 = new SelectListItem()
            {
                Value = "kg",
                Text = "kg"
            };
            itemList.Add(item0);
            itemList.Add(item1);
            return itemList;
        }

        private List<SelectListItem> GetMenufacturerList()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            foreach (var meun in Manufacturer.SelectAll())
            {
                SelectListItem item0 = new SelectListItem()
                {
                    Value = meun.id.ToString(),
                    Text = meun.manufacturersName
                };
                itemList.Add(item0);
            }
            return itemList;
        }

        private List<SelectListItem> GetPositionList()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            foreach (var pos in Position.SelectAll())
            {
                SelectListItem item0 = new SelectListItem()
                {
                    Value = pos.id.ToString(),
                    Text = pos.positionName
                };
                itemList.Add(item0);
            }
            return itemList;
        }

        private List<SelectListItem> GetOperatorList()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            foreach (var em in Employee.SelectAll())
            {
                SelectListItem item0 = new SelectListItem()
                {
                    Value = em.id.ToString(),
                    Text = em.name
                };
                itemList.Add(item0);
            }
            return itemList;
        }
    }
}