namespace RealEstate.UI.Models
{
    public class JwtResponseModel
    {
        public TokenModel Token { get; set; }
        public string RedirectUrl { get; set; }
    }

    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
