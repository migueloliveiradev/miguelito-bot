using Microsoft.AspNetCore.Mvc;
using miguelito_bot_site.Models;

namespace miguelito_bot_site.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/profile/")]
        public IActionResult Profile()
        {
            User user = new();
            user.username = Request.Cookies["username"];
            user.email = Request.Cookies["email"];
            user.avatar = Request.Cookies["avatar"];

            return View(user);
        }
    }
}
