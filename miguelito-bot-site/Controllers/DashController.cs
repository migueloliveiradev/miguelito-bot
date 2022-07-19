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
        public IActionResult Dashboard()
        {
            List<DiscordGuild> guild = Utils.Auth2.Guilds(Request.Cookies["id"]).Result;
            guild _guild = new guild()
            {
                guilds = guild
            };

            return View(_guild);
        }
    }
}
