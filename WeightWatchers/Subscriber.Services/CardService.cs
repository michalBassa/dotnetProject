using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Subscriber.Core;
using Subscriber.Core.InterfaceDAL;
using Subscriber.Core.InterfaceService;
using Subscriber.Core.Response;
using Subscriber.DAL;
using Subscriber.Data.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Services
{
    public class CardService : ICardService
    {
        private readonly Settings _settings;
        readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository,Settings settings)
        {
            _cardRepository = cardRepository;
            _settings = settings;    
        }
        //public async Task<BaseResponseGeneral<Card>> Login(string password, string email)
        //{
        //    return await _cardRepository.Login(password, email);
        //}
        public async Task<BaseResponseGeneral<LoginResponse>> Login(string password, string email)
        {
            BaseResponseGeneral<LoginResponse> response = new BaseResponseGeneral<LoginResponse>();
            response = await _cardRepository.Login(password, email);
            if (response.Succssed==true)
            {
                var claims = new List<Claim>()
                {
                 // new Claim(ClaimTypes.NameIdentifier, userName),
                   new Claim(ClaimTypes.Role ,"1")
                };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Jwt.Key));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                issuer: _settings.Jwt.Issuer,
                audience: _settings.Jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(100),
                signingCredentials: signinCredentials
);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                response.Response.Token = tokenString;
                response.Succssed = true;
                return response;
            }
            response.Succssed = false;
            response.Massage = "you cant enter";
            return response;
        }
        public async Task<BaseResponseGeneral<SubscriptionDetailsResponse>> GetSubscriptionDetails(int id)
        {
            return await _cardRepository.GetSubscriptionDetails(id);
        }

    }
}

