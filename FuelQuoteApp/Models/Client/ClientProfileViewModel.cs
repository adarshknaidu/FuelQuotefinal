using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuelQuoteApp.Models.Client
{
    public class ClientProfileViewModel
    {

        [Required(ErrorMessage = "Full name is required!")]
        [StringLength(50,ErrorMessage ="Maximum characters allowed is 50!!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Address 1 is required!")]
        [StringLength(100, ErrorMessage = "Maximum characters allowed is 100!!")]
        public string Address1 { get; set; }

        [StringLength(100, ErrorMessage = "Maximum characters allowed is 100!!")]
        public string Address2 { get; set; }

        [StringLength(100, ErrorMessage = "Maximum characters allowed is 100!!")]
        [Required(ErrorMessage = "City is required!")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required!")]
        public States? State { get; set; }

        [Required(ErrorMessage = "ZipCode is required!")]
        [StringLength(9, MinimumLength =5, ErrorMessage = "The Zipcode must have atleast 5 characters")]
        public string ZipCode { get; set; }

         

    }

}
