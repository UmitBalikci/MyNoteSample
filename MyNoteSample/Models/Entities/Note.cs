using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNoteSample.Models.Entities
{
    [Table("Notes")]
    public class Note:EntityLogBase
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(80), Display(Name = "Başlık")]
        public string Title { get; set; }

        [Required, StringLength(250), Display(Name = "Özet")]
        public string Summary { get; set; }

        [Display(Name ="Detay")]
        public string Detail { get; set; }

        [Display(Name ="Taslak")]
        public string IsDraft { get; set; }


        [Display(Name ="Kategori")]
        public int CategoryId{ get; set; }

        [Display(Name = "Yazar")]
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Comment> Comments{ get; set; }
        //[NotMapped]
        //public virtual List<User> LikedUsers { get; set; }
        public virtual List<LikedNotes> Likes { get; set; }
    }
}
