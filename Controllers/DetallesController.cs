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
    public class DetallesController : Controller
    {
        private db_a78ddc_mircrolabsEntities1 db = new db_a78ddc_mircrolabsEntities1();

        // GET: Detalles
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "codigo" : "";
            ViewBag.DateSortParm = sortOrder == "numeroFactura" ? "cantidad" : "numeroFactura";
            var detalle = from s in db.Detalle
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                detalle = detalle.Where(s => s.codigoProducto.Contains(searchString)
                                       || s.codigoProducto.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "codigo":
                    detalle = detalle.OrderByDescending(s => s.codigoProducto);
                    break;
                case "numeroFactura":
                    detalle = detalle.OrderBy(s => s.numeroFactura);
                    break;
                case "cantidad":
                    detalle = detalle.OrderByDescending(s => s.cantidad);
                    break;

            }
            return View(detalle.ToList());
        }

        // GET: Detalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // GET: Detalles/Create
        public ActionResult Create()
        {
            ViewBag.numeroFactura = new SelectList(db.Factura, "numeroFactura", "codigoFactura");
            ViewBag.idIVA = new SelectList(db.IVA, "idIVA", "idIVA");
            ViewBag.codigoProducto = new SelectList(db.Producto, "codigoProducto", "nombreProducto");
            return View();
        }

        // POST: Detalles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDetalle,numeroFactura,codigoProducto,cantidad,precioUnitario,montoIVA,subTotal,idIVA")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Detalle.Add(detalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.numeroFactura = new SelectList(db.Factura, "numeroFactura", "codigoFactura", detalle.numeroFactura);
            ViewBag.idIVA = new SelectList(db.IVA, "idIVA", "idIVA", detalle.idIVA);
            ViewBag.codigoProducto = new SelectList(db.Producto, "codigoProducto", "nombreProducto", detalle.codigoProducto);
            return View(detalle);
        }

        // GET: Detalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.numeroFactura = new SelectList(db.Factura, "numeroFactura", "codigoFactura", detalle.numeroFactura);
            ViewBag.idIVA = new SelectList(db.IVA, "idIVA", "idIVA", detalle.idIVA);
            ViewBag.codigoProducto = new SelectList(db.Producto, "codigoProducto", "nombreProducto", detalle.codigoProducto);
            return View(detalle);
        }

        // POST: Detalles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDetalle,numeroFactura,codigoProducto,cantidad,precioUnitario,montoIVA,subTotal,idIVA")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.numeroFactura = new SelectList(db.Factura, "numeroFactura", "codigoFactura", detalle.numeroFactura);
            ViewBag.idIVA = new SelectList(db.IVA, "idIVA", "idIVA", detalle.idIVA);
            ViewBag.codigoProducto = new SelectList(db.Producto, "codigoProducto", "nombreProducto", detalle.codigoProducto);
            return View(detalle);
        }

        // GET: Detalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // POST: Detalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detalle detalle = db.Detalle.Find(id);
            db.Detalle.Remove(detalle);
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
