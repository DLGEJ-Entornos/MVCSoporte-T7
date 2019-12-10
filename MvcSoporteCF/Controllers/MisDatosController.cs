using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSoporteCF.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

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

        // GET: MisDatos/Edit
        public ActionResult Edit()
        {
            // Se seleccionan los datos del empleado correspondiente al usuario actual
            string wUsuario = User.Identity.GetUserName();
            var empleado = db.Empleados.Where(e => e.Email == wUsuario).FirstOrDefault();
            if (empleado == null)
            {
                // Si no existe el empleado, redirige a la acción Index del controlador Home
                return RedirectToAction("Index", "Home");
            }
            // Si existe el empleado correspondiente se va a View
            return View(empleado);
        }
        // POST: MisDatos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
 "Id,Nombre,Email,Telefono,FechaNacimiento")] Empleado empleado)
        {
            empleado.Email = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(empleado);
        }
    }
}