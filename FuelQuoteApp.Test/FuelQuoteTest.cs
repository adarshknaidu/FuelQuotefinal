using System;
using Xunit;
using FuelQuoteApp.Controllers;
using FuelQuoteApp.Models;
using FuelQuoteApp.Models.Account;
using FuelQuoteApp.Models.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using FuelQuoteApp.DataLayer.Repo;
using FuelQuoteApp.BusinessLayer;

namespace FuelQuoteApp.Test
{
    public class FuelQuoteTest
    {
        private readonly AccountController _controller;
        private readonly ClientController _controllerClient;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IFuelQuoteRepository _FuelQuoteRepo;
        public FuelQuoteTest()
        {
            _controller = new AccountController(userManager, signInManager, _FuelQuoteRepo);
            _controllerClient = new ClientController(_FuelQuoteRepo);
        }
        //[Fact]
        //public void RegisterTest()
        //{
        //    //Hardcoding dummy user Info data
        //    RegisterViewModel registerinfo = new RegisterViewModel()
        //    {
        //        UserName = "New User1",
        //        Email = "newuser1@gmail.com",
        //        Password = "Abcd1234",
        //        ConfirmPassword = "Abcd1234"
        //    };

        //    var result = _controller.RegisterDataValidation(registerinfo);

        //    //Check if the it is returning a view type object or not
        //    Assert.IsType<ViewResult>(result);

        //}

        //[Fact]
        //public void LoginTest()
        //{
        //    //Hardcoding dummy user Info data
        //    LoginViewModel logininfo = new LoginViewModel()
        //    {
        //        Email = "newuser@gmail.com",
        //        Password = "Abcd1234",
        //        RememberMe = false
        //    };

        //    var result = _controller.Login(logininfo);

        //    //Check if the it is returning a view type object or not

        //    Assert.IsType<ViewResult>(result);

        //}

        //[Fact]
        //public void ClientProfileTest()
        //{
        //    ClientProfileViewModel clientProfile = new ClientProfileViewModel()
        //    {
        //        Address1 = "Linkwood Drive",
        //        Address2 = "Apt 641",
        //        City = "Houston",
        //        FullName = "Adarsh",
        //        State = States.AK,
        //        ZipCode = "15212"
        //    };

        //    var result = _controllerClient.ClientProfile(clientProfile);
        //    Assert.IsType<ViewResult>(result);
        //}

        [Fact]
        public void RegisterUserNameValidation()
        {
            //Hardcoding dummy user Info data
            RegisterViewModel registerinfo = new RegisterViewModel()
            {
                UserName = "",
                Email = "newuser1@gmail.com",
                Password = "Abcd1234",
                ConfirmPassword = "Abcd1234"
            };

            var result = _controller.RegisterDataValidation(registerinfo);

            //Check if all the validation passed
            Assert.True(result);

        }

        [Fact]
        public void RegisterPasswordValidation()
        {
            //Hardcoding dummy user Info data
            RegisterViewModel registerinfo = new RegisterViewModel()
            {
                UserName = "newuser",
                Email = "newuser1@gmail.com",
                Password = "Abcd1234",
                ConfirmPassword = "Abcd1234"
            };

            var result = _controller.RegisterDataValidation(registerinfo);

            //Check if all the validation passed
            Assert.True(result);

        }

        [Fact]
        public void RegisterConfirmPasswordValidation()
        {
            //Hardcoding dummy user Info data
            RegisterViewModel registerinfo = new RegisterViewModel()
            {
                UserName = "newuser",
                Email = "newuser1@gmail.com",
                Password = "Abcd1234",
                ConfirmPassword = "" // giving invalid password for testing purpose
            };

            var result = _controller.RegisterDataValidation(registerinfo);

            //Check if all the validation passed
            Assert.True(result);

        }

        [Fact]
        public void ClientFullNameValidation()
        {
            //Hardcoding dummy user Info data
            ClientProfileViewModel clientProfile = new ClientProfileViewModel()
            {
                Address1 = "Linkwood Drive",
                Address2 = "Apt 641",
                City = "Houston",
                FullName = "Adarsh",
                State = States.AK,
                ZipCode = "15212"
            };

            var result = _controllerClient.ClientProfileDataValidation(clientProfile);

            //Check if all the validation passed
            Assert.True(result);

        }

        [Fact]
        public void ClientAddressValidation()
        {
            //Hardcoding dummy user Info data
            ClientProfileViewModel clientProfile = new ClientProfileViewModel()
            {
                Address1 = "",
                Address2 = "Apt 641",
                City = "Houston",
                FullName = "", //passing empty invalid name for testing
                State = States.AK,
                ZipCode = "15212"
            };

            var result = _controllerClient.ClientProfileDataValidation(clientProfile);

            //Check if all the validation passed
            Assert.True(result);

        }


        [Fact]
        public void ClientZipCodeValidation()
        {
            //Hardcoding dummy user Info data
            ClientProfileViewModel clientProfile = new ClientProfileViewModel()
            {
                Address1 = "Linkwood Drive",
                Address2 = "Apt 641",
                City = "Houston",
                FullName = "Adarsh",
                State = States.AK,
                ZipCode = "152dsfsdf12" //passing invalid zipcode
            };

            var result = _controllerClient.ClientProfileDataValidation(clientProfile);

            //Check if all the validation passed
            Assert.True(result);

        }

        [Fact]
        public void PricingModule()
        {
            QuoteDetails quote = new QuoteDetails
            {
                DateRequested = DateTime.Today,
                GallonsRequested = 1500,
                quoteHistory = 1,
                State = "TX"
           };

            float actual = 2610.0f;
          
            PricingModule p = new PricingModule();
            Price price = p.GetPrice(quote);

            Assert.Equal(price.TotalAmount, actual);

        }
    }
}
