using DSharpPlus.Entities;
using Microsoft.AspNetCore.Mvc;
using miguelito_bot_site.Models;

namespace miguelito_bot_site.Controllers
{
    public class DashController : Controller
    {
        public static Dictionary<ulong, List<DiscordGuild>> guildsDic = new();
        public IActionResult Index()
        {
            return View();
        }

        [Route("/dashboard")]
        public ActionResult Dashboard()
        {
            IEnumerable<DiscordGuild> guild = Utils.Auth2.Guilds(Request.Cookies["token"]).Result;
            Guild _guild = new()
            {
                Guilds = guild
            };
             return View(_guild);
           
        }
        public Action Login()
        {
            Response.Redirect("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&redirect_uri=https%3A%2F%2Flocalhost%3A7243%2FHome&response_type=code&scope=guilds%20guilds.join%20guilds.members.read%20email%20identify");
            return null;
        }
    }
}