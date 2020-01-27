using System;
using System.Collections.Generic;
using System.Text;

namespace Prit.Domain.ValueObjects
{
    public static class Currency
    {
        public static string FormatToBRLCurrency(decimal value)
        {
            return $"R$ {value}";
        }
    }
}
