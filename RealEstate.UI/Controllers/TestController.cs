using Microsoft.AspNetCore.Mvc;

namespace RealEstate.UI.Controllers
{
    public class TestController : Controller
    {
        // SignalR baglanti durumunu test etmek icin kullanilmaktadir.
        public IActionResult Index()
        {
            return View();
        }
    }
}
