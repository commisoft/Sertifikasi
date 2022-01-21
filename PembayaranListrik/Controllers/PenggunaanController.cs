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
    public class PenggunaanController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
     
        public ActionResult Index()
        {
          
            ViewBag.Penggunaan = new SelectList(db.pelanggan, "id_pelanggan", "nama_pelanggan");
            string sqlQuery = string.Format(@"SELECT  [id_penggunaan]
                                                  ,[nama_pelanggan]
                                                  ,[bulan]
                                                  ,[tahun]
                                                  ,[meter_awal]
                                                  ,[meter_ahir]
                                              FROM [dbo].[penggunaan] as a
                                              join pelanggan as b on a.id_penggunaan=b.id_pelanggan");
            var result = db.Database.SqlQuery<joinpenggunaan>(sqlQuery).ToList();
            return View(result.ToList());
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "id_pelanggan,bulan,tahun,meter_awal,meter_ahir")] Penggunaan penggunaan)
        {
            if (ModelState.IsValid)
            {
                db.Penggunaan.Add(penggunaan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(penggunaan);


        }


        // POST: penggunaan/Edit/5
       



        // POST: penggunaan/Delete/5
        public ActionResult Delete(int idd)
        {
            Penggunaan penggunaan = db.Penggunaan.Find(idd);
            db.Penggunaan.Remove(penggunaan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}