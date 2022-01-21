using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using PembayaranListrik.Models;
using PembayaranListrik.Helper;
using PembayaranListrik.DAL;
using System.Globalization;

namespace PembayaranListrik.Controllers
{
    public class HomePelangganController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        public ActionResult Index()
        {
            string nama = Session["namalengkap"].ToString();
            var result = from s in db.vwTagihan
                         where s.nama_pelanggan==nama
                         select s;
            return View(result.ToList());
        }

     
    }
}