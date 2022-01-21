using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PembayaranListrik.Models
{
    public class pembayaran
    {
        [Key]
        public Int64 id_pembayaran { get; set; }
        public Int64 id_tagihan { get; set; }
        public Int64 id_pelanggan { get; set; }
        public DateTime tanggal_pembayaran { get; set; }
        public string bulan_bayar { get; set; }
        public decimal biaya_admin { get; set; }
        public decimal total_bayar { get; set; }
        public Int64 id_user { get; set; }
    }
}