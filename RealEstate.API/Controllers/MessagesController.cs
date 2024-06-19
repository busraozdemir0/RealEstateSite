using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.MessageDtos;
using RealEstate.API.Models.Repositories.MessageRepositories;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessagesController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetInBoxLast3MessageListByReceiver(int id)
        {
            var values = await _messageRepository.GetInBoxLast3MessageListByReceiver(id);
            return Ok(values);
        }

        [HttpGet("GetAllMessageInBox/{id}")]
        public async Task<IActionResult> GetAllMessageInBox(int id)
        {
            var values = await _messageRepository.GetAllMessageInBox(id);
            return Ok(values);
        }

        [HttpGet("GetAllMessageSendBox/{id}")]
        public async Task<IActionResult> GetAllMessageSendBox(int id)
        {
            var values = await _messageRepository.GetAllMessageSendBox(id);
            return Ok(values);
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> GetMessage(int messageId)
        {
            var values = await _messageRepository.GetMessage(messageId);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto)
        {
            await _messageRepository.CreateMessage(createMessageDto);
            return Ok("Mesaj başarılı bir şekilde gönderildi.");
        }
    }
}
