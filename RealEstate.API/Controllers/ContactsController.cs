using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.ContactDtos;
using RealEstate.API.Models.Repositories.ContactRepositories;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet("GetLast4Contact")]
        public async Task<IActionResult> GetLast4Contact()
        {
            var values = await _contactRepository.GetLast4Contact();
            return Ok(values);
        }

        [HttpGet("AdminContactList")]
        public async Task<IActionResult> ContactList()
        {
            var values = await _contactRepository.GetAllContact();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            await _contactRepository.CreateContact(createContactDto);
            return Ok("Mesajınız başarıyla gönderildi.");
        }

        [HttpDelete("{contactId}")] 
        public async Task<IActionResult> DeleteContact(int contactId)
        {
            await _contactRepository.DeleteContact(contactId);
            return Ok("Mesaj başarılı bir şekilde silindi.");
        }

        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetContact(int contactId)
        {
            var value = await _contactRepository.GetContact(contactId);
            return Ok(value);
        }
    }
}
