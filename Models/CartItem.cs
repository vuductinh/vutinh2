using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop_Shoes.Models
{
    [Serializable]
    public class CartItem
    {
        Data_Shoes data = new Data_Shoes();
        public int ID { set; get; }
        public int quantity { set; get; }
        public string Name { get; set; }
        public string link { get; set; }
        public int cost { get; set; }
        public CartItem() { }
        //public CartItem(int id)
        //{
        //    ID = id;
        //    Item item = data.Items.Single(x => x.IdItem == id);
        //    Name = item.Name_Item;
        //    link = item.linkImg;
        //    cost = item.cost;
        //    quantity = 1;
        //}
    }
}