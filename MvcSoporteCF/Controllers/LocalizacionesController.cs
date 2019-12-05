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
    public class LocalizacionesController : Controller
    {
        private SoporteContexto db = new SoporteContexto();

        // GET: Localizaciones
        public ActionResult Index()
        {
            return View(db.Localizaciones.ToList());
        }

        // GET: Localizaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localizacion localizacion = db.Localizaciones.Find(id);
            if (localizacion == null)
            {
                return HttpNotFound();
            }
            return View(localizacion);
        }

        // GET: Localizaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Localizaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] Localizacion localizacion)
        {
            if (ModelState.IsValid)
            {
                db.Localizaciones.Add(localizacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(localizacion);
        }

        // GET: Localizaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localizacion localizacion = db.Localizaciones.Find(id);
            if (localizacion == null)
            {
                return HttpNotFound();
            }
            return View(localizacion);
        }

        // POST: Localizaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] Localizacion localizacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localizacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(localizacion);
        }

        // GET: Localizaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localizacion localizacion = db.Localizaciones.Find(id);
            if (localizacion == null)
            {
                return HttpNotFound();
            }
            return View(localizacion);
        }

        // POST: Localizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Localizacion localizacion = db.Localizaciones.Find(id);
            db.Localizaciones.Remove(localizacion);
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
