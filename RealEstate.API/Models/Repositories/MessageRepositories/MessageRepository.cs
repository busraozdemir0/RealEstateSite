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

        public async Task CreateMessage(CreateMessageDto createMessageDto)
        {
            // Gelen Emaile gore UserId bulma islemi
            string queryFindUserIdByEmail = "SELECT UserId FROM AppUser WHERE Email = @email"; // Mesaj gonderme kisminda girilen emaile gore kullanicinin id'sini cekme
            var parametersForEmail = new DynamicParameters();
            parametersForEmail.Add("@email", createMessageDto.ReceiverEmail);

            using (var connection = _context.CreateConnection())
            {
                var userId = await connection.QuerySingleOrDefaultAsync<int?>(queryFindUserIdByEmail, parametersForEmail);

                if (userId.HasValue)
                {
                    createMessageDto.Receiver = userId.Value; // Alicinin emailine gore kullanici id'sini cektik ve bu id'yi burada atadik

                    string query = "INSERT INTO Message (Sender, Receiver, Subject, Detail, SendDate, IsRead) VALUES (@sender, @receiver, @subject, @detail, @sendDate, @isRead)";
                    var parameters = new DynamicParameters();
                    parameters.Add("@sender", createMessageDto.Sender);
                    parameters.Add("@receiver", userId.Value);
                    parameters.Add("@subject", createMessageDto.Subject);
                    parameters.Add("@detail", createMessageDto.Detail);
                    parameters.Add("@sendDate", createMessageDto.SendDate);
                    parameters.Add("@isRead", createMessageDto.IsRead);

                    await connection.ExecuteAsync(query, parameters);
                }
                else
                {
                    // Kullanici bulunamadiysa
                    throw new Exception("Kullanıcı bulunamadı");
                }
            }
        }

        public async Task<List<ResultInBoxMessageDto>> GetAllMessageInBox(int id) // Giris yapan kisiye ait gelen tum mesajlar
        {
            string query = "Select MessageID, Name, Subject, Detail, SendDate, IsRead, UserImageUrl From Message Inner Join AppUser On Message.Sender=AppUser.UserId Where Receiver=@receiverId Order By SendDate Desc";
            var parameters = new DynamicParameters();
            parameters.Add("@receiverId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultInBoxMessageDto>(query, parameters);
                return values.ToList();
            }
        }

        public  async Task<List<ResultSendBoxMessageDto>> GetAllMessageSendBox(int id) // Giris yapan kisinin gonderdigi mesajlar
        {
            string query = "Select MessageID, Name, Subject, Detail, SendDate, IsRead, UserImageUrl From Message Inner Join AppUser On Message.Receiver=AppUser.UserId Where Sender=@senderId Order By SendDate Desc";
            var parameters = new DynamicParameters();
            parameters.Add("@senderId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultSendBoxMessageDto>(query, parameters);
                return values.ToList();
            }
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

        public async Task<GetByIDMessageDto> GetMessage(int messageId)
        {
            string query = "Select * From Message Inner Join AppUser On Message.Sender=AppUser.UserId Where MessageID=@messageID";
            var parameters = new DynamicParameters();
            parameters.Add("@messageID", messageId);
            using (var connection = _context.CreateConnection()) 
            {
                var value = await connection.QueryFirstOrDefaultAsync<GetByIDMessageDto>(query, parameters);
                return value;
            }
        }
    }
}
