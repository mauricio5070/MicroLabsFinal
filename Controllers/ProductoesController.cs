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
    public class ProductoesController : Controller
    {
        private db_a78ddc_mircrolabsEntities1 db = new db_a78ddc_mircrolabsEntities1();

        // GET: Productoes
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var productos = from s in db.Producto
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(s => s.nombreProducto.Contains(searchString)
                                       || s.nombreQuickbooks.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    productos = productos.OrderByDescending(s => s.nombreProducto);
                    break;
                case "Date":
                    productos = productos.OrderBy(s => s.fechaVencimiento);
                    break;
                case "date_desc":
                    productos = productos.OrderByDescending(s => s.cantCajas);
                    break;
                default:
                    productos = productos.OrderBy(s => s.nombreQuickbooks);
                    break;
            }
            return View(productos.ToList());
        }




        // GET: Productoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            ViewBag.codigoProveedor = new SelectList(db.Proveedor, "codigoProveedor", "nombreProveedor");
            return View();
        }

        // POST: Productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoProducto,nombreProducto,tipoProducto,descripcionProducto,fechaVencimiento,cantCajas,cantUnidades,codigoProveedor,nombreQuickbooks,totalProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigoProveedor = new SelectList(db.Proveedor, "codigoProveedor", "nombreProveedor", producto.codigoProveedor);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigoProveedor = new SelectList(db.Proveedor, "codigoProveedor", "nombreProveedor", producto.codigoProveedor);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoProducto,nombreProducto,tipoProducto,descripcionProducto,fechaVencimiento,cantCajas,cantUnidades,codigoProveedor,nombreQuickbooks,totalProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigoProveedor = new SelectList(db.Proveedor, "codigoProveedor", "nombreProveedor", producto.codigoProveedor);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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
