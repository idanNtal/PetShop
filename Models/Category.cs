using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppProject.Models
{
    [Table("Categories")]
    public class Category
    {
        [Column("CategoryID")]
        [Key]
        public int Id { get; set; }
       
        [Required(ErrorMessage = "Enter a Name")]
        [MaxLength(50, ErrorMessage = "Name can't be over 50 characters")]
        public string? Name { get; set; }
       
        public virtual ICollection<Animal>? Animals { get; set;}
    }
}
