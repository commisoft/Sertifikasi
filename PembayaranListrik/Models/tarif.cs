using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PembayaranListrik.Models
{
    public class tarif
    {
        [Key]
        public Int64 id_tarif { get; set; }
        public string daya { get; set; }
        public string tarifperkwh { get; set; }
    }
}