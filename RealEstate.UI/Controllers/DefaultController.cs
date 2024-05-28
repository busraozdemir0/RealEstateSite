using Microsoft.AspNetCore.Mvc;

namespace RealEstate.UI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
