using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopASP.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mail jest wymagany!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane!")]
        public string Password { get; set; }

        [Display(Name ="Zapamiętaj mnie")]
        public bool RememberMe { get; set; }

    }
}