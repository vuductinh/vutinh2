using PagedList;
using Shop_Shoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Shoes.Controllers
{
    public class HomeController : Controller
    {
        public Data_Shoes data = new Data_Shoes();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Messenger()
        {
            return View();
        }
        public ActionResult Home(int? page, string search)
        {
            int PageNumber = (page ?? 1);
            int PageSize = 12;
            var Imgitem = (from p in data.Items
                           orderby (p.Name_Item)
                           select p).ToPagedList(PageNumber, PageSize);
            if (!string.IsNullOrEmpty(search))
            {
                Imgitem = (from p in data.Items
                           where (p.Name_Item.Contains(search))
                           orderby (p.Name_Item)
                           select p).ToPagedList(PageNumber, PageSize);
            }
            
            ViewBag.ListImg = Imgitem;
            return View(Imgitem);
        }
        public ActionResult Styles(int ?page,int ?id,int ?id1)
        {
            int PageNumber = (page ?? 1);
            int PageSize = 12;
            var Imgitem=(from p in data.Items
                           where p.IdStyle == id && p.Id_Type==id1
                           orderby (p.Name_Item)
                           select p).ToPagedList(PageNumber, PageSize);

            ViewBag.ListImg = Imgitem;
            return View(Imgitem);
        }
        public ActionResult Shoe_Care()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}