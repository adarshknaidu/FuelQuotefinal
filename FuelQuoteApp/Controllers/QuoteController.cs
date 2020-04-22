using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelQuoteApp.Models.Quote;
using Microsoft.AspNetCore.Mvc;
using FuelQuoteApp.BusinessLayer;
using FuelQuoteApp.EntityModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using FuelQuoteApp.DataLayer;
using FuelQuoteApp.DataLayer.Repo;

namespace FuelQuoteApp.Controllers
{
    public class QuoteController : Controller
    {
        private readonly IFuelQuoteRepository _FuelQuoteRepo;
        public QuoteController(IFuelQuoteRepository FuelQuoteRepo)
        {
            _FuelQuoteRepo = FuelQuoteRepo;
        }
        [HttpGet]
        public IActionResult GetQuote()
        {
            QuoteViewModel quote = new QuoteViewModel();
            Client client = new Client();
            client = JsonConvert.DeserializeObject<Client>(HttpContext.Session.GetString("ClientDetails"));
            quote.DeliveryAddress = client.Address1 + " " + client.Address2;
            return View(quote);
        }

        [HttpPost]
        public IActionResult GetQuote(QuoteViewModel quote)
        {
          
            Price pricedetails = new Price();
            pricedetails = GetPrice(quote);
            quote.PricePerGallon = pricedetails.PricePerGallon;
            quote.TotalAmount = pricedetails.TotalAmount;
            HttpContext.Session.SetString("QuoteDetails", JsonConvert.SerializeObject(quote));

            return RedirectToAction("GetFinalQuote");         

        }

        [HttpGet]
        public IActionResult QuoteHistory()
        {

            int usrID = _FuelQuoteRepo.GetUserID(User.Identity.Name);
            IEnumerable<Quote> quotess = _FuelQuoteRepo.GetQuoteHistory(usrID);

            return View(quotess);
        }


        public Price GetPrice(QuoteViewModel quote)
        {
            int usrID = _FuelQuoteRepo.GetUserID(User.Identity.Name);
          
            Client client = new Client();
            client = JsonConvert.DeserializeObject<Client>(HttpContext.Session.GetString("ClientDetails"));
            QuoteDetails quoteInfo = new QuoteDetails
            {
                DateRequested = quote.DateRequested,
                GallonsRequested = quote.GallonsRequested,
                State = client.State,
                quoteHistory = _FuelQuoteRepo.GetQuoteHistoryCount(usrID)
        };

            PricingModule getPrice = new PricingModule();
            Price price = getPrice.GetPrice(quoteInfo);


            return price;
        }

        [HttpGet]
        public IActionResult GetFinalQuote()
        {
            QuoteViewModel quote = new QuoteViewModel();
            Client client = new Client();
            quote = JsonConvert.DeserializeObject<QuoteViewModel>(HttpContext.Session.GetString("QuoteDetails"));
            client = JsonConvert.DeserializeObject<Client>(HttpContext.Session.GetString("ClientDetails"));
            quote.DeliveryAddress = client.Address1 + " " + client.Address2;
            return View(quote);
            
        }

        [HttpPost]
        public IActionResult GetFinalQuote(QuoteViewModel quote)
        {
            quote = JsonConvert.DeserializeObject<QuoteViewModel>(HttpContext.Session.GetString("QuoteDetails"));
            Client client = new Client();
            client = JsonConvert.DeserializeObject<Client>(HttpContext.Session.GetString("ClientDetails"));
            quote.DeliveryAddress = client.Address1 + " " + client.Address2;
                       
            Quote dbquote = new Quote
            {
                DateRequested = quote.DateRequested,
                DeliveryAddress = quote.DeliveryAddress,
                GallonsRequested = quote.GallonsRequested,
                PricePerGallon = quote.PricePerGallon,
                TotalAmount = quote.TotalAmount,
                
            };
            User userinfo = new User();
            dbquote.User = userinfo;
            dbquote.User.Id = _FuelQuoteRepo.GetUserID(User.Identity.Name); 
            dbquote = _FuelQuoteRepo.AddQuote(dbquote);
            return View("SavedQuote",dbquote);
        }
    }
}