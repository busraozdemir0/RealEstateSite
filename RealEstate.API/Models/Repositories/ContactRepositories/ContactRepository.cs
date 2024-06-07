using Dapper;
using RealEstate.API.DTOs.ContactDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.ContactRepositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly Context _context;

        public ContactRepository(Context context)
        {
            _context = context;
        }
        public Task CreateContact(CreateContactDto createContactDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContact(int contactId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultContactDto>> GetAllContact()
        {
            throw new NotImplementedException();
        }

        public Task<GetByIDContactDto> GetContact(int contactId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Last4ContactResultDto>> GetLast4Contact()
        {
            string query = "Select Top(4) * From Contact order by ContactID Desc"; // Son gelen 4 mesaj listeleniyor
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<Last4ContactResultDto>(query);
                return values.ToList();
            }
        }
    }
}
