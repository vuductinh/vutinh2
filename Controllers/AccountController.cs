using PagedList;
using Shop_Shoes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shop_Shoes.Controllers
{
    public class AccountController : Controller
    {
        Data_Shoes data = new Data_Shoes();
        // GET: Account
        public async Task<ActionResult>Login()
        {
            string usernamecookie = System.Web.HttpContext.Current.User.Identity.Name;
            if(!string.IsNullOrEmpty(usernamecookie))
            {
                return View("Home", "Home");
            }
            return View(new Account());
        }
        [HttpPost]
        public async Task<ActionResult> Login(Account account)
        {
            var ac = data.Accounts.FirstOrDefault(x => x.User_s == account.User_s.Trim());
            if(ModelState.IsValid)
            {
                Account account1 = data.Accounts.FirstOrDefault(x => x.User_s == account.User_s.Trim()&&x.Pass_word==account.Pass_word.Trim());
                if(account1!=null)
                {
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    ModelState.AddModelError("C_User", "Sai tên tài khoản");
                    ModelState.AddModelError("Pass_word", "Sai mật khẩu");
                    return View(account);
                }
            }else
            {
                ModelState.AddModelError("C_User", "Sai tên tài khoản");
                return View(account);
            }
            return View();
        }
        public ActionResult createAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createAccount(Account account)
        {
            data.Accounts.Add(account);
            data.SaveChanges();
            return RedirectToAction("Login","Account");
        }
        public ActionResult listAccount(int ?page,string search)
        {
            int PageNumber=(page ?? 1);
            int PageSize = 11;
            var list = data.Accounts.OrderBy(x => x.User_s).ToPagedList(PageNumber,PageSize);
            if(!string.IsNullOrEmpty(search))
            {
                list = data.Accounts.OrderBy(x => x.User_s).Where(x => x.User_s.Contains(search)).ToPagedList(PageNumber, PageSize);
            }
            return View(list);
        }
        public ActionResult editAccount(int ?id)
        {
            Account ac = data.Accounts.First();
            if(id!=null)
            {
                ac = data.Accounts.FirstOrDefault(x => x.idAccount == id);
            }

            return View(ac);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult editAccount(Account account)
        {
            data.Accounts.Add(account);
            data.Entry(account).State = EntityState.Modified;
            data.SaveChanges();
            return RedirectToAction("listAccount");
        }
    }
}