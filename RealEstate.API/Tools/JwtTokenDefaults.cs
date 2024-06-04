namespace RealEstate.API.Tools
{
    // https://tugrulbayrak.medium.com/jwt-json-web-tokens-nedir-nasil-calisir-5ca6ebc1584a
    public class JwtTokenDefaults
    {
        public const string ValidAudience = "https://localhost";
        public const string ValidIssuer = "https://localhost";
        public const string Key = "REALestate..5267856765Asp.NetCore8.0.1*-+/";
        public const int Expire = 5; // Token gecerlilik suresi 5 dk
    }
}
