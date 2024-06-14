using RealEstate.API.DTOs.AppUserDtos;

namespace RealEstate.API.Models.Repositories.AppUserRepositories
{
    public interface IAppUserRepository
    {
        Task<GetAppUserByProductIdDto> GetAppUserByProductId(int id); // ilani ekleyen kullanicinin bilgilerini gosterecegiz
        Task<List<ResultAppUserDto>> GetAllAppUser();
        Task CreateAppUser(CreateAppUserDto createAppUserDto);
        Task DeleteAppUser(int appUserId);
        Task UpdateAppUser(UpdateAppUserDto updateAppUserDto);
        Task<GetByIDAppUserDto> GetAppUser(int appUserId);
        Task<ProfileUpdateDto> GetLoginUserProfile(int appUserId); // Giris yapan kullanicinin bilgilerini guncelleme sayfasinda getirecek olan metod
    }
}
