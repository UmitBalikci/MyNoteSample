using System.ComponentModel.DataAnnotations;

namespace MyNoteSample.Models
{
    public class NoteViewModel
    {
        [Required(ErrorMessage = "{0} alanı boş geçilemez"), 
            StringLength(80, ErrorMessage = "{0} alanı max {1} karakter olabilir"), 
            Display(Name = "Başlık")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez"), 
            StringLength(250, ErrorMessage = "{0} alanı max {1} karakter olabilir"), 
            Display(Name = "Özet")]
        public string Summary { get; set; }

        [Display(Name = "Detay")]
        public string Detail { get; set; }

        [Display(Name = "Taslak")]
        public string IsDraft { get; set; }


        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
    }
}
