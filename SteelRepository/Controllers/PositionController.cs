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
    public class PositionController : Controller
    {
        private static Position po;
        // GET: Position
        public ActionResult Position_list()
        {
            return View(Position.SelectAll());
        }
        public ActionResult Position_add()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Position_add(Position position)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return Json(helper.Insert(position));
            }
        }
        public ActionResult Position_Update(int id)
        {
            ViewData["position"] = Position.GetPosition(id);
            po = Position.GetPosition(id);
            return View();
        }
        [HttpPost]
        public JsonResult Position_Update(Position position, FormCollection collection)
        {
            po.positionName = position.positionName;
            return Json(Position.Update(po));
        }
        [HttpPost]
        public JsonResult Position_Delete(int id)
        {
            return Json(Position.Delete(id));
        }
        public ActionResult Position_Select(int id)
        {
            ViewData["PositionSelect"] = Position.StatisticAmount(id);
            List<string> ls1 = new List<string>();
            List<double> ls2 = new List<double>();
            Dictionary<string, double> valuePairs = new Dictionary<string, double>();
            foreach (KeyValuePair<string, Dictionary<string, double>> kandv in Position.StatisticAmount(id))
            {
                valuePairs.Add(kandv.Value.Keys.ToString(), double.Parse(kandv.Value.Values.ToString()));
                foreach (var ll in valuePairs)
                {
                    ls1.Add(ll.Key);
                    ls2.Add(ll.Value);
                }
            }
            ViewData["PositionSelectstr"] = ls1;
            ViewData["PositionSelectdou"] = ls2;
            return View();
        }
    }
}