using Microsoft.AspNetCore.Mvc;

namespace RealEstate.UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
