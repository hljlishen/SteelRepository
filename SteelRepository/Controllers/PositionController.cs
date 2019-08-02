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

    }
}