using System;
using System.Collections.Generic;
using System.Text;

namespace FuelQuoteApp.BusinessLayer
{
    public class QuoteDetails
    {
        public int GallonsRequested { get; set; }
        public DateTime DateRequested { get; set; }
        public string State { get; set; }
        public int quoteHistory { get; set; }

    }
}
