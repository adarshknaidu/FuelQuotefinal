using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace FuelQuote.DataLayer.Models
{
    public class FuelQuoteDBContext: DbContext
    {
        public FuelQuoteDBContext(DbContextOptions<FuelQuoteDBContext> options): base(options)
        {

        }
    }
}
