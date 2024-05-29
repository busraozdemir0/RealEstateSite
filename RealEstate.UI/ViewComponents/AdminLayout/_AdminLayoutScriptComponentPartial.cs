using Microsoft.AspNetCore.Mvc;

namespace RealEstate.UI.ViewComponents.AdminLayout
{
    public class _AdminLayoutScriptComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
