using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PembayaranListrik.ViewModels
{
    public class vwTagihan
    {
        [Key]
        public Int64 id_tagihan { get; set; }
        public Int64 id_pengunaan { get; set; }
        public string nama_pelanggan { get; set; }
        public string bulan { get; set; }
        public string tahun { get; set; }
        public Int32 meter_awal { get; set; }
        public Int32 meter_ahir { get; set; }
        public Int32 jumlah_meter { get; set; }
        public string status { get; set; }
    }
}