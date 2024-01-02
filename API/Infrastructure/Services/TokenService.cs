using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Core.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;

        private readonly SymmetricSecurityKey key;

        public TokenService(IConfiguration _configuration, UserManager<User> _userManager)
        {
            configuration = _configuration;
            userManager = _userManager;
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));
        }
        public async Task<UserDTO> CreateTokenAsync(User user)
        {
            var userDto = new UserDTO();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken token = new JwtSecurityToken(issuer: configuration["Token:Issure"],
                                              expires: DateTime.Now.AddMinutes(30),
                                              claims: claims,
                                              signingCredentials: credentials);
            userDto.Token = new JwtSecurityTokenHandler().WriteToken(token);
            userDto.TokenExpiration = token.ValidTo;

            //Check if User have any active refresh token
            if ( user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(f => f.IsActive);
                userDto.RefreshToken = activeRefreshToken.Token;
                userDto.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                userDto.RefreshToken = refreshToken.Token;
                userDto.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await userManager.UpdateAsync(user);
            }

            return userDto;
        }

        //check if refresh token is not expired => generate new Access Token and send it to user
        public async Task<UserDTO> RefreshTokenAsync(string token)
        {
            var userDto = new UserDTO();

            var user = userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                return null;
            }

            var refreshToken = user.RefreshTokens.Single(u => u.Token == token);

            if (!refreshToken.IsActive) { return null; }
            //refreshToken.RevokedOn = DateTime.UtcNow;
            //var newRefreshToken = GenerateRefreshToken();
            //user.RefreshTokens.Add(newRefreshToken);
            //await userManager.UpdateAsync(user);
            var jwtToken = await CreateTokenAsync(user);
            userDto.Token = jwtToken.Token;
            userDto.Email = user.Email;
            userDto.Name = user.UserName;
            userDto.TokenExpiration = jwtToken.TokenExpiration;
            userDto.RefreshToken = jwtToken.RefreshToken;
            userDto.RefreshTokenExpiration = jwtToken.RefreshTokenExpiration;

            return userDto;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
