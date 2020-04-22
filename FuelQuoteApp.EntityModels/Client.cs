using Microsoft.AspNetCore.Identity;
using System;

namespace FuelQuoteApp.EntityModels
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
        public string Email { get; set; }        
        public User User_ID { get; set; }
    }
}
