using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace currency_exchange.Models
{
    public class ExchangeViewModel
    {
        public ValCurs valCurs { get; set; }
        public PassedData passedData { get; set; }
        public List<string> codes { get; set; }
    }
}
