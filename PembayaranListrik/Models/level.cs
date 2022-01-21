using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PembayaranListrik.Models
{
    public class level
    {
        [Key]
        public Int64 id_level { get; set; }
        public string nama_level { get; set; }
    }
}