using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MicroLabsFinal.Models;

namespace MicroLabsFinal.Controllers
{
    public class modoPagoesController : Controller
    {
        private db_a78ddc_mircrolabsEntities1 db = new db_a78ddc_mircrolabsEntities1();

        // GET: modoPagoes
        public ActionResult Index()
        {
            return View(db.modoPago.ToList());
        }

        // GET: modoPagoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modoPago modoPago = db.modoPago.Find(id);
            if (modoPago == null)
            {
                return HttpNotFound();
            }
            return View(modoPago);
        }

        // GET: modoPagoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: modoPagoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pagoID,tipopPago")] modoPago modoPago)
        {
            if (ModelState.IsValid)
            {
                db.modoPago.Add(modoPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modoPago);
        }

        // GET: modoPagoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modoPago modoPago = db.modoPago.Find(id);
            if (modoPago == null)
            {
                return HttpNotFound();
            }
            return View(modoPago);
        }

        // POST: modoPagoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pagoID,tipopPago")] modoPago modoPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modoPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modoPago);
        }

        // GET: modoPagoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modoPago modoPago = db.modoPago.Find(id);
            if (modoPago == null)
            {
                return HttpNotFound();
            }
            return View(modoPago);
        }

        // POST: modoPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            modoPago modoPago = db.modoPago.Find(id);
            db.modoPago.Remove(modoPago);
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
