using RealEstate.API.DTOs.AppRoleDtos;

namespace RealEstate.API.DTOs.AppUserDtos
{
    public class UpdateAppUserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserRole { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserImageUrl { get; set; }
    }
}
