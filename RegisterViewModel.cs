
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestFinal.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action:"IsEmailInUse",controller:"Account")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Oras { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile ProfilePhoto { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NumberPhone { get; set; }
    }
}
