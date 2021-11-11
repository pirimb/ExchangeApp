using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace currency_exchange.Models
{
    public class PassedData
    {
        public string Date { get; set; }
        public string FromValute { get; set; }
        public string ToValute { get; set; }
        public double AmountMoney { get; set; }
    }                                   
}
