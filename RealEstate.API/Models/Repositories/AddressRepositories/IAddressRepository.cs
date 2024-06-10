using RealEstate.API.DTOs.AddressDtos;

namespace RealEstate.API.Models.Repositories.AddressRepositories
{
    public interface IAddressRepository
    {
        Task<GetByIDAddressDto> GetAddress(int addressId);
        Task UpdateAddress(UpdateAddressDto updateAddressDto);
    }
}
