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
    public class AvisosController : Controller
    {
        private SoporteContexto db = new SoporteContexto();

        // GET: Avisos
        public ActionResult Index(string strTipoAveria, string strCadenaBusqueda)
        {
            var avisos = db.Avisos.Include(a => a.Empleado).Include(a => a.Equipo).Include(a => a.TipoAveria);
            avisos = avisos.OrderByDescending(s => s.FechaAviso); // Para ordenar por FechaAviso


            // Para presentar los tipos de avería en la vista
            var lstTipoAveria = new List<string>();
            var qryTipoAveria = from d in db.TipoAverias
                                orderby d.Descripcion
                                select d.Descripcion;
            lstTipoAveria.AddRange(qryTipoAveria.Distinct());
            ViewBag.ListaTipoAverias = new SelectList(lstTipoAveria);

            // Para buscar avisos por nombre de empleado en la lista de valores
            if (!String.IsNullOrEmpty(strCadenaBusqueda))
            {
                avisos = avisos.Where(s => s.Empleado.Nombre.Contains(strCadenaBusqueda));
            }

            // Para presentar los avisos filtrados por tipo de avería
            if (!string.IsNullOrEmpty(strTipoAveria))
            {
                avisos = avisos.Where(x => x.TipoAveria.Descripcion == strTipoAveria);
            }

            return View(avisos.ToList());
        }

        // GET: Avisos/Details/5
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

        // GET: Avisos/Create
        public ActionResult Create()
        {
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre");
            ViewBag.EquipoId = new SelectList(db.Equipos, "Id", "CodigoEquipo");
            ViewBag.TipoAveriaId = new SelectList(db.TipoAverias, "Id", "Descripcion");
            return View();
        }

        // POST: Avisos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,FechaAviso,FechaCierre,Observaciones,EmpleadoId,TipoAveriaId,EquipoId")] Aviso aviso)
        {
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

        // GET: Avisos/Edit/5
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

        // POST: Avisos/Edit/5
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

        // GET: Avisos/Delete/5
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

        // POST: Avisos/Delete/5
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
