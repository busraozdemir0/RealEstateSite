using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstate.API.Tools
{
    // Token olusturma islemleri
    public class JwtTokenGenerator
    {
        public static TokenResponseViewModel GenerateToken(GetCheckAppUserViewModel model)
        {
            var claims = new List<Claim>();
            /*
             string.IsNullOrEmpty yalnizca dizgenin null veya bos olup olmadigini kontrol eder.
             string.IsNullOrWhiteSpace dizgenin null, bos veya yalnizca bosluk karakterlerinden olusup olusmadigini kontrol eder.
             */
            if (!string.IsNullOrWhiteSpace(model.Role)) // Gelen rol'de bir deger varsa
                claims.Add(new Claim(ClaimTypes.Role, model.Role));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()));

            if (!string.IsNullOrWhiteSpace(model.UserName)) // Kullanici adi null degilse veya bosluk karakteri degilse

                claims.Add(new Claim("UserName", model.UserName));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)); // UTF8 ile turkce karakteri de destekleyecegi anlamina geliyor ve bu key JwtTokenDefaults classinda kendimiz yazdigimiz rastgele key degeridir.
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire); // Token'in gecerlilik suresi icin su anki tarih zaman dilimine JwtTokenDefaults classinda tanimladigimiz Expire'da belirtileni ekliyoruz.
           
            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefaults.ValidIssuer, audience: JwtTokenDefaults.ValidAudience, claims: claims,
                notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: signingCredentials);
           
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return new TokenResponseViewModel(tokenHandler.WriteToken(token), expireDate);
        }
    }
}
