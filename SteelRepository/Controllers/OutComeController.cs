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
            return View();
        }
        [HttpPost]
        public ActionResult OutCome_SelectCheck(FormCollection collection)
        {
            bool b = DateTime.TryParse(collection["date"],out DateTime begin);
            bool e = DateTime.TryParse(collection["date1"],out DateTime end);
            if (b&e)
            {
                return View(OutCome.GetOutComes(begin, end));
            }
            return View(new List<OutCome>());
        }
    }
}