using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class DataStatisticsController : Controller
    {
        private static Dictionary<string,double> keyValuePairs1;
        private static Dictionary<string, double> keyValuePairs2;
        private static Dictionary<string, double> keyValuePairs3;
        // GET: DataStatistics
        public ActionResult DataStatistics()
        {
            Employee.NoJudge();
            DateTime dt = DateTime.Now;
            DateTime dt_First = dt.AddDays(1 - (dt.Day));
            int year = dt.Date.Year;
            int month = dt.Date.Month;
            int dayCount = DateTime.DaysInMonth(year, month);
            DateTime dt_Last = dt_First.AddDays(dayCount - 1);
            keyValuePairs1 = Inventory.StatisticAmount();
            ViewData["amount"] = keyValuePairs1;
            keyValuePairs2 = Project.StatisticPrice();
            ViewData["project"] = keyValuePairs2;
            ViewData["Time"] = OutCome.StatisticConsumptionAmount(dt_First.Date.ToString(), dt_Last.Date.ToString());
            ViewData["NowTime"] = Inventory.NewDateMonth() + " - " + Inventory.NewDateMonth();
            keyValuePairs3 = Inventory.StatisticConsumptionAmount();
            ViewData["consumptionAmount"] = keyValuePairs3;
            return View();
        }

        [HttpPost]
        public ActionResult DataStatistics(FormCollection collection)
        {
            string times = collection["test8"];
            string[] condition = { " - " };
            string[] result = times.Split(condition, StringSplitOptions.None);
            DateTime dtFirst = DateTime.Parse(result[0]);
            DateTime dtLast = DateTime.Parse(result[1]);
            int year = dtLast.Date.Year;
            int month = dtLast.Date.Month;
            int dayCount = DateTime.DaysInMonth(year, month);
            DateTime dt_Last = dtLast.AddDays(dayCount - 1);
            ViewData["amount"] = keyValuePairs1;
            ViewData["project"] = keyValuePairs2;
            ViewData["Time"] = OutCome.StatisticConsumptionAmount(dtFirst.Date.ToString(), dt_Last.Date.ToString());
            ViewData["NowTime"] = times;
            ViewData["consumptionAmount"] = keyValuePairs3;
            return View();
        }
    }
}