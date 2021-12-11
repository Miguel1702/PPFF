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
    public class Asientos_ContablesController : Controller
    {
        private FacturacionDBEntities db = new FacturacionDBEntities();

        // GET: Asientos_Contables
        public ActionResult Index()
        {
            return View(db.Asientos_Contables.ToList());
        }

        // GET: Asientos_Contables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asientos_Contables asientos_Contables = db.Asientos_Contables.Find(id);
            if (asientos_Contables == null)
            {
                return HttpNotFound();
            }
            return View(asientos_Contables);
        }

        // GET: Asientos_Contables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Asientos_Contables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_Contabilidad,Descripcion,ID_Auxiliar,Codigo_Moneda,Cuenta_CR,MontoCR,Cuenta_DB,MontoDB")] Asientos_Contables asientos_Contables)
        {
            if (ModelState.IsValid)
            {
                db.Asientos_Contables.Add(asientos_Contables);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asientos_Contables);
        }

        // GET: Asientos_Contables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asientos_Contables asientos_Contables = db.Asientos_Contables.Find(id);
            if (asientos_Contables == null)
            {
                return HttpNotFound();
            }
            return View(asientos_Contables);
        }

        // POST: Asientos_Contables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_Contabilidad,Descripcion,ID_Auxiliar,Codigo_Moneda,Cuenta_CR,MontoCR,Cuenta_DB,MontoDB")] Asientos_Contables asientos_Contables)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asientos_Contables).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asientos_Contables);
        }

        // GET: Asientos_Contables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asientos_Contables asientos_Contables = db.Asientos_Contables.Find(id);
            if (asientos_Contables == null)
            {
                return HttpNotFound();
            }
            return View(asientos_Contables);
        }

        // POST: Asientos_Contables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asientos_Contables asientos_Contables = db.Asientos_Contables.Find(id);
            db.Asientos_Contables.Remove(asientos_Contables);
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
