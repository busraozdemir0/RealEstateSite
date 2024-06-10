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
        public async Task CreateContact(CreateContactDto createContactDto)
        {
            string query = "insert into Contact (Name, Subject, Email, Message, SendDate) values (@name, @subject, @email, @message, @sendDate)";
            var parameters = new DynamicParameters();
            parameters.Add("@name", createContactDto.Name);
            parameters.Add("@subject", createContactDto.Subject);
            parameters.Add("@email", createContactDto.Email);
            parameters.Add("@message", createContactDto.Message);
            parameters.Add("@sendDate", DateTime.Now);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultContactDto>> GetAllContact()
        {
            string query = "Select * From Contact";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultContactDto>(query);
                return values.ToList();
            }
        }

        public async Task DeleteContact(int contactId)
        {
            string query = "Delete From Contact Where ContactID=@contactID";
            var parameters = new DynamicParameters();
            parameters.Add("@contactID", contactId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<GetByIDContactDto> GetContact(int contactId)
        {
            string query = "Select * From Contact Where ContactID=@contactID";
            var parameters = new DynamicParameters();
            parameters.Add("@contactID", contactId);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<GetByIDContactDto>(query, parameters);
                return value;
            }
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
