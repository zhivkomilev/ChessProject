using Chess.Core.Domain.Interfaces;
using Chess.Users.Models.SettingsModels;
using Chess.Users.Models.UserModels;
using Chess.Users.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Users.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;
        private readonly IDateTimeProvider _dateTime;

        public TokenService(ChessUsersSettings settings,
            IDateTimeProvider dateTime)
        {
            _settings = settings.JwtSettings;
            _dateTime = dateTime;
        }

        public async Task<string> GenerateJwtAsync(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(_dateTime.Now).ToUnixTimeSeconds().ToString())
            };

            foreach (var aud in _settings.Audiences)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Aud, aud));
            }

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: null,
                claims: claims,
                expires: _dateTime.Now.Add(TimeSpan.FromMinutes(_settings.ExpirationInMinutes)),
                signingCredentials: credentials);

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}