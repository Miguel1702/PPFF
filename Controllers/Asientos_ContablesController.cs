using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
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
        public async Task<ActionResult> Create([Bind(Include = "ID,ID_Contabilidad,Descripcion,ID_Auxiliar,Codigo_Moneda,Cuenta_CR,MontoCR,Cuenta_DB,MontoDB")] Asientos_Contables asientos_Contables)
        {
            if (ModelState.IsValid)
            {
                Body body = new Body();
                body.description = asientos_Contables.Descripcion;
                body.auxiliar = asientos_Contables.ID_Auxiliar;
                body.currencyCode = asientos_Contables.Codigo_Moneda;
                detail detail = new detail();
                detail.cuentaCR = asientos_Contables.Cuenta_CR;
                detail.cuentaDB = asientos_Contables.Cuenta_DB;
                detail.amountCR = asientos_Contables.MontoCR;
                detail.amountDB = asientos_Contables.MontoDB;
                body.detail = detail;

                string Baseurl = "https://accountingaccountapi20211205021409.azurewebsites.net/";
                var client = new HttpClient();
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(body));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                content.Headers.ContentType.CharSet = "utf-8";
                HttpResponseMessage Res = await client.PostAsync("api/AccountingSeat/Register", content);
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    resp Respu = JsonConvert.DeserializeObject<resp>(Response);

                    asientos_Contables.ID_Contabilidad = int.Parse(Respu.id);

                    db.Asientos_Contables.Add(asientos_Contables);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                db.Asientos_Contables.Add(asientos_Contables);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Error en el guardado";
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
    public class Body
    {
        public string description { get; set; }
        public int auxiliar { get; set; }
        public int currencyCode { get; set; }
        public detail detail { get; set; }
    }

    public class detail
    {
        public string cuentaCR { get; set; }
        public string cuentaDB { get; set; }
        public decimal amountCR { get; set; }
        public decimal amountDB { get; set; }
    }

    public class resp
    {
        public string id { get; set; }
        public int value { get; set; }
    }
}
