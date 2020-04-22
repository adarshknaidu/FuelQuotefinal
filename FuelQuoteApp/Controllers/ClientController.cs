using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelQuoteApp.Models;
using FuelQuoteApp.Models.Client;
using Microsoft.AspNetCore.Mvc;
using FuelQuoteApp.EntityModels;
using FuelQuoteApp.DataLayer.Repo;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace FuelQuoteApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IFuelQuoteRepository _FuelQuoteRepo;

        public ClientController(IFuelQuoteRepository FuelQuoteRepo)
        {
            _FuelQuoteRepo = FuelQuoteRepo;

        }
        [HttpGet]
        public IActionResult ClientDashBoard()
        {
            int usrID = _FuelQuoteRepo.GetUserID(User.Identity.Name);
            Client client = _FuelQuoteRepo.GetClient(usrID);
            HttpContext.Session.SetString("ClientDetails", JsonConvert.SerializeObject(client));

            return View();
        }
        [HttpGet]
        public IActionResult ClientProfile()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ClientProfile(ClientProfileViewModel clientProfile)
        {
            if (ModelState.IsValid)
            {
                Client client = new Client
                {
                    FullName = clientProfile.FullName,
                    Address1 = clientProfile.Address1,
                    Address2 = clientProfile.Address2,
                    City = clientProfile.City,
                    State = clientProfile.State.ToString(),
                    ZipCode = clientProfile.ZipCode,
                    Email = User.Identity.Name
                    
                };

                User usrDetails = new User();
                client.User_ID = usrDetails;
                client.User_ID.Id = _FuelQuoteRepo.GetUserID(User.Identity.Name);
                Client AddedClient = _FuelQuoteRepo.AddClient(client);

                return RedirectToAction("ClientDashBoard", "Client");
            }
            else
            {
                return View();
            }
       
        }

        [HttpGet]
        public IActionResult DisplayProfile()
        {
            Client client = new Client();
            client = JsonConvert.DeserializeObject<Client>(HttpContext.Session.GetString("ClientDetails"));
         

            ClientProfileViewModel cl = new ClientProfileViewModel()
            {
                FullName = client.FullName,
                Address1 = client.Address1,
                Address2 = client.Address2,
                City = client.City,
                State = States.AK,
                ZipCode = client.ZipCode
            };

            return View(cl);
        }

        public bool ClientProfileDataValidation(ClientProfileViewModel data)
        {
            bool flag = false;
            if ((data.FullName.Length <= 50) && (data.FullName != String.Empty))
            {
                if (((data.Address1.Length <= 100) && (data.Address1 != String.Empty)) && (data.Address2.Length <= 100))
                {
                    if ((data.City.Length <= 100) && (data.City != String.Empty))
                    {
                        if (data.ZipCode.Length <= 9 && data.ZipCode.Length >= 5)
                        {
                            flag = true;
                        }
                    }
                }
            }
            else
            {
                flag = false;
            }

            return flag;
        }


    }
}