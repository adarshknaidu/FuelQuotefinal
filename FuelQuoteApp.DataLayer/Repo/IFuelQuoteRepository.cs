using FuelQuoteApp.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuelQuoteApp.DataLayer.Repo
{
    public interface IFuelQuoteRepository
    {
        Client AddClient(Client client);

        User AddUser(User user);

        int GetUserID(string email);

        bool GetClientInfo(int usrID);

        Client GetClient(int UserID);

        Quote AddQuote(Quote quote);

        int GetQuoteHistoryCount(int UserID);

        IEnumerable<Quote> GetQuoteHistory(int UserID);
    }
}
