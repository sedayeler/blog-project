//using BlogProject.Application.Abstractions.Services;
//using BlogProject.Application.DTOs;
//using BlogProject.Domain.Entities;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;

//namespace BlogProject.Infrastructure.Services
//{
//    public class TokenService : ITokenService
//    {
//        private readonly IConfiguration _configuration;

//        public TokenService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public TokenDto CreateAccessToken(int minute, User user)
//        {
//            TokenDto token = new TokenDto();

//            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
//            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

//            token.Expiration = DateTime.UtcNow.AddMinutes(minute); //.AddHours(3)
//            JwtSecurityToken securityToken = new(
//                issuer: _configuration["Token:Issuer"],
//                audience: _configuration["Token:Audience"],
//                expires: token.Expiration,
//                notBefore: DateTime.UtcNow,
//                signingCredentials: signingCredentials,
//                claims: new List<Claim> { new(ClaimTypes.Name, user.UserName) });

//            JwtSecurityTokenHandler tokenHandler = new();
//            token.AccessToken = tokenHandler.WriteToken(securityToken);
//            token.RefreshToken = CreateRefreshToken();
//            return token;
//        }

//        public string CreateRefreshToken()
//        {
//            byte[] number = new byte[32];
//            using RandomNumberGenerator random = RandomNumberGenerator.Create();
//            random.GetBytes(number);
//            return Convert.ToBase64String(number);
//        }
//    }
//}
