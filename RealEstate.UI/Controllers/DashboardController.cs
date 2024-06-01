using Microsoft.AspNetCore.Mvc;

namespace RealEstate.UI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
