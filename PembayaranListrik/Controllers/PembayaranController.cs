using Newtonsoft.Json;
using PembayaranListrik.DAL;
using PembayaranListrik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PembayaranListrik.Controllers
{
    public class PembayaranController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        
        public ActionResult Index()
        {

            ViewBag.tagihan = new SelectList(db.vwTagihan, "id_tagihan", "nama_pelanggan");
            string sqlQuery = string.Format(@"SELECT [id_pembayaran]
                                                  ,[id_tagihan]
                                                  ,nama_pelanggan
                                                  ,[tanggal_pembayaran]
                                                  ,[bulan_bayar]
                                                  ,[biaya_admin]
                                                  ,[total_bayar]
                                                  ,[id_user]
                                              FROM [dbo].[pembayaran] as a
                                              join pelanggan as b on a.id_pelanggan=b.id_pelanggan");
            var result = db.Database.SqlQuery<joinpembayaran>(sqlQuery).ToList();
            return View(result.ToList());
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "id_tagihan,id_pelanggan,tanggal_pembayaran,bulan_bayar,biaya_admin,total_bayar,id_user")] pembayaran pembayaran)
        {
            if (ModelState.IsValid)
            {
                db.pembayaran.Add(pembayaran);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pembayaran);


        }


        public ContentResult GetPelanggan(Int64? idtagihan)
        {
            var result = from s in db.tagihan
                         where s.id_tagihan==idtagihan
                         select s.id_pelanggan;
            return Content(JsonConvert.SerializeObject(result.FirstOrDefault()), "application/json");
        }



        // POST: tarif/Delete/5
        public ActionResult Delete(int idd)
        {
            pembayaran pemabayaran = db.pembayaran.Find(idd);
            db.pembayaran.Remove(pemabayaran);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}