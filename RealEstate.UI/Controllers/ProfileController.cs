using Microsoft.AspNetCore.Mvc;

namespace RealEstate.UI.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
