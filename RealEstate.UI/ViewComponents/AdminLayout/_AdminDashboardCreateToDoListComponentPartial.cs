using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.ToDoListDtos;
using RealEstate.UI.Models;
using System.Net.Http;
using System.Text;

namespace RealEstate.UI.ViewComponents.AdminLayout
{
    public class _AdminDashboardCreateToDoListComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CreateToDoListDto createToDoListDto)
        {
            return View(createToDoListDto);
        }
    }
}
