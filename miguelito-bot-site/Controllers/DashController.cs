using Microsoft.AspNetCore.Mvc;

namespace miguelito_bot_site.Controllers
{
    public class DashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("/dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
