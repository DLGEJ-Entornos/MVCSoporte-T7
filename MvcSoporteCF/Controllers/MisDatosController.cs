using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSoporteCF.Models;
using Microsoft.AspNet.Identity;

namespace MvcSoporteCF.Controllers
{
    public class MisDatosController : Controller
    {
        private SoporteContexto db = new SoporteContexto();
        // GET: MisDatos / Create
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        // POST: MisDatos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
 "Id,Nombre,Email,Telefono,FechaNacimiento")] Empleado empleado)
        {
            // Para asignar el Nombre del usuario identificado al campo Email de Empleado
            empleado.Email = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleado);
                db.SaveChanges();
                // Redirige a la acción Index del controlador Home
                return RedirectToAction("Index", "Home");
            }
            return View(empleado);
        }
    }
}