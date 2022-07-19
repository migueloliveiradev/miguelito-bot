using Microsoft.AspNetCore.Mvc.RazorPages;

namespace miguelito_bot_site.Views.Home
{
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
            Console.WriteLine("DashboardModel");
        }


    }
}