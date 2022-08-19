using DSharpPlus.Entities;
using Microsoft.AspNetCore.Mvc;
using miguelito_bot_site.Models;
using miguelito_bot_site.Utils;
using System.Diagnostics;

namespace miguelito_bot_site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
      
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string code = HttpContext.Request.Query["code"];
            if (string.IsNullOrEmpty(code))
            {
                if (string.IsNullOrEmpty(Request.Cookies["token"]))
                {
                    return View();
                }
            }
            else if (!string.IsNullOrEmpty(code))
            {
                CookieOptions Options = new()
                {
                    IsEssential = true,
                    Expires = DateTime.Now.AddMonths(1),
                    Secure = true,
                    HttpOnly = true,
                };
                string token = Auth2.Token(code).Result;
                Response.Cookies.Append("token", token, Options);
                Response.Redirect("/");
            }
            return View();
        }

        [Route("/commands")]
        public IActionResult Commands()
        {
            return View();
        }
        [Route("/support")]
        public IActionResult Support()
        {
            return View();
        }
        [Route("/donate")]
        public IActionResult Donate()
        {
            return View();
        }

        [Route("/404")]
        public IActionResult Status404()
        {
            return View();
        }

        [Route("/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        public Action Login()
        {
            Response.Redirect("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&redirect_uri=https%3A%2F%2Flocalhost%3A7243%2FHome&response_type=code&scope=guilds%20guilds.join%20guilds.members.read%20email%20identify");
            return null;
        }
        public Action logout()
        {
            Response.Cookies.Delete("token");
            Response.Redirect("/");
            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}