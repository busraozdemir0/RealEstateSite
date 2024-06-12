namespace RealEstate.API.DTOs.AppUserDtos
{
    public class CreateAppUserDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserRole { get; set; } // Listelenen roller arasindan secim yapildiginda arkaplanda rolun id bilgisi kaydedilecegi icin int olarak tutulmaktadir.
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserImageUrl { get; set; }
    }
}
