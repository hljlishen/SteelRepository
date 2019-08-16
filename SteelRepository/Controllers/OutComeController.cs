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
        private static List<OutCome> outComes;
        public ActionResult OutCome_list()
        {
            return View(OutCome.SelectAll());
        }
        public ActionResult OutCome_More(int id)
        {
            ViewData["OutCome"] = OutCome.GetOutCome(id);
            return View();
        }
        public ActionResult OutCome_Select()
        {
            ViewData["MaterialCode"] = MaterialCode.GetMaterialCodeList();
            ViewData["employee"] = Employee.SelectAll();
            ViewData["outcome"] = new List<OutCome>();
            return View();
        }
        [HttpPost]
        public ActionResult OutCome_Select(FormCollection collection)
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