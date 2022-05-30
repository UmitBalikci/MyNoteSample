using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNoteSample.Models.Entities
{
    [Table("EmailMemberships")]
    public class EmailMembership : EntityLogBase
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(60), EmailAddress, Display(Name = "E-Posta")]
        public string EMailAddress { get; set; }
    }
}
