using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppProject.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Column("CommentID")]
        [Key]
        public int Id { get; set; }

        [Column("AnimalID")]
        [Required(ErrorMessage = "Enter a Animal ID")]
        public int AnimalId { get; set; }
        
        public virtual Animal? Animal { get; set; }
            
        [Column("Comment")]
        [Required(ErrorMessage = "Enter a Comment")]
        [MaxLength(255, ErrorMessage = "Comment can't be over 255 characters")]
        [MinLength(3, ErrorMessage = "Comment can't be less than 3 characters")]
        [RegularExpression(@"^(.*[a-zA-Z]){3,}.*$", ErrorMessage = "Comment must have at least three letters")]
        public string? comment { get; set;}

        [Required]
        public string? UserId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
