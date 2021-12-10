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
    public class VendedoresController : Controller
    {
        private FacturacionDBEntities db = new FacturacionDBEntities();

        // GET: Vendedores
        public ActionResult Index()
        {
            return View(db.Vendedores.ToList());
        }

        // GET: Vendedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedore vendedore = db.Vendedores.Find(id);
            if (vendedore == null)
            {
                return HttpNotFound();
            }
            return View(vendedore);
        }

        // GET: Vendedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Comision,Estado")] Vendedore vendedore)
        {
            if (ModelState.IsValid)
            {
                db.Vendedores.Add(vendedore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendedore);
        }

        // GET: Vendedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedore vendedore = db.Vendedores.Find(id);
            if (vendedore == null)
            {
                return HttpNotFound();
            }
            return View(vendedore);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Comision,Estado")] Vendedore vendedore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendedore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendedore);
        }

        // GET: Vendedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedore vendedore = db.Vendedores.Find(id);
            if (vendedore == null)
            {
                return HttpNotFound();
            }
            return View(vendedore);
        }

        // POST: Vendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendedore vendedore = db.Vendedores.Find(id);
            db.Vendedores.Remove(vendedore);
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


        [HttpPost]
        public ActionResult Index(string search)
        {

            string name = string.Empty;

            if (string.IsNullOrWhiteSpace(search))
            {
                return View(db.Articulos.ToList());
            }

            string artName = search;


            for (int i = 0; i < artName.Length; i++)
            {
                if (char.IsLetter(artName[i]))
                {
                    name += artName[i];
                }
            }

            var Artics = db.Articulos.Where(m => m.Descripcion.Contains(name)).ToList();

            return View(Artics);

        }

    }
}
