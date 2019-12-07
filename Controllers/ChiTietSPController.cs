using Shop_Shoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Shoes.Controllers
{
    public class ChiTietSPController : Controller
    {
        Data_Shoes data = new Data_Shoes();
        // GET: ChiTietSP
        public ActionResult ChiTiet(int ?id)
        {
            var _item = data.Items.Where(x => x.IdItem == id).ToList();
            var type = (from p in data.Type_s join c in data.Items on p.IdType equals c.Id_Type where c.IdItem == id select p).ToList();
            ViewBag.Item = _item;
            ViewBag.ID = id;
            foreach(Type_s item in type.ToList()) { ViewBag.type = item.Name_Type; }
            return View();
        }
    }
}