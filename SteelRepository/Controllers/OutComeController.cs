using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class OutComeController : Controller
    {
        // GET: OutCome
        public ActionResult OutCome_list()
        {
            return View(OutCome.SelectAll());
        }
        public ActionResult OutCome_Select()
        {
            List<OutCome> Outcome = new List<OutCome>();
            return View(Outcome);
        }
        [HttpPost]
        public ActionResult OutCome_SelectChcek(FormCollection collection)
        {
            DateTime begin = DateTime.ParseExact(collection["data"],"YYYYMMDD",null);
            DateTime end = DateTime.ParseExact(collection["data1"], "YYYYMMDD", null);
            List<OutCome> outCome = OutCome.GetOutComes(begin, end);
            if (outCome != null)
                return View(outCome);
            return View();
        }
    }
}