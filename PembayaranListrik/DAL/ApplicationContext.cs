using System.Data.Entity;
using PembayaranListrik.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using PembayaranListrik.ViewModels;

namespace PembayaranListrik.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("name=ApplicationContext")
        {

        }


        public DbSet<user> user { get; set; }
        public DbSet<level> level { get; set; }
        public DbSet<Pelanggan> pelanggan { get; set; }
        public DbSet<pembayaran> pembayaran { get; set; }
        public DbSet<Penggunaan> Penggunaan { get; set; }
        public DbSet<tagihan> tagihan { get; set; }
        public DbSet<tarif> tarif { get; set; }
        public DbSet<joinpenggunaan> joinpenggunaan { get; set; }
        public DbSet<jointarif> jointarif { get; set; }
        public DbSet<joinpembayaran> joinpembayaran { get; set; }
        public DbSet<vwTagihan> vwTagihan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}