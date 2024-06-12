namespace RealEstate.UI.DTOs.AppUserDtos
{
    public class ResultAppUserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; } // Kullanicinin rol adini getirebilmek icin back-ed tarafinda iki tablo birbirine entegre edildi
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserImageUrl { get; set; }
    }
}
