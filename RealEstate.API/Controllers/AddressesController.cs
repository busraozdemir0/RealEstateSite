using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.AddressDtos;
using RealEstate.API.Models.Repositories.AddressRepositories;


namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;

        public AddressesController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetAddress(int addressId)
        {
            var value = await _addressRepository.GetAddress(addressId);
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress(UpdateAddressDto updateAddressDto)
        {
            await _addressRepository.UpdateAddress(updateAddressDto);
            return Ok("Adres bilgileri başarılı bir şekilde güncellendi.");
        }

    }
}
