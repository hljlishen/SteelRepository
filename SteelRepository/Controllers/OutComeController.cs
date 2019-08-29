﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SteelRepository.Controllers
{
    public class OutComeController : Controller
    {
        // GET: OutCome
        public ActionResult OutCome_list()
        {
            ViewData["LoginEmployee"] = IndexController.LoginEmployee();
            Employee.NoJudge();
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["employee"] = Employee.SelectAll();
            ViewData["outcome"] = OutCome.GetOutComesDesc();
            ViewData["manufacturer"] = Manufacturer.SelectAll();
            ViewData["department"] = Department.SelectAll();
            return View();
        }
        public ActionResult OutCome_More(int id)
        {
            ViewData["outcome"] = null;
            ViewData["outcome"] = OutCome.GetOutCome(id);
            return View();
        }
        [HttpPost]
        public ActionResult OutCome_list(FormCollection collection)
        {
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["employee"] = Employee.SelectAll();
            ViewData["manufacturer"] = Manufacturer.SelectAll();
            ViewData["department"] = Department.SelectAll();
            string begin = collection["date"];
            string end = collection["date1"];
            int materialCodeid = Convert.ToInt32(collection["materialCodeid"]);
            int manufacturerid = Convert.ToInt32(collection["manufacturerid"]);
            int departmentid = Convert.ToInt32(collection["departmentid"]);
            int  employeeid = Convert.ToInt32(collection["employeeid"]);
            ViewData["outcome"] = null;
            ViewData["outcome"] = OutCome.MulSelectCheckOutCome(begin,end, materialCodeid, employeeid,manufacturerid,departmentid);
            return View();
        }
        [HttpPost]
        public JsonResult OutCome_revocation(FormCollection collection)
        {
            int materialCodeid2 = Convert.ToInt32(collection["materialCodeid2"]);
            return Json(OutCome.OutComeRevocation(materialCodeid2));
        }
        public ActionResult OutCome_revocationlist()
        {
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["employee"] = Employee.SelectAll();
            ViewData["manufacturer"] = Manufacturer.SelectAll();
            ViewData["department"] = Department.SelectAll();
            ViewData["outcome"] = null;
            ViewData["outcome"] = OutCome.GetRevocationOutComes();
            return View("OutCome_list");
        }
    }
}