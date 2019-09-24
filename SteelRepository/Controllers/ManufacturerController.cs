using Models;
using System.Web.Mvc;

namespace SteelRepository.Controllers
{
    public class ManufacturerController : Controller
    {
        private static Manufacturer mf;
        // GET: Employee
        public ActionResult Manufacturer_list()
        {
            ViewData["permissions"] = Session["permissions"];
            Employee.NoJudge();
            return View(Manufacturer.SelectAll());
        }

        public ActionResult Manufacturer_add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Manufacturer_add(Manufacturer manufacturer)
        {
            return Json(Manufacturer.Insert(manufacturer));
        }


        public ActionResult Manufacturer_update(int id)
        {
            ViewData["Manufacturer"] = Manufacturer.GetManufacturer(id);
            mf = Manufacturer.GetManufacturer(id);
            return View();
        }

        [HttpPost]
        public JsonResult Manufacturer_update(Manufacturer manufacturer)
        {
            manufacturer.id = mf.id;
            return Json(Manufacturer.Update(manufacturer));
        }

        public JsonResult Manufacturer_delete(int id)
        {
            return Json(Manufacturer.Delete(id));
        }

        public ActionResult Manufacturer_information(int id)
        {
            Manufacturer manufacturer = Manufacturer.GetManufacturer(id);
            ViewData["Manufacturer"] = manufacturer;
            mf = manufacturer;
            return View();
        }

        [HttpPost]
        public JsonResult Manufacturer_information(Manufacturer manufacturer)
        {
            manufacturer.id = mf.id;
            return Json(Manufacturer.Update(manufacturer));
        }
    }
}