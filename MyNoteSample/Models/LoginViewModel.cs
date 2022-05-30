using System.ComponentModel.DataAnnotations;

namespace MyNoteSample.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} alanı boş geçilemez"),
            StringLength(30, ErrorMessage = "{0} alanı max {1} karakter olabilir"),
            Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez"),
            StringLength(16, MinimumLength = 6, ErrorMessage = "{0} alanı max {1} min {2} karakter olabilir"),
            Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
