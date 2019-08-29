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
            Employee.NoJudge();
            ViewData["LoginEmployee"] = IndexController.LoginEmployee();
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
            ViewData["positionId"] = id;
            return View();
        }
    }
}