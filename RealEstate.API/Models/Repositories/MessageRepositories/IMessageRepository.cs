using RealEstate.API.DTOs.MessageDtos;

namespace RealEstate.API.Models.Repositories.MessageRepositories
{
    public interface IMessageRepository
    {
        Task<List<ResultInBoxMessageDto>> GetInBoxLast3MessageListByReceiver(int id); // Aliciya gore yani giris yapan kullaniciya gelen son uc mesaj navbarda listelenecek
        Task<List<ResultInBoxMessageDto>> GetAllMessageInBox(int id); // Giris yapan kisiye ait gelen tum mesajlar
        Task<List<ResultSendBoxMessageDto>> GetAllMessageSendBox(int id); // Giris yapan kisinin gonderdigi mesajlar
        Task CreateMessage(CreateMessageDto createMessageDto);
        Task<GetByIDMessageDto> GetMessage(int messageId);
    }
}
