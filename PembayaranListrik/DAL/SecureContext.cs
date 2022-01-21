using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PembayaranListrik.DAL
{
    public class SecureContext : DbContext
    {
        public SecureContext()
            : base("name=SecureContext")
        {

        }
    }
}