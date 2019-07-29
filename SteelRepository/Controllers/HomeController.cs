using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";



            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your Login page.";

            return View();
        }
        public ActionResult Searching()
        {
            ViewBag.Message = "Your Searching page";
            return View();
        }
        public ActionResult Chart()
        {
            ViewBag.Message = "Your Charting page";
            return View();
        }
        public ActionResult In()
        {
            ViewBag.Message = "Your 入库 page";
            return View();
        }
        public ActionResult Out()
        {
            ViewBag.Message = "Your 出库 page";
            return View();
        }
    }
}