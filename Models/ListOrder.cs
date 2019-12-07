using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Shoes.Models
{
    public class listOrder
    {
        public int? custumerID { get; set; }
        public DateTime ?dateCreate { get; set; }
        public string shipName { get; set; }
        public string shipMobile { get; set; }
        public string shipAdress { get; set; }
        public string shipEmail { get; set; }
        public string itemName { get; set; }
        public int? priceItem { get; set; }
        public int? quantity { get; set; }
    }
}