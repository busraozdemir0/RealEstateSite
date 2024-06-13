namespace RealEstate.UI.DTOs.AppUserDtos
{
    public class CreateAppUserDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserRole { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? UserImageUrl { get; set; }
    }
}
