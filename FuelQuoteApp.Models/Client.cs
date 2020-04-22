using System;

namespace FuelQuoteApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; } 
        public string State { get; set; }      
        public string ZipCode { get; set; }
    }
}
