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
            string positionName = collection["positionName"];
            int number = Convert.ToInt32(collection["number"]);
            string employeeName = collection["employeeName"];
            string projectName = collection["projectName"];
            if (b)
            {
                if (e)
                {
                    return View(OutCome.GetOutComes(begin, end));
                }
                else
                    return View(OutCome.GetOutComes(begin, DateTime.MaxValue));
            }
            else
                if (e)
            {
                return View(OutCome.GetOutComes(DateTime.MinValue, end));
            }
            else
                if (positionName != null)
            {
                return View(OutCome.ReverseGetPositionName(positionName));
            }
            else
                if (number != 0)
            {
                return View(OutCome.OutComeNumber(number));
            }
            else
                if (employeeName != null)
            {
                return View(OutCome.ReverseGetEmployeeName(employeeName));
            }
            else
                if (projectName != null)
            {
                return View(OutCome.ReverseGetProjectName(projectName));
            }else
            return View(new List<OutCome>());
        }
    }
}