using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PembayaranListrik.Models
{
    public class user
    {
        [Key]
        public Int64 id_user { get; set; }
        public string username { get; set; }
        public string nama_admin { get; set; }
        public Int64 id_level { get; set; }
        public string password { get; set; }
    }
}