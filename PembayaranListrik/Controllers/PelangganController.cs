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
    public class PelangganController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        // GET: Pelanggan
        public ActionResult Index()
        {

            ViewBag.tarif = new SelectList(db.tarif, "id_tarif", "daya", "tarifperkwh");
            string sqlQuery = string.Format(@"SELECT id_pelanggan
                                                      ,username
                                                      ,password
                                                      ,nomor_kwh
                                                      ,nama_pelanggan
                                                      ,alamat
                                                      ,a.id_tarif
	                                                  ,daya
	                                                  ,tarifperkwh
                                                  FROM dbo.pelanggan as a 
                                                  join tarif as b on a.id_tarif=b.id_tarif");
            var result = db.Database.SqlQuery<jointarif>(sqlQuery).ToList();
            return View(result.ToList());
        }

        [HttpPost]
        public ActionResult Create(Pelanggan pelanggan)
        {
          
                Pelanggan pelanggan1 = new Pelanggan();
                pelanggan1.username = pelanggan.username;
                pelanggan1.password = CryptorHelper.Encrypt(pelanggan.password, "MD5", true); ;
                pelanggan1.nomor_kwh = pelanggan.nomor_kwh;
                pelanggan1.nama_pelanggan = pelanggan.nama_pelanggan;
                pelanggan1.alamat = pelanggan.alamat;
                pelanggan1.id_tarif = pelanggan.id_tarif;
                db.pelanggan.Add(pelanggan);
                db.SaveChanges();
                return RedirectToAction("Index");


        }


        // POST: pelanggan/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id_pelanggan,nama_pelanggan,alamat,nomor_kwh,id_tarif")] Pelanggan pelanggan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pelanggan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pelanggan);
        }



        // POST: pelanggan/Delete/5
        public ActionResult Delete(int idd)
        {
            Pelanggan pelanggan = db.pelanggan.Find(idd);
            db.pelanggan.Remove(pelanggan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}