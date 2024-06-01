using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace RealEstate.UI.ViewComponents.AdminLayout
{
    public class _AdminLayoutHeadComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
