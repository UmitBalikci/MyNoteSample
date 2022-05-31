using System.ComponentModel.DataAnnotations;

namespace MyNoteSample.Models
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "{0} alanı boş geçilemez"), 
            StringLength(40, ErrorMessage = "{0} alanı max {1} karakter olabilir"), 
            Display(Name = "Kategori Adı")]
        public string Name { get; set; }

        [StringLength(160, ErrorMessage = "{0} alanı max {1} karakter olabilir"), 
            Display(Name = "Kategori Açıklaması")]
        public string Description { get; set; }
    }
}
