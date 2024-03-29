﻿using Models;
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
            ViewData["permissions"] = Session["permissions"];
            return View(Position.SelectAll());
        }
        public ActionResult Position_add()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Position_add(Position position)
        {
            return Json(Position.Insert(position));
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
            po.note = position.note;
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