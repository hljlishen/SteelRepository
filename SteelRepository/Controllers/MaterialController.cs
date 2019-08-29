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
        private static InCome In;
        private static int incomeid;
        // GET: Material
        public ActionResult InCome_list()
        {
            Employee.NoJudge();
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["manufacturer"] = Manufacturer.SelectAll();
            ViewData["LoginEmployee"] = IndexController.LoginEmployee();
            ViewData["InComeView"] = InCome.GetInComeViewDesc();
            return View();
        }

        [HttpPost]
        public ActionResult InCome_list(FormCollection collection)
        {
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["manufacturer"] = Manufacturer.SelectAll();
            string begin = collection["date"];
            string end = collection["date1"];
            int materialCodeid = Convert.ToInt32(collection["materialCodeid"]);
            int manufacturerid = Convert.ToInt32(collection["manufacturerid"]);
            ViewData["InComeView"] = null;
            ViewData["InComeView"] = InCome.MulSelectCheckInCome(begin, end, materialCodeid, manufacturerid);
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
            SelectList select5 = new SelectList(GetUnitPriceList(), "Value", "Text");
            ViewBag.selectUnitPrice = select5;
            return View();
        }

        [HttpPost]
        public JsonResult InCome_add(InCome inCome, FormCollection collection)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                inCome.categoryId = Category.Insert(collection["category"]).id;
                inCome.batch = collection["batch"];
                inCome.unitPrice = double.Parse(collection["unitPrice"]);
                inCome.amount = double.Parse(collection["amount"]);
                inCome.storageTime = DateTime.Parse(collection["InComeTime"]);
                inCome.reviewCycle = int.Parse(collection["RecheckCycle"]);
                InCome.NewInCome(inCome, int.Parse(collection["position"]), collection["materialCode"], collection["name1"], collection["model"], DateTime.Parse(collection["RecheckTime"]), qualityCertification(collection), recheckReport(collection));
                return Json(true);
            }
        }

        private List<byte[]> qualityCertification(FormCollection collection)
        {
            int qualityCount = 0;
            List<byte[]> listByte = new List<byte[]>();
            if (collection["qualityCount"] != "")
                qualityCount = int.Parse(collection["qualityCount"]);
            for (int i=0; i<qualityCount;i++)
            {
                if(collection["quality"+i] != null)
                {
                    if (collection["quality" + i] != "")
                    {
                        string[] array = collection["quality" + i].Split(',');
                        byte[] imageBytes = Convert.FromBase64String(array[1]);
                        listByte.Add(imageBytes);
                    }
                }    
            }
            return listByte;
        }

        private List<byte[]> recheckReport(FormCollection collection)
        {
            int recheckCount = 0;
            List<byte[]> listByte = new List<byte[]>();
            if (collection["recheckCount"] != "")
                recheckCount = int.Parse(collection["recheckCount"]);
            for (int i = 0; i < recheckCount; i++)
            {
                if (collection["recheck" + i] != null)
                {
                    if (collection["recheck" + i] != "")
                    {
                        string[] array = collection["recheck" + i].Split(',');
                        byte[] imageBytes = Convert.FromBase64String(array[1]);
                        listByte.Add(imageBytes);
                    }
                }
            }
            return listByte;
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
            SelectList select5 = new SelectList(GetUnitPriceList(id), "Value", "Text");
            ViewBag.selectUnitPrice = select5;
            InComeView inComeView = InCome.SeleteInComeView(id);
            ViewData["InComeView"] = inComeView;
            InCome inCome = InCome.Selete(id);
            In = inCome;
            return View();
        }

        public ActionResult InCome_selete(int id)
        {
            ViewData["InComeView"] = InCome.SeleteInComeView(id);
            return View();
        }

        [HttpPost]
        public JsonResult InCome_update(InComeView inCome, FormCollection collection)
        {
            InCome UpIncome = new InCome();
            UpIncome.id = In.id;
            UpIncome.categoryId = Category.Insert(collection["category"]).id;
            UpIncome.codeId = inCome.materId;
            UpIncome.batch = collection["batch"];
            UpIncome.menufactureId = int.Parse(collection["manufacturer"]);
            UpIncome.unit = collection["unit"];
            UpIncome.amount = double.Parse(collection["amount"]);
            UpIncome.unitPrice = double.Parse(collection["unitPrice"]);
            UpIncome.priceMeasure = collection["priceMeasure"];
            UpIncome.storageTime = DateTime.Parse(collection["IncomeText"]);
            UpIncome.operatorId = int.Parse(collection["operator"]);
            UpIncome.reviewCycle = double.Parse(collection["reviewCycle"]);
            //inCome.id = In.id;
            //inCome.categoryId = Category.Insert(collection["category"]).id;
            //inCome.codeId = In.codeId;
            //inCome.batch = collection["batch"];
            //inCome.menufactureId = int.Parse(collection["manufacturer"]);
            //inCome.unit = collection["unit"];
            //inCome.amount = double.Parse(collection["amount"]);
            //inCome.unitPrice = double.Parse(collection["unitPrice"]);
            //inCome.priceMeasure = collection["priceMeasure"];
            //inCome.storageTime = DateTime.Parse(collection["IncomeText"]);
            //inCome.operatorId = int.Parse(collection["operator"]);
            //inCome.reviewCycle = double.Parse(collection["reviewCycle"]);
            return Json(InCome.Update(UpIncome));
        }

        public ActionResult QualityReport(int id)
        {
            incomeid = id;
            List<QualityCertificationReportImg> quality = QualityCertificationReportImg.GetQualityCertificationReportImg(id);
            if (quality != null)
                return View(quality);
            return View();
        }

        [HttpPost]
        public JsonResult QualityReport(FormCollection collection)
        {
            DelectQualityReportImg(collection["quality"]);
            List<byte[]> byteList = qualityCertification(collection);
            if (byteList != null)
            {
                foreach (var item in byteList)
                {
                    QualityCertificationReportImg.Insert(incomeid, item);
                }
            }
            return Json(true);
        }

        public ActionResult RecheckReports(int id)
        {
            incomeid = id;
            List<RecheckReportImg> recheckReportImgs = new List<RecheckReportImg>();
            foreach (var recheck in RecheckReport.GetRecheckReports(id))
            {
                foreach (var recheckImg in RecheckReportImg.GetRecheckReportImgs(recheck.id))
                {
                    recheckReportImgs.Add(recheckImg);
                }
            }
            return View(recheckReportImgs);
        }

        [HttpPost]
        public JsonResult RecheckReports(FormCollection collection)
        {
            DelectRecheckReportImg(collection["recheck"]);
            List<byte[]> byteList = recheckReport(collection);
            if (byteList != null && byteList.Count != 0)
            {
                DateTime dateTime = DateTime.Parse(collection["RecheckTime"]);
                RecheckReport.Insert(incomeid, dateTime, byteList);
            }
            return Json(true);
        }

        private void DelectRecheckReportImg(string imgIds)
        {
            if (imgIds != null)
            {
                string[] array = imgIds.Split(',');
                foreach (var imgId in array)
                {
                    if (imgId == "")
                        return;
                    RecheckReportImg.Delect(int.Parse(imgId));
                }
            }
        }

        private void DelectQualityReportImg(string imgIds)
        {
            if (imgIds != null)
            {
                string[] array = imgIds.Split(',');
                foreach (var imgId in array)
                {
                    if (imgId == "")
                        return;
                    QualityCertificationReportImg.Delect(int.Parse(imgId));
                }
            }
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

        private List<SelectListItem> GetUnitPriceList()
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

        private List<SelectListItem> GetUnitPriceList(int inComeId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            string priceMeasure = InCome.Selete(inComeId).priceMeasure;
            SelectListItem item0 = new SelectListItem()
            {

                Value = priceMeasure,
                Text = priceMeasure
            };
            itemList.Add(item0);
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
            if (position != null)
            {
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