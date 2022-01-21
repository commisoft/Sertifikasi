using Newtonsoft.Json;
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
    public class TagihanController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        // GET: Tagihan
        public ActionResult Index()
        {

            string sqlQuery1 = string.Format(@"SELECT  [id_penggunaan]
                                                  ,[nama_pelanggan]
                                                  ,[bulan]
                                                  ,[tahun]
                                                  ,[meter_awal]
                                                  ,[meter_ahir]
                                              FROM [dbo].[penggunaan] as a
                                              join pelanggan as b on a.id_pelanggan=b.id_pelanggan");
            var resultt = db.Database.SqlQuery<joinpenggunaan>(sqlQuery1).ToList();
            ViewBag.penggunaan = new SelectList(resultt, "id_penggunaan", "nama_pelanggan");
            var result = from s in db.vwTagihan
                         select s;
            return View(result.ToList());
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "id_pengunaan,id_pelanggan,bulan,tahun,jumlah_meter,status")] tagihan tagihan)
        {
            if (ModelState.IsValid)
            {
                db.tagihan.Add(tagihan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tagihan);


        }

        public ContentResult GetPelanggan(Int64? id_penggunaan)
        {
            var result = from s in db.Penggunaan
                         where s.id_penggunaan == id_penggunaan
                         select s.id_pelanggan;
            return Content(JsonConvert.SerializeObject(result.FirstOrDefault()), "application/json");
        }




        // POST: tarif/Delete/5
        public ActionResult Delete(int idd)
        {
            tagihan tagihan = db.tagihan.Find(idd);
            db.tagihan.Remove(tagihan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}