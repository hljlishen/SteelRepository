using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class ProjectController : Controller
    {
        private static Project pro;
        // GET: Project
        public ActionResult Project_list()
        {
            return View(Project.SelectAll());
        }

        public ActionResult Project_add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Project_add(Project project)
        {
            project.state = 1;
            return Json(Project.Insert(project));
        }

        public ActionResult Project_update(int id)
        {
            pro = Project.GetProject(id);
            return View(pro);
        }

        [HttpPost]
        public JsonResult Project_update(Project project)
        {
            project.id = pro.id;
            return Json(Project.Update(project));
        }

        public JsonResult Project_delete(int id)
        {
            Project project = Project.GetProject(id);
            project.state = 2;
            return Json(Project.Update(project));
        }
    }
}