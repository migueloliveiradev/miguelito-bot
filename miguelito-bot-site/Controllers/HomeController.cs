﻿using DSharpPlus.Entities;
using Microsoft.AspNetCore.Mvc;
using miguelito_bot_site.Models;
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
                if (string.IsNullOrEmpty(Request.Cookies["code"]))
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
                Response.Cookies.Append("code", code, Options);
                string[] infos = Utils.Auth2.Infos(code);
                
                Response.Cookies.Append("id", infos[0], Options);
                Response.Cookies.Append("username", infos[1], Options);
                Response.Cookies.Append("avatar", infos[2], Options);
                Response.Cookies.Append("email", infos[3], Options);
                Response.Redirect("/");
            }
            return View();
        }

        [Route("/commands")]
        public IActionResult Commands()
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
            Response.Redirect("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&redirect_uri=https%3A%2F%2Flocalhost%3A7243%2FHome&response_type=code&scope=guilds%20guilds.join%20email%20identify");
            return null;
        }
        public Action logout()
        {
            Response.Cookies.Delete("code");
            Response.Cookies.Delete("id");
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("avatar");
            Response.Cookies.Delete("email");
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