using PembayaranListrik.DAL;
using PembayaranListrik.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PembayaranListrik.Controllers
{
    public class TarifController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        // GET: Tarif
        public ActionResult Index()
        {
            var result = from s in db.tarif
                         select s;
            return View(result.ToList());
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "id_tarif,daya,tarifperkwh")] tarif tarif)
        {
            if (ModelState.IsValid)
            {
                db.tarif.Add(tarif);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tarif);


        }


        // POST: tarif/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id_tarif,daya,tarifperkwh")] tarif tarif)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tarif);
        }



        // POST: tarif/Delete/5
        public ActionResult Delete(int idd)
        {
            tarif tarif = db.tarif.Find(idd);
            db.tarif.Remove(tarif);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}