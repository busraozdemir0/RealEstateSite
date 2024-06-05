using Dapper;
using RealEstate.API.DTOs.MessageDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.MessageRepositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly Context _context;

        public MessageRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultInBoxMessageDto>> GetInBoxLast3MessageListByReceiver(int id)
        {
            // Mesaji gonderen kisinin bilgisini alabilmek icin Sender ile AppUser tablosundaki UserId'yi birlestirdik
            string query = "Select Top(3) MessageID, Name, Subject, Detail, SendDate, IsRead, UserImageUrl From Message Inner Join AppUser On Message.Sender=AppUser.UserId Where Receiver=@receiverId Order By MessageID Desc";
            var parameters = new DynamicParameters();
            parameters.Add("@receiverId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultInBoxMessageDto>(query, parameters);
                return values.ToList();
            }
        }
    }
}
