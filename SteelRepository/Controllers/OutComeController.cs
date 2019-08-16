using Models;
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
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["employee"] = Employee.SelectAll();
            ViewData["outcome"] = OutCome.SelectAll();
            return View();
        }
        public ActionResult OutCome_More(int id)
        {
            ViewData["outcome"] = null;
            ViewData["outcome"] = OutCome.GetOutCome(id);
            return View();
        }

        public ActionResult OutCome_Select()
        {
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["employee"] = Employee.SelectAll();
            ViewData["outcome"] = null;
            ViewData["outcome"] = new List<OutCome>();
            return View("OutCome_list");
        }
        [HttpPost]
        public ActionResult OutCome_list(FormCollection collection)
        {
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["employee"] = Employee.SelectAll();
            bool b = DateTime.TryParse(collection["date"], out DateTime begin);
            bool e = DateTime.TryParse(collection["date1"], out DateTime end);
            int materialCodeid = Convert.ToInt32(collection["materialCodeid"]);
            int  employeeid = Convert.ToInt32(collection["employeeid"]);
            ViewData["outcome"] = null;
            ViewData["outcome"] = OutCome.MulSelectCheckOutCome(b, begin, e, end, materialCodeid, employeeid);
            return View();
        }
    }
}