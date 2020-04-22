using FuelQuoteApp.DataLayer.Models;
using FuelQuoteApp.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime;

namespace FuelQuoteApp.DataLayer.Repo
{
    public class FuelQuoteRepository: IFuelQuoteRepository
    {
        private readonly FuelQuoteDBContext context;

        public FuelQuoteRepository(FuelQuoteDBContext context)
        {
            this.context = context;
        }

        public Client AddClient(Client client)
        {
            context.Client.Add(client);
            context.SaveChanges();
            return client;
        }

        public User AddUser(User user)
        {
            context.UsersInfo.Add(user);
            context.SaveChanges();
            return user;
        }

        public int GetUserID(string email)
        {
            User a = context.UsersInfo.FirstOrDefault(c => c.Email == email);
            return a.Id;
        }

        public bool GetClientInfo(int usrID)
        {
            Client c = context.Client.FirstOrDefault(x => x.User_ID.Id== usrID);
            if (c == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Client GetClient(int UserID)
        {
            Client c = context.Client.FirstOrDefault(x => x.User_ID.Id == UserID);
            return c;
        }

        public Quote AddQuote(Quote quote)
        {
            context.FuelQuote.Add(quote);
            context.SaveChanges();
            return quote;
        }

        public int GetQuoteHistoryCount(int UserID)
        {
            IEnumerable<Quote> quotes= context.FuelQuote.Where(x => x.User.Id == UserID);
            int count = quotes.Count();
            return count;
        }

        public IEnumerable<Quote> GetQuoteHistory(int UserID)
        {
            IEnumerable<Quote> quotes = context.FuelQuote.Where(x => x.User.Id == UserID);
            
            return quotes;
        }
    }
}
