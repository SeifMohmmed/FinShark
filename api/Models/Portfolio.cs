using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Portfolio
    {
        public string UserId { get; set; }
        public int StockId { get; set; }
        public ApplicationUser User { get; set; }
        public Stock Stock { get; set; }
    }
}