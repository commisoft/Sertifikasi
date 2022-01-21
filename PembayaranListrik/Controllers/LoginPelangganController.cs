using PembayaranListrik.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PembayaranListrik.Controllers
{
    public class LoginPelangganController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        //
        // GET: /Login/
        [AllowAnonymous]
        public ActionResult Index(string errorMessage)
        {
            if (HttpContext.Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (DateTime.Now.ToString("yyyy-MM-dd") == "2021-02-25")
            {
                return RedirectToAction("Index", "Expired");
            }
            ViewBag.errorMessage = errorMessage;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Loginsi(string username, string password)
        {
            var pass = GetMD5(password);
            var obj = db.pelanggan.Where(a => a.username.Equals(username) && a.password.Equals(pass)).FirstOrDefault();
            if (obj != null)
            {
                Session["UserID"] = obj.id_pelanggan.ToString();
                Session["UserName"] = obj.username.ToString();
                Session["namalengkap"] = obj.nama_pelanggan.ToString();
                Session["id_level"] = "2";
                return RedirectToAction("Index", "HomePelanggan");
            }
            else
            {

                return RedirectToAction("Index", "LoginPelanggan", new { errorMessage = "Email Atau Password Salah" });
            }


        }
        public ActionResult Logout()
        {

            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

    }
}