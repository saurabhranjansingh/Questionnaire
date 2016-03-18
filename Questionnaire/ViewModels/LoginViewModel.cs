using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}