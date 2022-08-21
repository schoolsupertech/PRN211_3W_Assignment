using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{

    [Route("account")]
    public class AccountController : Controller
    {
        IMemberRepository members= null;
        public AccountController() => members= new MemberRepository();

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var mem = new Member();
            if ( username.Equals("admin") && password.Equals("111"))
            {
              HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
               
                var list = members.LoginMember(username, password);
                
              foreach (var member in list)
                {
                    mem.Email = member.Email;
                    mem.Password = member.Password;
                    mem.City= member.City;
                    mem.MemberId = member.MemberId;
                    mem.CompanyName = member.CompanyName;
                    mem.Country= member.Country;
                }
                return RedirectToAction("Index", "Member", mem);
                
            }
            if (username == null)
            {
                ViewBag.error = "Invalid Account";
                return View("Index");
            }

        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
    }
}