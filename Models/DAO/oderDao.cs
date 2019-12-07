using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Shoes.Models.DAO
{
    public class oderDao
    {
        Data_Shoes data = null;
        public oderDao()
        {
            data = new Data_Shoes();
        }
        public int Insert(Order_s order)
        {
            data.Order_s.Add(order);
            data.SaveChanges();
            return order.orderID;
        }
    }
}