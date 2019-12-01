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
    public class TipoAveriasController : Controller
    {
        private SoporteContexto db = new SoporteContexto();

        // GET: TipoAverias
        public ActionResult Index()
        {
            return View(db.TipoAverias.ToList());
        }

        // GET: TipoAverias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAveria tipoAveria = db.TipoAverias.Find(id);
            if (tipoAveria == null)
            {
                return HttpNotFound();
            }
            return View(tipoAveria);
        }

        // GET: TipoAverias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoAverias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] TipoAveria tipoAveria)
        {
            if (ModelState.IsValid)
            {
                db.TipoAverias.Add(tipoAveria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoAveria);
        }

        // GET: TipoAverias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAveria tipoAveria = db.TipoAverias.Find(id);
            if (tipoAveria == null)
            {
                return HttpNotFound();
            }
            return View(tipoAveria);
        }

        // POST: TipoAverias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] TipoAveria tipoAveria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoAveria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoAveria);
        }

        // GET: TipoAverias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAveria tipoAveria = db.TipoAverias.Find(id);
            if (tipoAveria == null)
            {
                return HttpNotFound();
            }
            return View(tipoAveria);
        }

        // POST: TipoAverias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoAveria tipoAveria = db.TipoAverias.Find(id);
            db.TipoAverias.Remove(tipoAveria);
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
