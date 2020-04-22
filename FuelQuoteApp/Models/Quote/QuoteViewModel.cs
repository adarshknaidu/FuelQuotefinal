using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuelQuoteApp.Models.Quote
{
    public class QuoteViewModel
    {
        [Required(ErrorMessage = "Please enter the required quantity of fuel!")]
        public int GallonsRequested { get; set; }

        public string DeliveryAddress { get; set; }

        [Required(ErrorMessage = "Please enter the date!")]
        [DataType(DataType.Date)]
        public DateTime DateRequested { get; set; } = DateTime.Now;
       
        public float PricePerGallon { get; set; }

        public float TotalAmount { get; set; }

        public string buttonType { get; set; }


    }
}
