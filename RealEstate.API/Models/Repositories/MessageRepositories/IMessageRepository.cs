using RealEstate.API.DTOs.MessageDtos;

namespace RealEstate.API.Models.Repositories.MessageRepositories
{
    public interface IMessageRepository
    {
        Task<List<ResultInBoxMessageDto>> GetInBoxLast3MessageListByReceiver(int id); // Aliciya gore yani giris yapan kullaniciya gelen son uc mesaj navbarda listelenecek
    }
}
