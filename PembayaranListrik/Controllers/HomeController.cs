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
    public class HomeController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        public int itemPerPage = 10;
        public ActionResult Index(string tanggall, string startcar, string endcar, string sortby)
        {
   //         string idAdmin = String.Empty;

   //         if (Session["UserID"]== null)
   //         {
   //             return RedirectToAction("Index", "Login");
   //         }
   //         else
   //         {

   //              idAdmin = Session["UserID"].ToString();
   //         }

   //         string keytanggal = string.Empty;
   //         string keyrangecar = string.Empty;
   //         #region Filter By Tgl
   //         if (!String.IsNullOrEmpty(tanggall))
   //         {
   //             if (tanggall.ToUpper() != "FALSE")
   //             {
   //                 string strStart = Convert.ToDateTime(tanggall.Split('-')[0].Trim()).ToString("yyyy-MM-dd");
   //                 string strEnd = Convert.ToDateTime(tanggall.Split('-')[1].Trim()).ToString("yyyy-MM-dd");
   //                 keytanggal = "tanggal between '" + strStart + "' and  '" + strEnd + "'";

   //                 ViewBag.rangeDate = tanggall;
   //             }
   //         }
   //         else
   //         {

   //             keytanggal = "month(tanggal)='" + DateTime.Now.Month + "'";
   //         }
   //         if (!string.IsNullOrEmpty(startcar))
   //         {
                
   //                 keyrangecar = "where  nama_golfcar between '" + startcar + "' and  '" + endcar + "'";
               
   //             ViewBag.startcar = startcar;
   //             ViewBag.endcar = endcar;
   //         }
   //         var ResultsPrediksi = db.Database.SqlQuery<ViewTap>(queryCustom(keytanggal, keyrangecar)).ToList();
   //         if (!string.IsNullOrEmpty(sortby))
   //         {
   //             if (sortby == "asc")
   //             {

   //                 ResultsPrediksi = ResultsPrediksi.OrderBy(s => s.pemakaian).ToList();
   //             }
   //             else
   //             {

   //                 ResultsPrediksi = ResultsPrediksi.OrderByDescending(s => s.pemakaian).ToList();
   //             }
   //             ViewBag.sort = sortby;
   //         }
   //         else
   //         {

   //             ResultsPrediksi = ResultsPrediksi.OrderByDescending(s => s.pemakaian).ToList();
   //         }
            
   //         //mobil pemakaian
          

   //         #endregion

   //         //menu

   //         string sqlQuery = string.Format(@"SELECT  c.title,
   //         c.id_menu,
   //         c.is_main_menu,
   //         c.url,
   //         c.icon
   //         FROM admin as a inner join
   //         tbl_hakakses as b on a.id_admin=b.id_admin inner join
   //         tbl_menu as c on c.id_menu=b.id_menu  
   //         where a.id_admin='{0}'
			//order by title asc", idAdmin);
   //         var Results = db.Database.SqlQuery<tbl_menu>(sqlQuery).ToList();
   //         ViewBag.menu = Results;
   //         ViewBag.curentController = "Dashboard";

            //return View(ResultsPrediksi.ToList());
            return View();
        }

        private static string queryCustom(string keytanggal, string keyrangecar)
        {
            string queryViewLaporan = string.Format(@"
        SELECT
        ROW_NUMBER() OVER(ORder by golfcar.id_golfcar)as id,
        golfcar.id_golfcar,
        golfcar.nama_golfcar,
        golfcar.jenis,
        golfcar.warna,
         cast(cast(b.pemakaian  as numeric(18,1))as varchar)+' Jam'as pemakaian
        FROM
        golfcar
        INNER JOIN (
        SELECT  format(CAST(t.time_sum AS numeric)/3600,'##0.0') as pemakaian,
        id_golfcar
        FROM ( 
        SELECT SUM(DATEDIFF(S, '00:00:00', durasi)) AS time_sum,
       id_golfcar FROM transaksiGolf 
	   where  {0} 
	   group by id_golfcar) t
) as b on b.id_golfcar = golfcar.id_golfcar 
{1}
group by  golfcar.id_golfcar,
        golfcar.nama_golfcar,
        golfcar.jenis,
	 b.pemakaian,
	golfcar.warna, b.pemakaian ", keytanggal, keyrangecar);
            return queryViewLaporan;
        }
    }
}