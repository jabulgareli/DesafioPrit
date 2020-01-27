using Prit.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prit.Domain.Aggregates
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string GetPriceToDisplay()
        {
            return Currency.FormatToBRLCurrency(Price);
        }
    }
}
