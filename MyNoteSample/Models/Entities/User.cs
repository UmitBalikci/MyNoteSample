using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNoteSample.Models.Entities
{
    [Table("Users")]
    public class User : EntityLogBase
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60), Display(Name = "Ad Soyad")]
        public string FullName { get; set; }

        [Required, StringLength(30), Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required, StringLength(60), EmailAddress, Display(Name = "E-Posta")]
        public string Email { get; set; }

        [Required, StringLength(100), Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }

        [Display(Name = "Yönetici")]
        public bool IsAdmin { get; set; }

        //public virtual List<Note> Notes { get; set; } 
        public virtual List<Note> LikedNotes { get; set; } 
    }
}
