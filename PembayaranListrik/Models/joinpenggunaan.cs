using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PembayaranListrik.Models
{
    public class joinpenggunaan
    {
        [Key]
        public Int64 id_penggunaan { get; set; }
        public string nama_pelanggan { get; set; }
        public string bulan { get; set; }
        public string tahun { get; set; }
        public Int32 meter_awal { get; set; }
        public Int32 meter_ahir { get; set; }
    }
}