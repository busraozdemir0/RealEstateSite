namespace RealEstate.API.Tools
{
    public class TokenResponseViewModel
    {
        public TokenResponseViewModel(string token, DateTime expiredDate)
        {
            Token = token;
            ExpiredDate = expiredDate;
        }

        public string Token { get; set; } // Token tutulacak
        public DateTime ExpiredDate { get; set; } // Token ne zamana kadar gecerli oldugu bilgisi
    }
}
