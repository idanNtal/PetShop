using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppProject.Models
{
    [Table("Animals")]
    public class Animal
    {
        [Column("AnimalID")]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [MaxLength(20, ErrorMessage = "Name cannot exceed 20 characters")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        [RegularExpression("^[A-Za-z\\-]+$", ErrorMessage = "Invalid format for name")]
        [Remote(action: "IsNameUsed", controller: "Admin", AdditionalFields = nameof(Id))]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter an age")]
        [Range(1, 20, ErrorMessage = "Age must be between 1 and 20")]
        public int? Age { get; set; }

        [Remote(action: "IsPngImage", controller: "Admin")]
        [NotMapped]
        public IFormFile? Picture { get; set; }

        [Required(ErrorMessage = "Please select a picture")]
        [MaxLength(25, ErrorMessage = "Picture name cannot exceed 25 characters")]
        public string? PictureName { get; set; }


        [Required(ErrorMessage = "Please enter a description")]
        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters long")]
        public string? Description { get; set; }

        [Column("CategoryID")]
        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
    }

}
