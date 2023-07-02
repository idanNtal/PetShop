using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebAppProject.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Enter a UserName")]
        [DataType(DataType.Text)]
        [MinLength(4, ErrorMessage = "length need to be more than 4 chars")]
        [MaxLength(20, ErrorMessage = "length need to be less than 20 chars")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Invalid UserName, Only numbers and letters allow")]
        [Remote(action: "IsUsernameInUse", controller: "Account")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Enter a Password")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+])[a-zA-Z\\d!@#$%^&*()_+]{8,}$", ErrorMessage = "Password is too weak")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [Required(ErrorMessage = "Enter a Password Confirmation")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+])[a-zA-Z\\d!@#$%^&*()_+]{8,}$", ErrorMessage = "Password is too weak")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Enter an Email")]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string? Email { get; set; }

        public bool IsPasswordValid()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }

            // Check if password is at least 8 characters long
            if (Password.Length < 8)
            {
                return false;
            }

            // Add additional validation rules as per your requirements
            // For example, checking for at least one uppercase letter, lowercase letter, digit, etc.

            // Check if password contains at least one uppercase letter
            if (!Regex.IsMatch(Password, "[A-Z]"))
            {
                return false;
            }

            // Check if password contains at least one lowercase letter
            if (!Regex.IsMatch(Password, "[a-z]"))
            {
                return false;
            }

            // Check if password contains at least one digit
            if (!Regex.IsMatch(Password, @"\d"))
            {
                return false;
            }

            // Check if password contains at least one special character
            if (!Regex.IsMatch(Password, @"[^a-zA-Z0-9]"))
            {
                return false;
            }

            return true;
        }
    }
}
