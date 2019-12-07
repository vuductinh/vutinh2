using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Shoes.Models.DAO
{
    public class orderDeatialDao
    {
        Data_Shoes data = null;
        public orderDeatialDao()
        {
            data = new Data_Shoes();
        }
        public bool Insert(OrderDetail detail)
        {
           
                data.OrderDetails.Add(detail);
                data.SaveChanges();
                return true;
         
        }
    }
}