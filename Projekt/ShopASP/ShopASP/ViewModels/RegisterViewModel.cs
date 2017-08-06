using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopASP.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "E-mail jest wymagany!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane!")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć więcej niż {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane!")]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie są jednakowe!")]
        public string ConfirmPassword { get; set; }
    }
}