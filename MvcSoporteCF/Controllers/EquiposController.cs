using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcSoporteCF.Models;

namespace MvcSoporteCF.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EquiposController : Controller
    {
        private SoporteContexto db = new SoporteContexto();

        // GET: Equipos
        public ActionResult Index()
        {
            var equipos = db.Equipos.Include(e => e.Localizacion);
            return View(equipos.ToList());
        }

        // GET: Equipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipos.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // GET: Equipos/Create
        public ActionResult Create()
        {
            ViewBag.LocalizacionId = new SelectList(db.Localizaciones, "Id", "Descripcion");
            return View();
        }

        // POST: Equipos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodigoEquipo,Marca,Modelo,NumeroSerie,Caracteristicas,FechaCompra,FechaAlta,FechaBaja,LocalizacionId")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Equipos.Add(equipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocalizacionId = new SelectList(db.Localizaciones, "Id", "Descripcion", equipo.LocalizacionId);
            return View(equipo);
        }

        // GET: Equipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipos.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocalizacionId = new SelectList(db.Localizaciones, "Id", "Descripcion", equipo.LocalizacionId);
            return View(equipo);
        }

        // POST: Equipos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodigoEquipo,Marca,Modelo,NumeroSerie,Caracteristicas,FechaCompra,FechaAlta,FechaBaja,LocalizacionId")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocalizacionId = new SelectList(db.Localizaciones, "Id", "Descripcion", equipo.LocalizacionId);
            return View(equipo);
        }

        // GET: Equipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipos.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipo equipo = db.Equipos.Find(id);
            db.Equipos.Remove(equipo);
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
