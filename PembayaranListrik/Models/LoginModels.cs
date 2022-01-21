using System.ComponentModel.DataAnnotations;

namespace PembayaranListrik.Models
{
    public class LoginModels
    {
        [Required(ErrorMessage = "Username harus diisi")]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password harus diisi")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}