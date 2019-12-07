using PagedList;
using Shop_Shoes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using Shop_Shoes.ViewModel;
namespace Shop_Shoes.Controllers
{
    public class AdminController : Controller
    {

        Data_Shoes data = new Data_Shoes();
        private int ?id1;
        // GET: Admin
        public ActionResult ListItems(int ?page,string search)
        { 
            int PageNumber = (page ?? 1);
            int PageSize = 11;
            var Listitem = data.Items.OrderBy(x => x.Name_Item).ToPagedList(PageNumber,PageSize);
            var Nametype = (from p in data.Type_s select p).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                Listitem = data.Items.OrderBy(x => x.Name_Item).Where(x => x.Name_Item.Contains(search)).ToPagedList(PageNumber, PageSize);
            }
            ViewBag.List = Listitem;
            ViewBag.ListType = Nametype;
            return View(Listitem);
        }
        public ActionResult CreateItem()
        {
            ViewBag.Type = data.Type_s.Select(x => x.Name_Type).ToList();
            ViewBag.Style = data.Styles.Select(p => p.NameStyle).ToList();

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateItem(Item item,HttpPostedFileBase fileUpload,string nameType,string nameStyle)
        {
            if (ModelState.IsValid)
            {
                //Lưu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);
                //Lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/Content/Image"), fileName);
                //Kiểm tra hình ảnh đã tồn tại chưa
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                var IdTypes = from p in data.Type_s where(p.Name_Type==nameType) select p.IdType;
                var IdStyle = from p in data.Styles where (p.NameStyle == nameStyle) select p.IdStyle;
                item.linkImg = fileUpload.FileName;
                foreach (int i in IdTypes) { item.Id_Type = i; }
                foreach(int i in IdStyle) { item.IdStyle = i; }
                data.Items.Add(item);
                data.SaveChanges();
                return RedirectToAction("ListItems");
            }
            return View(item);

        }
        [HttpGet]
        public ActionResult EditItem(int ?id)
        {
            
            Item item = data.Items.First();
            var listitem = data.Items.Where(x=>x.IdItem==id).ToList();
            
            foreach (Item it in listitem) { 
                ViewBag.Name = it.Name_Item;
                var listType = from p in data.Type_s where(p.IdType==it.Id_Type) select p.Name_Type;
                foreach(string i in listType) { ViewBag.NameType = i; }
                var listStyle = from p in data.Styles where (p.IdStyle == it.IdStyle) select p.NameStyle;
                foreach (string i in listStyle) { ViewBag.NameStyle = i; }
            }
            if (id!=null)
            {
                item= data.Items.SingleOrDefault(x => x.IdItem == id);
            }
            return View(item);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditItem(Item item,HttpPostedFileBase fileUpload,string NameType,string NameStyle)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Image"), fileUpload.FileName);
                if(System.IO.File.Exists(path))
                {
                    ViewBag.thongbao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                item.linkImg = fileUpload.FileName;
                
                var IdTypes = from p in data.Type_s where (p.Name_Type == NameType) select p.IdType;
                var IdStyle = from p in data.Styles where (p.NameStyle == NameStyle) select p.IdStyle;
                foreach (int i in IdTypes) { item.Id_Type = i; }
                foreach (int i in IdStyle) { item.IdStyle = i; }
                data.Items.Add(item);
                data.Entry(item).State = EntityState.Modified;
                data.SaveChanges();
                return RedirectToAction("ListItems");
            }
            return View(item);
        }
        public ActionResult DetialItem(int ?id)
        {
            Item item = data.Items.First();
            if (id!=null)
            {
                item = data.Items.SingleOrDefault(x => x.IdItem == id);
            }
            ViewBag.Name = item.Name_Item;
            ViewBag.Made_in = item.Made_in;
            ViewBag.Size = item.Size;
            ViewBag.Color = item.Color;
            ViewBag.Amount = item.Amount;
            ViewBag.Cost = item.cost;
            ViewBag.linkImg = item.linkImg;
            return View();
        }
        [HttpGet]
        public ActionResult DeleteItem(int ?id)
        {
            DetailDelete(id);
            return RedirectToAction("ListItems");
        }
        [HttpPost,ActionName("DeleteItem")]
        public ActionResult DetailDelete(int ?id)
        {
            Item item = data.Items.First();
            if(id!=null)
            {
                item = data.Items.SingleOrDefault(x => x.IdItem == id);
            }
            data.Items.Remove(item);
            data.SaveChanges();
            return Content("Xóa Thành công!");
        }
        public ActionResult listOrder(int ?page,string Search)
        {
            int PageNumber = (page ?? 1);
            int PageSize = 11;
            var list = (from p in data.Order_s
                        join c in data.OrderDetails on p.orderID equals c.OrderID
                        join d in data.Items on c.productID equals d.IdItem
                        orderby (p.ShipName)
                        select new listOrder
                        {
                            custumerID = p.CustomerID,
                            dateCreate =p.CreateDate,
                            shipName = p.ShipName,
                            shipMobile = p.Shipmobile,
                            shipAdress = p.ShipAdress,
                            shipEmail = p.ShipEmail,
                            itemName = d.Name_Item,
                            priceItem = c.price,
                            quantity = c.quantity
                        }).ToPagedList(PageNumber, PageSize);
            if (!string.IsNullOrEmpty(Search))
            {
                list = (from p in data.Order_s
                            join c in data.OrderDetails on p.orderID equals c.OrderID
                            join d in data.Items on c.productID equals d.IdItem
                            orderby (p.ShipName)
                            where (p.ShipName.Contains(Search))
                            select new listOrder
                            {
                                custumerID = p.CustomerID,
                                dateCreate = p.CreateDate,
                                shipName = p.ShipName,
                                shipMobile = p.Shipmobile,
                                shipAdress = p.ShipAdress,
                                shipEmail = p.ShipEmail,
                                itemName = d.Name_Item,
                                priceItem = c.price,
                                quantity = c.quantity
                            }).ToPagedList(PageNumber, PageSize);
            }
            ViewBag.list = list;
            return View(list);
        }
    }
}