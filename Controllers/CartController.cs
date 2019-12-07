using Shop_Shoes.Models;
using Shop_Shoes.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace Shop_Shoes.Controllers
{
    public class CartController : Controller
    {
        public Data_Shoes data = new Data_Shoes();
        private  const string CartSession ="CartSession";
        // GET: Cart
        public ActionResult CartItem()
        {
            var Cart = Session[CartSession];
            
            var list =  new List<CartItem>();
            if(Cart!=null)
            {
                list = (List<CartItem>)Cart;
                int ?price = 0;
                foreach(CartItem item in list)
                {
                    price +=(item.cost * item.quantity);
                }
                ViewBag.price = price;
            }
            return View(list);
        }
        //public JsonResult update(string cartModel)
        //{
        //    var jsonCart = new JavaScriptSerializer().Deserialize<List<Item>>(cartModel);
        //    var sessionCart = (List<Item>)Session[CartSession];
        //    foreach(var item in sessionCart)
        //    {
        //        var jsonItem = jsonCart.SingleOrDefault(x => x.IdItem == item.IdItem);
        //        if(jsonItem!=null)
        //        {
        //            item.Amount = jsonItem.Amount;
        //        }
        //    }
        //    Session[CartSession] = sessionCart;
        //    return Json(new {
        //        status = true
        //    });
        //}
        public ActionResult updateCart(int id,FormCollection f)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            CartItem cart = sessionCart.SingleOrDefault(x => x.ID == id);
            if(cart!=null)
            {
                cart.quantity = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("CartItem");
        }
        public ActionResult AddItem(int productID,int quantity,string name,int cost,string name_img)
        {
            var Cart =Session[CartSession];
            if(Cart!=null)
            {
                var list = (List<CartItem>)Cart;
                //nếu tồn tại Item
                if(list.Exists(x=>x.ID==productID))
                {
                    foreach (var item in list)
                    {
                        if (item.ID == productID)
                        {
                            item.quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.ID = productID;
                    item.quantity = quantity;
                    item.Name= name;
                    item.cost = cost;
                    item.link = name_img;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.ID = productID;
                item.quantity = quantity;
                item.Name = name;
                item.cost = cost;
                item.link = name_img;
                var list = new List<CartItem>();
                list.Add(item);
                //gán vào Session
                Session[CartSession] = list;
            }
            return RedirectToAction("CartItem");
        }
        public ActionResult deleteCart(int id)
        {
            var sesssionCart = (List<CartItem>)Session[CartSession];
            CartItem item = sesssionCart.SingleOrDefault(x => x.ID == id);
            if(item!=null)
            {
                sesssionCart.RemoveAll(x => x.ID == id);
            }
            return RedirectToAction("CartItem");
        }
        public ActionResult formOrder()
        {
            var Cart = Session[CartSession];

            var list = new List<CartItem>();
            if (Cart != null)
            {
                list = (List<CartItem>)Cart;
                int price = 0;
                foreach (CartItem item in list)
                {
                    price += (item.cost * item.quantity);
                }
                ViewBag.price = price;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult formOrder(string shipName,string mobile,string adress,string email)
        {
            Order_s order = new Order_s();
            order.CreateDate = DateTime.Now;
            order.ShipName = shipName;
            order.Shipmobile = mobile;
            order.ShipAdress = adress;
            order.ShipEmail = email;
            var id = new oderDao().Insert(order);
            var cart=(List<CartItem>)Session[CartSession];
            try
            {
                foreach (CartItem item in cart)
                {
                    OrderDetail detail = new OrderDetail();
                    detail.productID = item.ID;
                    detail.OrderID = id;
                    detail.price = item.cost;
                    detail.quantity = item.quantity;
                    data.OrderDetails.Add(detail);
                    data.SaveChanges();
                }
                return Redirect("Success");
            }
            catch(Exception ex)
            {
                Redirect("lỗi");
            }
            return View();
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}