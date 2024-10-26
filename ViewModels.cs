using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMaster
{
    internal class ViewModels
    {

    }

    public class PortfolioViewModel
    {
        public string Name { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Sum { get; set; }
    }

    public class UserViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
    public class StockViewModel
    {
        public string Name { get; set; }
        public string Sector { get; set; }
        public decimal? BuyPrice { get; set; }
    }
}
