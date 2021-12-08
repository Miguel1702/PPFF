using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PPFF.Models;

namespace PPFF.Controllers
{
    public class FacturacionsController : Controller
    {
        private FacturacionDBEntities db = new FacturacionDBEntities();

        // GET: Facturacions
        public ActionResult Index()
        {
            var facturacions = db.Facturacions.Include(f => f.Articulo).Include(f => f.Cliente).Include(f => f.Vendedore);
            return View(facturacions.ToList());
        }

        // GET: Facturacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion facturacion = db.Facturacions.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            return View(facturacion);
        }

        // GET: Facturacions/Create
        public ActionResult Create()
        {
            ViewBag.ID_Articulo = new SelectList(db.Articulos, "ID", "Descripcion");
            ViewBag.ID_Cliente = new SelectList(db.Clientes, "ID", "Nombre");
            ViewBag.ID_Vendedor = new SelectList(db.Vendedores, "ID", "Nombre");
            return View();
        }

        // POST: Facturacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_Vendedor,ID_Cliente,Fecha,Comentario,ID_Articulo,Cantidad,Precio_unidad")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                db.Facturacions.Add(facturacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Articulo = new SelectList(db.Articulos, "ID", "Descripcion", facturacion.ID_Articulo);
            ViewBag.ID_Cliente = new SelectList(db.Clientes, "ID", "Nombre", facturacion.ID_Cliente);
            ViewBag.ID_Vendedor = new SelectList(db.Vendedores, "ID", "Nombre", facturacion.ID_Vendedor);
            return View(facturacion);
        }

        // GET: Facturacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion facturacion = db.Facturacions.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Articulo = new SelectList(db.Articulos, "ID", "Descripcion", facturacion.ID_Articulo);
            ViewBag.ID_Cliente = new SelectList(db.Clientes, "ID", "Nombre", facturacion.ID_Cliente);
            ViewBag.ID_Vendedor = new SelectList(db.Vendedores, "ID", "Nombre", facturacion.ID_Vendedor);
            return View(facturacion);
        }

        // POST: Facturacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_Vendedor,ID_Cliente,Fecha,Comentario,ID_Articulo,Cantidad,Precio_unidad")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Articulo = new SelectList(db.Articulos, "ID", "Descripcion", facturacion.ID_Articulo);
            ViewBag.ID_Cliente = new SelectList(db.Clientes, "ID", "Nombre", facturacion.ID_Cliente);
            ViewBag.ID_Vendedor = new SelectList(db.Vendedores, "ID", "Nombre", facturacion.ID_Vendedor);
            return View(facturacion);
        }

        // GET: Facturacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion facturacion = db.Facturacions.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            return View(facturacion);
        }

        // POST: Facturacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facturacion facturacion = db.Facturacions.Find(id);
            db.Facturacions.Remove(facturacion);
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
