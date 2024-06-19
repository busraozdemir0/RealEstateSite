using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.ToDoListDtos;
using RealEstate.API.Models.Repositories.ToDoListRepositories;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private readonly IToDoListRepository _toDoListRepository;

        public ToDoListsController(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ToDoListList()
        {
            var values = await _toDoListRepository.GetAllToDoList();
            return Ok(values);
        }

        [HttpGet("GetAllToDoListStatusFalse")]
        public async Task<IActionResult> GetAllToDoListStatusFalse()
        {
            var values = await _toDoListRepository.GetAllToDoListStatusFalse();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToDoList(CreateToDoListDto createToDoListDto)
        {
            await _toDoListRepository.CreateToDoList(createToDoListDto);
            return Ok("Görev başarılı bir şekilde eklendi.");
        }

        [HttpDelete("{toDoListId}")]
        public async Task<IActionResult> DeleteToDoList(int toDoListId)
        {
            await _toDoListRepository.DeleteToDoList(toDoListId);
            return Ok("Görev başarılı bir şekilde silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateToDoList(UpdateToDoListDto updateToDoListDto)
        {
            await _toDoListRepository.UpdateToDoList(updateToDoListDto);
            return Ok("Görev başarılı bir şekilde güncellendi.");
        }

        [HttpGet("{toDoListId}")]
        public async Task<IActionResult> GetToDoList(int toDoListId)
        {
            var value = await _toDoListRepository.GetToDoList(toDoListId);
            return Ok(value);
        }

        [HttpGet("ToDoListStatusChangeToTrue/{id}")]
        public async Task<IActionResult> ToDoListStatusChangeToTrue(int id)
        {
            await _toDoListRepository.ToDoListStatusChangeToTrue(id);
            return Ok("Görev aktif olarak işaretlendi.");
        }

        [HttpGet("ToDoListStatusChangeToFalse/{id}")] 
        public async Task<IActionResult> ToDoListStatusChangeToFalse(int id)
        {
            await _toDoListRepository.ToDoListStatusChangeToFalse(id);
            return Ok("Görev pasif olarak işaretlendi.");
        }
    }
}
