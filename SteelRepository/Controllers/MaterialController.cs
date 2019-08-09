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
        public JsonResult InCome_add(InCome inCome, FormCollection collection)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                Category category = Category.Insert(collection["category"]);
                inCome.categoryId = category.id;
                inCome.batch = collection["batch"];
                inCome.unitPrice = double.Parse(collection["unitPrice"]);
                inCome.amount = double.Parse(collection["amount"]);
                inCome.storageTime = DateTime.Parse(collection["InComeTime"]);
                //string s = collection["quality1"];

                InCome.NewInCome(inCome,int.Parse(collection["position"]) ,collection["materialCode"], collection["name1"], collection["model"]);
                return Json(true);
            }
        }

        public ActionResult InCome_update(int id)
        {
            SelectList select = new SelectList(GetUnitList(id), "Value", "Text");
            ViewBag.select = select;
            SelectList select2 = new SelectList(GetMenufacturerList(id), "Value", "Text");
            ViewBag.selectMenu = select2;
            SelectList select3 = new SelectList(GetPositionList(id), "Value", "Text");
            ViewBag.selectPosition = select3;
            SelectList select4 = new SelectList(GetOperatorList(id), "Value", "Text");
            ViewBag.selectOperator = select4;
            ViewData["InCome"] = InCome.Selete(id);
            return View();
        }

        public ActionResult InCome_selete(int id)
        {
            ViewData["InCome"] = InCome.Selete(id);
            return View();
        }

        //[HttpPost]
        //public JsonResult InCome_update(InCome inCome,FormCollection collection)
        //{
        //    return Json();
        //}

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

        private List<SelectListItem> GetUnitList(int inComeId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            string unit = InCome.Selete(inComeId).unit;
            SelectListItem item0 = new SelectListItem()
            {
                
                Value = unit,
                Text = unit
            };
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
            itemList.Add(item0);
            itemList.Add(item1);
            itemList.Add(item2);
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

        private List<SelectListItem> GetMenufacturerList(int inComeId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            Manufacturer menufacture = Manufacturer.GetManufacturer(int.Parse(InCome.Selete(inComeId).menufactureId.ToString()));
            SelectListItem item0 = new SelectListItem()
            {
                Value = menufacture.id.ToString(),
                Text = menufacture.manufacturersName
            };
            itemList.Add(item0);
            foreach (var meun in Manufacturer.SelectAll())
            {
                SelectListItem item1 = new SelectListItem()
                {
                    Value = meun.id.ToString(),
                    Text = meun.manufacturersName
                };
                itemList.Add(item1);
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

        private List<SelectListItem> GetPositionList(int inComeId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            int positionId = 0;
            foreach (var inventory in Inventory.InComeIdGetInventory(inComeId))
            {
                positionId = inventory.positionId;
            }
            Position position = Position.GetPosition(positionId);
            SelectListItem item0 = new SelectListItem()
            {
                Value = position.id.ToString(),
                Text = position.positionName
            };
            itemList.Add(item0);
            foreach (var pos in Position.SelectAll())
            {
                SelectListItem item1 = new SelectListItem()
                {
                    Value = pos.id.ToString(),
                    Text = pos.positionName
                };
                itemList.Add(item1);
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

        private List<SelectListItem> GetOperatorList(int inComeId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            Employee employee = Employee.FindId(int.Parse(InCome.Selete(inComeId).operatorId.ToString()));
            SelectListItem item0 = new SelectListItem()
            {
                Value = employee.id.ToString(),
                Text = employee.name
            };
            itemList.Add(item0);
            foreach (var em in Employee.SelectAll())
            {
                SelectListItem item1 = new SelectListItem()
                {
                    Value = em.id.ToString(),
                    Text = em.name
                };
                itemList.Add(item1);
            }
            return itemList;
        }

    }
}