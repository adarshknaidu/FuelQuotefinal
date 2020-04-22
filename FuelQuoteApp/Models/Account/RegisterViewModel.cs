using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuelQuoteApp.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "UserName is required!")]
        [StringLength(50, ErrorMessage = "Maximum characters allowed is 50!!")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(50, ErrorMessage = "Maximum characters allowed is 50!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match!")]
        public string ConfirmPassword { get; set; }
    }
}
