using RealEstate.API.DTOs.ContactDtos;

namespace RealEstate.API.Models.Repositories.ContactRepositories
{
    public interface IContactRepository
    {
        Task<List<ResultContactDto>> GetAllContact();
        Task<List<Last4ContactResultDto>> GetLast4Contact(); // Son 4 mesaji getirecek olan metod imzasi
        Task CreateContact(CreateContactDto createContactDto);
        Task DeleteContact(int contactId);
        Task<GetByIDContactDto> GetContact(int contactId);
    }
}
