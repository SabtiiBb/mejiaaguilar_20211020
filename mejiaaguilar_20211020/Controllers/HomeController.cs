using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mejiaaguilar_20211020.Services;
using mejiaaguilar_20211020.Models;
using System.IO;

namespace mejiaaguilar_20211020.Controllers
{
    public class HomeController : Controller
    {
        readonly EmpleadoRepository _manejoEmpleadoServices = new EmpleadoRepository();
        public ActionResult Index(int? page, string message)
        {
            page = page == null ? 0 : page-1;
            ViewBag.Message = message;

            EmpleadoVM model = _manejoEmpleadoServices.getListEmp((int)page);
            return View(model);
        }

        public ActionResult NewEmpleado()
        {
            ViewBag.DptoList = _manejoEmpleadoServices.getDepartamentos();
            return View();
        }

        [HttpPost]
        public ActionResult NewEmpleado(EmpleadoVM model)
        {
            ViewBag.DptoList = _manejoEmpleadoServices.getDepartamentos();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (_manejoEmpleadoServices.AddEmpleado(model))
                {
                    return RedirectToAction("Index", "Home", new { message = "NewAlert" });
                }
            }
            return View(model);
        }

        public ActionResult EditEmpleado(int id)
        {
            ViewBag.DptoList = _manejoEmpleadoServices.getDepartamentos();
            EmpleadoVM model = _manejoEmpleadoServices.getEmpleado(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditEmpleado(EmpleadoVM model)
        {
            ViewBag.DptoList = _manejoEmpleadoServices.getDepartamentos();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (_manejoEmpleadoServices.EditEmpleado(model))
                {
                    return RedirectToAction("Index", "Home", new { message = "EditAlert" });
                }
            }
            return View(model);
        }


        //------------------------------------------------------END POINTS------------------------------------------------------
        public string RenderPartialToString(string ViewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, ViewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);

                return writer.ToString();
            }
        }

        [HttpPost]
        public JsonResult EliminarEmpleado(int id)
        {
            bool Exito = _manejoEmpleadoServices.DeleteEmpleado(id);

            return Json(Exito, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDeatilsModal(int id)
        {
            EmpleadoVM model = _manejoEmpleadoServices.getEmpleado(id);
            string partial = RenderPartialToString("~/Views/Home/_PartialDetalleEmpleado.cshtml", model);

            return Json(new { partial }, JsonRequestBehavior.AllowGet);
        }
    }
}