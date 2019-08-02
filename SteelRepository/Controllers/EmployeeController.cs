﻿using DbInterface;
using DbService;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class EmployeeController : Controller
    {
        private static Employee em;
        // GET: Employee
        public ActionResult Employee_list()
        {
            return View(Employee.SelectAll());
        }

        public ActionResult Employee_add()
        {
            SelectList select = new SelectList(GetPermissionsList(), "Value", "Text");
            ViewBag.select = select;
            return View();
        }

        [HttpPost]
        public JsonResult Employee_add(Employee employee)
        {
            return Json(Employee.Inster(employee));
        }

        private List<SelectListItem> GetPermissionsList()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            SelectListItem item0 = new SelectListItem()
            {
                Value = "1",
                Text = "一级管理员"
            };
            SelectListItem item1 = new SelectListItem()
            {
                Value = "2",
                Text = "二级管理员"
            };
            SelectListItem item2 = new SelectListItem()
            {
                Value = "3",
                Text = "三级管理员"
            };
            itemList.Add(item0);
            itemList.Add(item1);
            itemList.Add(item2);
            return itemList;
        }

        public ActionResult Employee_update(int id)
        {
            ViewData["Employee"] = Employee.FindId(id);
            em = Employee.FindId(id);
            return View();
        }

        [HttpPost]
        public JsonResult Employee_update(Employee employee, FormCollection collection)
        {
            employee.id = em.id;
            employee.number = em.number;
            employee.password = em.password;
            return Json(Employee.Update(employee));
        }

        public ActionResult Employee_select(int id)
        {
            ViewData["Employee"] = Employee.FindId(id);
            return View();
        }

        public JsonResult Employee_delete(int id)
        {
            return Json(Employee.Delete(id));
        }

        public ActionResult Employee_information(int id)
        {
            ViewData["Employee"] = Employee.FindId(id);
            return View();
        }
    }
}