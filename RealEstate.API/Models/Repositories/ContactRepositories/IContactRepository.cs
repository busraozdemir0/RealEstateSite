using RealEstate.API.DTOs.ContactDtos;

namespace RealEstate.API.Models.Repositories.ContactRepositories
{
    public interface IContactRepository
    {
        Task<List<ResultContactDto>> GetAllContactAsync();
        Task<List<Last4ContactResultDto>> GetLast4Contact(); // Son 4 mesaji getirecek olan metod imzasi
        void CreateContact(CreateContactDto createContactDto);
        void DeleteContact(int contactId);
        Task<GetByIDContactDto> GetContact(int contactId);
    }
}
