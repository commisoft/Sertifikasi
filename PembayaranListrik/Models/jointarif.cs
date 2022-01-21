using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PembayaranListrik.Models
{
    public class jointarif
    {
        [Key]
        public Int64 id_pelanggan { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Int32 nomor_kwh { get; set; }
        public string nama_pelanggan { get; set; }
        public string alamat { get; set; }
        public Int64 id_tarif { get; set; }
        public string daya { get; set; }
        public string tarifperkwh { get; set; }
    }
}