using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MvcSoporteCF.Models;

namespace MvcSoporteCF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SoporteContexto db = new SoporteContexto();
            // Si existe el empleado correspondiente al usuario actual
            // se va a View, en caso contrario se va a crear el empleado.
            string usuario = User.Identity.GetUserName();
            var empleado = db.Empleados.Where(e => e.Email == usuario).FirstOrDefault();
            if (User.Identity.IsAuthenticated &&
            User.IsInRole("Usuario") &&
            empleado == null)
            {
                return RedirectToAction("Create", "MisDatos");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        [Authorize(Roles = "Usuario")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

