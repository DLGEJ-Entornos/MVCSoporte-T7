using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MvcSoporteCF.Models;

namespace MvcSoporteCF.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class MisAvisosController : Controller
    {
        private SoporteContexto db = new SoporteContexto();

        // GET: MisAvisos
        public ActionResult Index()
        {
            // Se seleccionan los datos del empleado correspondiente al usuario actual
            string wUsuario = User.Identity.GetUserName();
            var empleado = db.Empleados.Where(e => e.Email == wUsuario).FirstOrDefault();
            if (empleado == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var avisos = db.Avisos.Include(a => a.Empleado).Include(a => a.Equipo).Include(a => a.TipoAveria);

            // Se seleccionan los avisos del Empleado correspondiente al usuario actual
            avisos = avisos.Where(a => a.EmpleadoId == empleado.Id).OrderByDescending(a =>
           a.FechaAviso);

            return View(avisos.ToList());
        }

        // GET: MisAvisos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviso aviso = db.Avisos.Find(id);
            if (aviso == null)
            {
                return HttpNotFound();
            }
            return View(aviso);
        }

        // GET: MisAvisos/Create
        public ActionResult Create()
        {
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre");
            ViewBag.EquipoId = new SelectList(db.Equipos, "Id", "CodigoEquipo");
            ViewBag.TipoAveriaId = new SelectList(db.TipoAverias, "Id", "Descripcion");
            return View();
        }

        // POST: MisAvisos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,FechaAviso,FechaCierre,Observaciones,EmpleadoId,TipoAveriaId,EquipoId")] Aviso aviso)
        {
            // Se asigna el Id de empleado correspondiente al usuario actual al aviso
            string wUsuario = User.Identity.GetUserName();
            int wEmpleadoId = db.Empleados.Where(e => e.Email == wUsuario).FirstOrDefault().Id;
            aviso.EmpleadoId = wEmpleadoId;

            if (ModelState.IsValid)
            {
                db.Avisos.Add(aviso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre", aviso.EmpleadoId);
            ViewBag.EquipoId = new SelectList(db.Equipos, "Id", "CodigoEquipo", aviso.EquipoId);
            ViewBag.TipoAveriaId = new SelectList(db.TipoAverias, "Id", "Descripcion", aviso.TipoAveriaId);
            return View(aviso);
        }

        // GET: MisAvisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviso aviso = db.Avisos.Find(id);
            if (aviso == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre", aviso.EmpleadoId);
            ViewBag.EquipoId = new SelectList(db.Equipos, "Id", "CodigoEquipo", aviso.EquipoId);
            ViewBag.TipoAveriaId = new SelectList(db.TipoAverias, "Id", "Descripcion", aviso.TipoAveriaId);
            return View(aviso);
        }

        // POST: MisAvisos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,FechaAviso,FechaCierre,Observaciones,EmpleadoId,TipoAveriaId,EquipoId")] Aviso aviso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aviso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre", aviso.EmpleadoId);
            ViewBag.EquipoId = new SelectList(db.Equipos, "Id", "CodigoEquipo", aviso.EquipoId);
            ViewBag.TipoAveriaId = new SelectList(db.TipoAverias, "Id", "Descripcion", aviso.TipoAveriaId);
            return View(aviso);
        }

        // GET: MisAvisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviso aviso = db.Avisos.Find(id);
            if (aviso == null)
            {
                return HttpNotFound();
            }
            return View(aviso);
        }

        // POST: MisAvisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aviso aviso = db.Avisos.Find(id);
            db.Avisos.Remove(aviso);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
