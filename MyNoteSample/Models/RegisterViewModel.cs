using System.ComponentModel.DataAnnotations;

namespace MyNoteSample.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="{0} alanı boş geçilemez"), 
            StringLength(30, ErrorMessage ="{0} alanı max {1} karakter olabilir"), 
            Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez"), 
            StringLength(60, ErrorMessage = "{0} alanı max {1} karakter olabilir"), 
            EmailAddress(ErrorMessage ="{0} alanına geçerli bir e-posta giriniz."), 
            Display(Name = "E -Posta")]
        public string Email{ get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez"), 
            StringLength(16,MinimumLength =6, ErrorMessage = "{0} alanı max {1} min {2} karakter olabilir"), 
            Display(Name = "Şifre")]
        public string Password{ get; set; }

        // Compaere Repassword ile password u karşılaştırıyor ve eşleşmesini sağlıyor.
        [Required(ErrorMessage = "{0} alanı boş geçilemez"), 
            StringLength(16, MinimumLength = 6, ErrorMessage = "{0} alanı max {1} min {2} karakter olabilir"), 
            Display(Name = "Şifre(Tekrar)"), 
            Compare(nameof(Password), ErrorMessage ="{0} alanaı ile {1} alanı eşleşmiyor")]
        public string RePassword { get; set; }
    }
}