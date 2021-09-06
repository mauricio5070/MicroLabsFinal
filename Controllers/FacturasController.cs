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
    public class FacturasController : Controller
    {
        private db_a78ddc_mircrolabsEntities1 db = new db_a78ddc_mircrolabsEntities1();

        // GET: Facturas
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "numeroFactura" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Moneda" : "Date";
            var factura = from s in db.Factura
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                factura = factura.Where(s => s.codigoFactura.Contains(searchString)
                                       || s.codigoFactura.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "codigoFactura":
                    factura = factura.OrderByDescending(s => s.codigoFactura);
                    break;
                case "Date":
                    factura = factura.OrderBy(s => s.fechaCompra);
                    break;
                case "Moneda":
                    factura = factura.OrderByDescending(s => s.monedaID);
                    break;

            }
            return View(factura.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.monedaID = new SelectList(db.Moneda, "monedaID", "tipoMoneda");
            ViewBag.pagoID = new SelectList(db.modoPago, "pagoID", "tipopPago");
            ViewBag.codigoProveedor = new SelectList(db.Proveedor, "codigoProveedor", "nombreProveedor");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "numeroFactura,codigoFactura,fechaCompra,monedaID,tipoCambio,pagoID,codigoProveedor,total")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Factura.Add(factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.monedaID = new SelectList(db.Moneda, "monedaID", "tipoMoneda", factura.monedaID);
            ViewBag.pagoID = new SelectList(db.modoPago, "pagoID", "tipopPago", factura.pagoID);
            ViewBag.codigoProveedor = new SelectList(db.Proveedor, "codigoProveedor", "nombreProveedor", factura.codigoProveedor);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.monedaID = new SelectList(db.Moneda, "monedaID", "tipoMoneda", factura.monedaID);
            ViewBag.pagoID = new SelectList(db.modoPago, "pagoID", "tipopPago", factura.pagoID);
            ViewBag.codigoProveedor = new SelectList(db.Proveedor, "codigoProveedor", "nombreProveedor", factura.codigoProveedor);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "numeroFactura,codigoFactura,fechaCompra,monedaID,tipoCambio,pagoID,codigoProveedor,total")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.monedaID = new SelectList(db.Moneda, "monedaID", "tipoMoneda", factura.monedaID);
            ViewBag.pagoID = new SelectList(db.modoPago, "pagoID", "tipopPago", factura.pagoID);
            ViewBag.codigoProveedor = new SelectList(db.Proveedor, "codigoProveedor", "nombreProveedor", factura.codigoProveedor);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Factura.Find(id);
            db.Factura.Remove(factura);
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
